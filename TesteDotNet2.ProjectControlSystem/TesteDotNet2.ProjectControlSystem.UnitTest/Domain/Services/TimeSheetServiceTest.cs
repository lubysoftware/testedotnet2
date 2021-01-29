using FizzWare.NBuilder;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Services;
using Xunit;

namespace TesteDotNet2.TimeSheetControlSystem.UnitTest.Domain.Services
{
    public class TimeSheetServiceTest
    {
        public Mock<ITimeSheetRepository> mockTimeSheetRepository { get; set; }
        public TimeSheetService timeSheetService { get; set; }

        public TimeSheetServiceTest()
        {
            mockTimeSheetRepository = new Mock<ITimeSheetRepository>();
            timeSheetService = new TimeSheetService(mockTimeSheetRepository.Object);
        }

        [Fact(DisplayName = "Add timeSheet service")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_Add_Success()
        {
            TimeSheet timeSheet = Builder<TimeSheet>.CreateNew().Build();
            MessageResponse messageResponse = Builder<MessageResponse>.CreateNew()
             .With(x => x.message, "Enviado")
             .Build();
            // Arrange
            mockTimeSheetRepository.Setup(m => m.Add(timeSheet)).Returns(timeSheet);
            mockTimeSheetRepository.Setup(m => m.Notify(It.IsAny<Guid>())).Returns(Task.FromResult(messageResponse));

            // Act
            var result = timeSheetService.Add(timeSheet);

            // Assert
            mockTimeSheetRepository.Verify(m => m.Add(timeSheet), Times.Once);
            mockTimeSheetRepository.Verify(m => m.Notify(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<TimeSheet>(result);
            Assert.Equal("Enviado", result.Messages.First());
        }

        [Fact(DisplayName = "Delete timeSheet service")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_Delete_Success()
        {
            TimeSheet timeSheet = Builder<TimeSheet>.CreateNew().Build();
            bool response = true;

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.Delete(timeSheet.TimeSheetId)).Returns(response);

            // Act
            var result = timeSheetService.Delete(timeSheet.TimeSheetId);

            // Assert
            mockTimeSheetRepository.Verify(m => m.Delete(timeSheet.TimeSheetId), Times.Once);
            Assert.True(result);
        }

        [Fact(DisplayName = "Get timeSheet service")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_Get_Success()
        {
            List<TimeSheet> timeSheets = (List<TimeSheet>)Builder<TimeSheet>.CreateListOfSize(3).Build();

            int page = 1;
            int size = 10;

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.Get(page, size)).Returns(timeSheets);

            // Act
            var result = timeSheetService.Get(page, size);

            // Assert
            mockTimeSheetRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count > 0);
        }

        [Fact(DisplayName = "Get timeSheet service empty list")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_Get_EmptyList()
        {
            List<TimeSheet> timeSheets = new List<TimeSheet>();

            int page = 30;
            int size = 10;

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.Get(page, size)).Returns(timeSheets);

            // Act
            var result = timeSheetService.Get(page, size);

            // Assert
            mockTimeSheetRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count == 0);
        }

        [Fact(DisplayName = "GetById timeSheet service")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_GetById_Success()
        {
            TimeSheet timeSheet = Builder<TimeSheet>.CreateNew().Build();

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.GetById(timeSheet.TimeSheetId)).Returns(timeSheet);

            // Act
            var result = timeSheetService.GetById(timeSheet.TimeSheetId);

            // Assert
            mockTimeSheetRepository.Verify(m => m.GetById(timeSheet.TimeSheetId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "GetById timeSheet service not exist")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_GetById_NotExist()
        {
            TimeSheet timeSheet = Builder<TimeSheet>.CreateNew().Build();
            TimeSheet timeSheetResponse = null;

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.GetById(timeSheet.TimeSheetId)).Returns(timeSheetResponse);

            // Act
            var result = timeSheetService.GetById(timeSheet.TimeSheetId);

            // Assert
            mockTimeSheetRepository.Verify(m => m.GetById(timeSheet.TimeSheetId), Times.Once);
            Assert.Null(result);
        }

        [Fact(DisplayName = "Update timeSheet service")]
        [Trait("Category", "TimeSheet Service test")]
        public void TimeSheetService_Update_Success()
        {
            TimeSheet timeSheet = Builder<TimeSheet>.CreateNew().Build();

            // Arrange            
            mockTimeSheetRepository.Setup(m => m.Update(timeSheet)).Returns(timeSheet);

            // Act
            var result = timeSheetService.Update(timeSheet);

            // Assert
            mockTimeSheetRepository.Verify(m => m.Update(timeSheet), Times.Once);
            Assert.NotNull(result);
        }
    }
}

