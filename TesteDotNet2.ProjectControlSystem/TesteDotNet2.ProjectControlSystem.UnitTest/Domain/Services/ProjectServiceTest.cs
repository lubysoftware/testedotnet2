using FizzWare.NBuilder;
using Moq;
using System.Collections.Generic;
using TesteDotNet2.ProjectControlSystem.Domain.Entities;
using TesteDotNet2.ProjectControlSystem.Domain.Interfaces.Repository;
using TesteDotNet2.ProjectControlSystem.Domain.Services;
using Xunit;

namespace TesteDotNet2.ProjectControlSystem.UnitTest.Domain.Services
{
    public class ProjectServiceTest
    {
        public Mock<IProjectRepository> mockProjectRepository { get; set; }
        public ProjectService projectService { get; set; }

        public ProjectServiceTest()
        {
            mockProjectRepository = new Mock<IProjectRepository>();
            projectService = new ProjectService(mockProjectRepository.Object);
        }

        [Fact(DisplayName = "Add project service")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_Add_Success()
        {
            Project project = Builder<Project>.CreateNew().Build();

            // Arrange
            mockProjectRepository.Setup(m => m.Add(project)).Returns(project);            

            // Act
            var result = projectService.Add(project);

            // Assert
            mockProjectRepository.Verify(m => m.Add(project), Times.Once);            
            Assert.IsType<Project>(result);
        }                

        [Fact(DisplayName = "Delete project service")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_Delete_Success()
        {
            Project project = Builder<Project>.CreateNew().Build();
            bool response = true;

            // Arrange            
            mockProjectRepository.Setup(m => m.Delete(project.ProjectId)).Returns(response);

            // Act
            var result = projectService.Delete(project.ProjectId);

            // Assert
            mockProjectRepository.Verify(m => m.Delete(project.ProjectId), Times.Once);
            Assert.True(result);
        }

        [Fact(DisplayName = "Get project service")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_Get_Success()
        {
            List<Project> projects = (List<Project>)Builder<Project>.CreateListOfSize(3).Build();

            int page = 1;
            int size = 10;

            // Arrange            
            mockProjectRepository.Setup(m => m.Get(page, size)).Returns(projects);

            // Act
            var result = projectService.Get(page, size);

            // Assert
            mockProjectRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count > 0);
        }

        [Fact(DisplayName = "Get project service empty list")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_Get_EmptyList()
        {
            List<Project> projects = new List<Project>();

            int page = 30;
            int size = 10;

            // Arrange            
            mockProjectRepository.Setup(m => m.Get(page, size)).Returns(projects);

            // Act
            var result = projectService.Get(page, size);

            // Assert
            mockProjectRepository.Verify(m => m.Get(page, size), Times.Once);
            Assert.True(result.Count == 0);
        }

        [Fact(DisplayName = "GetById project service")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_GetById_Success()
        {
            Project project = Builder<Project>.CreateNew().Build();

            // Arrange            
            mockProjectRepository.Setup(m => m.GetById(project.ProjectId)).Returns(project);

            // Act
            var result = projectService.GetById(project.ProjectId);

            // Assert
            mockProjectRepository.Verify(m => m.GetById(project.ProjectId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "GetById project service not exist")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_GetById_NotExist()
        {
            Project project = Builder<Project>.CreateNew().Build();
            Project projectResponse = null;

            // Arrange            
            mockProjectRepository.Setup(m => m.GetById(project.ProjectId)).Returns(projectResponse);

            // Act
            var result = projectService.GetById(project.ProjectId);

            // Assert
            mockProjectRepository.Verify(m => m.GetById(project.ProjectId), Times.Once);
            Assert.Null(result);
        }

        [Fact(DisplayName = "Update project service")]
        [Trait("Category", "Project Service test")]
        public void ProjectService_Update_Success()
        {
            Project project = Builder<Project>.CreateNew().Build();

            // Arrange            
            mockProjectRepository.Setup(m => m.Update(project)).Returns(project);

            // Act
            var result = projectService.Update(project);

            // Assert
            mockProjectRepository.Verify(m => m.Update(project), Times.Once);
            Assert.NotNull(result);
        }
    }
}

