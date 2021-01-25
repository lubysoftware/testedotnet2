using Desafio.Business.Interfaces;
using Desafio.Business.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Desafio.Business.Services
{

    public class ValidacaoService : IValidacaoService
    {


        private readonly IConfiguration _mySettings;
        private readonly IProjetoDesenvolvedorRepository _projetoDesenvolvedorRepository;


        public ValidacaoService(IConfiguration mySettings, IProjetoDesenvolvedorRepository projetoDesenvolvedorRepository)
        {
            _mySettings = mySettings;
            _projetoDesenvolvedorRepository = projetoDesenvolvedorRepository;
        }



        public async Task<bool> ValidarCPF(string cpf)
        {
            var cpfValido = false;

            HttpClient clientGet = new HttpClient();
            HttpResponseMessage responseCliente = clientGet.GetAsync(_mySettings.GetSection("EndPoints:CPFApi").Value).Result;

            if (responseCliente.IsSuccessStatusCode)
            {
                var responseContent = await responseCliente.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if (result.message == "Autorizado")
                {
                    cpfValido = true;
                }
            }

            return cpfValido;
        }


        public bool ValidarDesenvolvedorNoProjeto(int ProjetoID, int DesenvolvedorID)
        {

            var Vinculado =  (_projetoDesenvolvedorRepository.Buscar(x => x.ProjetoID == ProjetoID && x.DesenvolvedorID == DesenvolvedorID).Result.Any());


            return Vinculado;
        }



        public void Dispose()
        {
            _projetoDesenvolvedorRepository.Dispose();

        }


    }


}