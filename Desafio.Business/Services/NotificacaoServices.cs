using Desafio.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace Desafio.Business.Services
{

    public class NotificacaoService : INotificacaoService
    {
        private readonly IConfiguration _mySettings;        


        public NotificacaoService(IConfiguration mySettings)
        {
            _mySettings = mySettings;            
        }



        public async Task<bool> EnviarNotificacao()
        {
            var mensagemEnviada = false;

            HttpClient clientGet = new HttpClient();
            HttpResponseMessage responseCliente = clientGet.GetAsync(_mySettings.GetSection("EndPoints:NotificacaoApi").Value).Result;

            if (responseCliente.IsSuccessStatusCode)
            {
                var responseContent = await responseCliente.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if (result.message == "Enviado")
                {
                    mensagemEnviada = true;
                }
            }

            return mensagemEnviada;
        }
    }


}