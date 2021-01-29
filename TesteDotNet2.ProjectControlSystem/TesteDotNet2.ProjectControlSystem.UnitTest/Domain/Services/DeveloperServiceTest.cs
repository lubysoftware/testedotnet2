using FizzWare.NBuilder;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Services;
using Xunit;

namespace TesteDotNet2.ProjectControlSystem.UnitTest.Domain.Services
{
    public class DeveloperServiceTest
    {
        public Mock<IDeveloperRepository> mockDeveloperRepository { get; set; }
        public DeveloperService developerService { get; set; }

        public DeveloperServiceTest()
        {
            mockDeveloperRepository = new Mock<IDeveloperRepository>();
            developerService = new DeveloperService(mockDeveloperRepository.Object);
        }

        [Fact(DisplayName = "Add developer service")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Add_Success()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();

            Developer developerResponse = null;

            MessageResponse messageResponse = Builder<MessageResponse>.CreateNew()
                .With(x => x.message, "Autorizado")
                .Build();

            // Arrange
            mockDeveloperRepository.Setup(m => m.Add(developer)).Returns(developer);
            mockDeveloperRepository.Setup(m => m.GetByCPF(developer.CPF)).Returns(developerResponse);
            mockDeveloperRepository.Setup(m => m.ValidateCPFAsync(developer.CPF)).Returns(Task.FromResult(messageResponse));

            // Act
            var result = developerService.Add(developer);

            // Assert
            mockDeveloperRepository.Verify(m => m.Add(developer), Times.Once);
            mockDeveloperRepository.Verify(m => m.GetByCPF(developer.CPF), Times.Once);
            mockDeveloperRepository.Verify(m => m.ValidateCPFAsync(developer.CPF), Times.Once);
            Assert.IsType<Developer>(result);
        }

        [Fact(DisplayName = "Add developer service DeveloperExist")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Add_DeveloperExist()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();

            Developer developerResponse = Builder<Developer>.CreateNew().Build();
            // Arrange            
            mockDeveloperRepository.Setup(m => m.GetByCPF(developer.CPF)).Returns(developerResponse);           

            // Act
            var result = developerService.Add(developer);

            // Assert
            mockDeveloperRepository.Verify(m => m.Add(developer), Times.Never);
            mockDeveloperRepository.Verify(m => m.GetByCPF(developer.CPF), Times.Once);
            mockDeveloperRepository.Verify(m => m.ValidateCPFAsync(developer.CPF), Times.Never);
            Assert.True(result.Messages.First() == "Registro já existe");
        }

        [Fact(DisplayName = "Add developer service DeveloperUnauthorized")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Add_Unauthorized()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();

            Developer developerResponse = null;

            MessageResponse messageResponse = Builder<MessageResponse>.CreateNew()
                .With(x => x.message, "")
                .Build();

            // Arrange            
            mockDeveloperRepository.Setup(m => m.GetByCPF(developer.CPF)).Returns(developerResponse);
            mockDeveloperRepository.Setup(m => m.ValidateCPFAsync(developer.CPF)).Returns(Task.FromResult(messageResponse));

            // Act
            var result = developerService.Add(developer);

            // Assert
            mockDeveloperRepository.Verify(m => m.Add(developer), Times.Never);
            mockDeveloperRepository.Verify(m => m.GetByCPF(developer.CPF), Times.Once);
            mockDeveloperRepository.Verify(m => m.ValidateCPFAsync(developer.CPF), Times.Once);
            Assert.True(result.Messages.First() == "Registro inválido");
        }

        [Fact(DisplayName = "Delete developer service")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Delete_Success()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();

            bool response = true;      

            // Arrange            
            mockDeveloperRepository.Setup(m => m.Delete(developer.DeveloperId)).Returns(response);           

            // Act
            var result = developerService.Delete(developer.DeveloperId);

            // Assert
            mockDeveloperRepository.Verify(m => m.Delete(developer.DeveloperId), Times.Once);            
            Assert.True(result);
        }

        [Fact(DisplayName = "Get developer service")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Get_Success()
        {
            List<Developer> developers = (List<Developer>)Builder<Developer>.CreateListOfSize(3).Build();

            int page = 1;
            int size = 10;

            // Arrange            
            mockDeveloperRepository.Setup(m => m.Get(page, size)).Returns(developers);

            // Act
            var result = developerService.Get(page, size);

            // Assert
            mockDeveloperRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count > 0);
        }

        [Fact(DisplayName = "Get developer service empty list")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Get_EmptyList()
        {
            List<Developer> developers = new List<Developer>();

            int page = 30;
            int size = 10;

            // Arrange            
            mockDeveloperRepository.Setup(m => m.Get(page, size)).Returns(developers);

            // Act
            var result = developerService.Get(page, size);

            // Assert
            mockDeveloperRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count == 0);
        }

        [Fact(DisplayName = "GetById developer service")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_GetById_Success()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();

            // Arrange            
            mockDeveloperRepository.Setup(m => m.GetById(developer.DeveloperId)).Returns(developer);

            // Act
            var result = developerService.GetById(developer.DeveloperId);

            // Assert
            mockDeveloperRepository.Verify(m => m.GetById(developer.DeveloperId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "GetById developer service not exist")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_GetById_NotExist()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();
            Developer developerResponse = null;

            // Arrange            
            mockDeveloperRepository.Setup(m => m.GetById(developer.DeveloperId)).Returns(developerResponse);

            // Act
            var result = developerService.GetById(developer.DeveloperId);

            // Assert
            mockDeveloperRepository.Verify(m => m.GetById(developer.DeveloperId), Times.Once);
            Assert.Null(result);
        }

        [Fact(DisplayName = "Update developer service")]
        [Trait("Category", "Developer Service test")]
        public void DeveloperService_Update_Success()
        {
            Developer developer = Builder<Developer>.CreateNew().Build();            

            // Arrange            
            mockDeveloperRepository.Setup(m => m.Update(developer)).Returns(developer);

            // Act
            var result = developerService.Update(developer);

            // Assert
            mockDeveloperRepository.Verify(m => m.Update(developer), Times.Once);
            Assert.NotNull(result);
        }
    }
}

