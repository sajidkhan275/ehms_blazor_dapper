using AutoFixture;
using EHMSModel;
using EHMSWebApp.Controllers;
using EHMSWebApp.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EHMSTest.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestForHelpControllerTests : ControllerBase
    {
        private readonly Mock<IRequestForHelpService> _mockRequestForHelpServic;
        private readonly RequestForHelpController _targert;
        private readonly Fixture _fixture;
        public RequestForHelpControllerTests()
        {
            _mockRequestForHelpServic = new Mock<IRequestForHelpService>();
            _targert = new RequestForHelpController(_mockRequestForHelpServic.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var data = _fixture.Build<RequestForHelp>().CreateMany(2);
            _mockRequestForHelpServic.Setup(x => x.GetRequestsByEmployeeAsync()).ReturnsAsync(data);

            //Act
            var actual = await _targert.GetRequestsByEmployeeAsync();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<RequestForHelp>>(okResult.Value);
            Assert.Equal(data.Count(), returnEmployees.Count);
            _mockRequestForHelpServic.Verify(x => x.GetRequestsByEmployeeAsync(), Times.Once);
        }


        [Fact]
        public async Task GetRequestsByEmployeeIdAsync_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var data = _fixture.Build<RequestForHelp>().CreateMany(2);
            _mockRequestForHelpServic.Setup(x => x.GetRequestsByEmployeeIdAsync(1)).ReturnsAsync(data);

            //Act
            var actual = await _targert.GetRequestsByEmployeeIdAsync(1);

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<RequestForHelp>>(okResult.Value);
            Assert.Equal(data.Count(), returnEmployees.Count);
            _mockRequestForHelpServic.Verify(x => x.GetRequestsByEmployeeIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task CreateRequestForHelpAsync_ShouldReturnOk()
        {
            // Arrange
            var data = new RequestForHelp();
            _mockRequestForHelpServic.Setup(s => s.CreateRequestForHelpAsync(It.IsAny<RequestForHelp>())).ReturnsAsync(1);

            // Act
            var result = await _targert.CreateRequestForHelpAsync(data);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockRequestForHelpServic.Verify(s => s.CreateRequestForHelpAsync(It.IsAny<RequestForHelp>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDepartments_ShouldReturnOk()
        {
            // Arrange
            var data = new RequestForHelp();
            _mockRequestForHelpServic.Setup(s => s.UpdateHRRequestAsync(It.IsAny<RequestForHelp>())).ReturnsAsync(1);

            // Act
            var result = await _targert.UpdateHRRequestAsync(data);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockRequestForHelpServic.Verify(s => s.UpdateHRRequestAsync(It.IsAny<RequestForHelp>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDepartments_ShouldReturnOk()
        {
            // Arrange
            int id = 1;
            _mockRequestForHelpServic.Setup(s => s.DeleteRequestForHelpAsync(It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _targert.DeleteRequestForHelpAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            _mockRequestForHelpServic.Verify(s => s.DeleteRequestForHelpAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
