using AutoFixture;
using EHMSModel;
using EHMSWebApp.Controllers;
using EHMSWebApp.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EHMSTest.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentControllerTests : ControllerBase
    {
        private readonly Mock<IDepartmentService> _mockDepartmentServicee;
        private readonly DepartmentController _targert;
        private readonly Fixture _fixture;
        public DepartmentControllerTests()
        {
            _mockDepartmentServicee = new Mock<IDepartmentService>();
            _targert = new DepartmentController(_mockDepartmentServicee.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var data = _fixture.Build<DepartmentDetails>()
                .With(c => c.DepartmentName, "Test")
                .CreateMany(2);
            _mockDepartmentServicee.Setup(x => x.GetAllDepartments()).ReturnsAsync(data);

            //Act
            var actual = await _targert.GetAllDepartments();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<DepartmentDetails>>(okResult.Value);
            Assert.Equal(data.Count(), returnEmployees.Count);
            _mockDepartmentServicee.Verify(x => x.GetAllDepartments(), Times.Once);
        }

        [Fact]
        public async Task CreateDepartments_ShouldReturnOk()
        {
            // Arrange
            var data = new DepartmentDetails();

            _mockDepartmentServicee.Setup(s => s.CreateDepartments(It.IsAny<DepartmentDetails>())).ReturnsAsync(1);

            // Act
            var result = await _targert.CreateDepartments(data);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockDepartmentServicee.Verify(s => s.CreateDepartments(It.IsAny<DepartmentDetails>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartments_ShouldReturnOk()
        {
            // Arrange
            var data = new DepartmentDetails();

            _mockDepartmentServicee.Setup(s => s.UpdateDepartments(It.IsAny<DepartmentDetails>())).ReturnsAsync(1);

            // Act
            var result = await _targert.UpdateDepartments(data);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockDepartmentServicee.Verify(s => s.UpdateDepartments(It.IsAny<DepartmentDetails>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDepartments_ShouldReturnOk()
        {
            // Arrange
            int id = 1;

            _mockDepartmentServicee.Setup(s => s.DeleteDepartments(It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _targert.DeleteDepartments(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockDepartmentServicee.Verify(s => s.DeleteDepartments(It.IsAny<int>()), Times.Once);
        }
    }
}
