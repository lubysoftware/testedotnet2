using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TesteDotNet2.ProjectControlSystem.Services.ViewModel
{
    [DataContract(Name = "timeSheet")]
    public class TimeSheetViewModel
    {        
        public TimeSheetViewModel()
        {
            TimeSheetId = Guid.NewGuid();
        }

        [DataMember(Name = "timeSheetId")]
        public Guid TimeSheetId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: data inicial")]
        [DataMember(Name = "beginDate")]
        public DateTime BeginDate { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatório: data final")]
        [DataMember(Name = "endDate")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: Desenvolvedor")]
        [DataMember(Name = "developerId")]
        public Guid? DeveloperId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: Projeto")]
        [DataMember(Name = "projectId")]
        public Guid? ProjectId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: Quantidade de horas")]
        [DataMember(Name = "amountOfHours")]
        public int AmountOfHours { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }
    }
}
