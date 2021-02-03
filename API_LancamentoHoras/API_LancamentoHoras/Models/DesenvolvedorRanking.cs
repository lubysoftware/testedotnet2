using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LancamentoHoras.Models
{
    public class DesenvolvedorRanking
    {
        public int DesenvolvedorId { get; set; }
        public string DesenvolvedorNome { get; set; }
        public TimeSpan HorasSoma { get; set; }

        public DesenvolvedorRanking(int DesenvolvedorId, string DesenvolvedorNome, TimeSpan HorasSoma)
        {
            this.DesenvolvedorId = DesenvolvedorId;
            this.DesenvolvedorNome = DesenvolvedorNome;
            this.HorasSoma = HorasSoma;
        }
    }
}
