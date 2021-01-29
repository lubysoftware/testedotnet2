using System;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Service;

namespace TesteDotNet2.ProjectControlSystem.Domain.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository timeSheetRepository;
        public TimeSheetService(ITimeSheetRepository timeSheetRepository)
        {
            this.timeSheetRepository = timeSheetRepository;
        }
        public TimeSheet Add(TimeSheet timeSheet)
        {
            var response = this.timeSheetRepository.Add(timeSheet);
            var notifyResponse = this.timeSheetRepository.Notify(response.TimeSheetId);
            response.Messages = new List<string>();
            if(notifyResponse == null)
            {
                response.Messages.Add("Não foi possível enviar a notificação");
            }
            else
            {
                response.Messages.Add(notifyResponse.Result.message);
            }

            return response;
        }

        public bool Delete(Guid id)
        {
            return this.timeSheetRepository.Delete(id);
        }

        public List<TimeSheet> Get(int page, int size)
        {
            return this.timeSheetRepository.Get(page, size);
        }

        public TimeSheet GetById(Guid id)
        {
            return this.timeSheetRepository.GetById(id);
        }

        public TimeSheet Update(TimeSheet timeSheet)
        {
            return this.timeSheetRepository.Update(timeSheet);
        }
    }
}
