using AutoFixture;
using EHMSModel;
using EHMSWebApp.Controllers;
using EHMSWebApp.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class EmployeePhysicalFitnessControllerTests : ControllerBase
    {
        private readonly Mock<IEmployeePhysicalFitnessService> _mockEmployeePhysicalFitnessService;
        private readonly EmployeePhysicalFitnessController _targert;
        private readonly Fixture _fixture;

        public EmployeePhysicalFitnessControllerTests()
        {
            _mockEmployeePhysicalFitnessService = new Mock<IEmployeePhysicalFitnessService>();
            _targert = new EmployeePhysicalFitnessController(_mockEmployeePhysicalFitnessService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllEmployeePhysicalFitness_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var expectedEmployee = _fixture.Build<EmployeePhysicalFitness>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(2);
            _mockEmployeePhysicalFitnessService.Setup(x => x.GetAllEmployeePhysicalFitness()).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetAllEmployeePhysicalFitness();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<EmployeePhysicalFitness>>(okResult.Value);
            Assert.Equal(expectedEmployee.Count(), returnEmployees.Count);
            _mockEmployeePhysicalFitnessService.Verify(x => x.GetAllEmployeePhysicalFitness(), Times.Once);
        }


        [Fact]
        public async Task CreateEmployeePhysicalFitness_ShouldReturnOk()
        {
            // Arrange
            var employeeHealthInfo = new EmployeePhysicalFitness();

            _mockEmployeePhysicalFitnessService.Setup(s => s.CreateEmployeePhysicalFitnessAsync(It.IsAny<EmployeePhysicalFitness>())).ReturnsAsync(1);

            // Act
            var result = await _targert.CreateEmployeePhysicalFitness(employeeHealthInfo);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockEmployeePhysicalFitnessService.Verify(s => s.CreateEmployeePhysicalFitnessAsync(It.IsAny<EmployeePhysicalFitness>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeePhysicalFitness_ShouldReturnOk()
        {
            // Arrange
            var employeeHealthInfo = new EmployeePhysicalFitness();

            _mockEmployeePhysicalFitnessService.Setup(s => s.UpdateEmployeePhysicalFitnessAsync(It.IsAny<EmployeePhysicalFitness>())).ReturnsAsync(1);

            // Act
            var result = await _targert.UpdateEmployeePhysicalFitness(employeeHealthInfo);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockEmployeePhysicalFitnessService.Verify(s => s.UpdateEmployeePhysicalFitnessAsync(It.IsAny<EmployeePhysicalFitness>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeePhysicalFitness_ShouldReturnOk()
        {
            // Arrange
            int id = 1;

            _mockEmployeePhysicalFitnessService.Setup(s => s.DeleteEmployeePhysicalFitnessAsync(It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _targert.DeleteEmployeePhysicalFitness(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockEmployeePhysicalFitnessService.Verify(s => s.DeleteEmployeePhysicalFitnessAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
