using EHMSWebApp.Controllers;
using EHMSWebApp.Interface;
using EHMSModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;
using Xunit;
using EHMSWebApp.Utility;
using AutoFixture;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EHMSTest.API
{
    public class EmployeeHealthInfoControllerTests
    {
        private readonly Mock<IEmployeeHealthInfoService> _mockEmployeeHealthInfoService;
        private readonly Mock<IWebHostEnvironment> _mockEnvironment;
        private readonly EmployeeHealthInfoController _targert;
        private readonly Fixture _fixture;

        public EmployeeHealthInfoControllerTests()
        {
            _mockEmployeeHealthInfoService = new Mock<IEmployeeHealthInfoService>();
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());
            _targert = new EmployeeHealthInfoController(_mockEmployeeHealthInfoService.Object, _mockEnvironment.Object);
            _fixture = new Fixture();
        }

        [Fact]

        public async Task CreateEmployeeHealthInfo_WhenNoFileUploaded()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            IFormFile? file = null;

            // Act
            var result = await _targert.CreateEmployeeHealthInfo(employeeHealthInfo, file);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("No file uploaded.", badRequest.Value);
        }

        [Fact]
        public async Task CreateEmployeeHealthInfo_WhenFileTypeIsInvalid()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.ContentType).Returns("text/plain");
            fileMock.Setup(f => f.Length).Returns(1024);

            // Act
            var result = await _targert.CreateEmployeeHealthInfo(employeeHealthInfo, fileMock.Object);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("Invalid file type. Only PDF files are allowed.", badRequest.Value);
        }

        [Fact]
        public async Task CreateEmployeeHealthInfo_WhenFileSizeExceedsLimit()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.ContentType).Returns("application/pdf");
            fileMock.Setup(f => f.Length).Returns(15 * 1024 * 1024);

            // Act
            var result = await _targert.CreateEmployeeHealthInfo(employeeHealthInfo, fileMock.Object);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("File size exceeds 10 MB. Please upload a smaller file.", badRequest.Value);
        }

        [Fact]
        public async Task CreateEmployeeHealthInfo_ShouldReturnOk()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            string fileName = "test.pdf";
            var fileMock = new Mock<IFormFile>();

            byte[] fileContent = Encoding.UTF8.GetBytes("This is a test PDF file.");
            var stream = new MemoryStream(fileContent);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.ContentType).Returns("application/pdf");
            fileMock.Setup(f => f.Length).Returns(fileContent.Length);
            fileMock.Setup(f => f.FileName).Returns(fileName);

            _mockEmployeeHealthInfoService.Setup(s => s.CreateEmployeeHealthInfoAsync(It.IsAny<EmployeeHealthInfo>())).ReturnsAsync(1);

            // Act
            var result = await _targert.CreateEmployeeHealthInfo(employeeHealthInfo, fileMock.Object);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);

            _mockEmployeeHealthInfoService.Verify(s => s.CreateEmployeeHealthInfoAsync(It.Is<EmployeeHealthInfo>(e => e.MedicalReportFileName == fileName)), Times.Once);
        }

        [Fact]
        public async Task CreateEmployeeHealthInfo_Directory_WhenNotExists()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            var fileMock = new Mock<IFormFile>();
            byte[] fileContent = new byte[100];
            string tempPath = Path.Combine(Path.GetTempPath(), "uploads");
            string fileName = "test.pdf";

            fileMock.Setup(f => f.ContentType).Returns("application/pdf");
            fileMock.Setup(f => f.Length).Returns(1024);
            fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileContent));
            fileMock.Setup(f => f.FileName).Returns(fileName);

            _mockEnvironment.Setup(env => env.WebRootPath).Returns(tempPath);
            _mockEmployeeHealthInfoService.Setup(service => service.CreateEmployeeHealthInfoAsync(It.IsAny<EmployeeHealthInfo>())).ReturnsAsync(1);

            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            // Act
            var result = await _targert.CreateEmployeeHealthInfo(employeeHealthInfo, fileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
            Assert.True(Directory.Exists(tempPath));
            _mockEmployeeHealthInfoService.Verify(s => s.CreateEmployeeHealthInfoAsync(It.Is<EmployeeHealthInfo>(e => e.MedicalReportFileName == fileName)), Times.Once);
        }

        [Fact]
        public async Task UploadEmployeeHealthInfo_Directory_WhenNotExists()
        {
            // Arrange
            var employeeHealthInfo = new EmployeeHealthInfo();
            var fileMock = new Mock<IFormFile>();
            byte[] fileContent = new byte[100];
            string tempPath = Path.Combine(Path.GetTempPath(), "uploads");
            string fileName = "test.pdf";

            fileMock.Setup(f => f.ContentType).Returns("application/pdf");
            fileMock.Setup(f => f.Length).Returns(1024);
            fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(fileContent));
            fileMock.Setup(f => f.FileName).Returns(fileName);

            _mockEnvironment.Setup(env => env.WebRootPath).Returns(tempPath);
            _mockEmployeeHealthInfoService.Setup(service => service.UpdateEmployeeHealthInfoAsync(It.IsAny<EmployeeHealthInfo>())).ReturnsAsync(1);

            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            // Act
            var result = await _targert.UpdateEmployeeHealthInfo(employeeHealthInfo, fileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
            Assert.True(Directory.Exists(tempPath));
            _mockEmployeeHealthInfoService.Verify(s => s.UpdateEmployeeHealthInfoAsync(It.Is<EmployeeHealthInfo>(e => e.MedicalReportFileName == fileName)), Times.Once);
        }
        [Fact]
        public async Task DeleteEmployeeHealthInfo_DeleteFile()
        {
            // Arrange
            int employeeId = 1;
            string medicalReportFileName = "test-report";
            string encryptedFilePath = Path.Combine(Path.GetTempPath(), "uploads", medicalReportFileName + ".enc");
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "uploads"));
            await File.WriteAllTextAsync(encryptedFilePath, "Mock file content");

            _mockEmployeeHealthInfoService
                .Setup(service => service.DeleteEmployeeHealthInfoAsync(employeeId))
                .ReturnsAsync(1);

            // Act
            var result = await _targert.DeleteEmployeeHealthInfo(employeeId, medicalReportFileName);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(1, okResult.Value);
            Assert.False(File.Exists(encryptedFilePath)); _mockEmployeeHealthInfoService.Verify(service => service.DeleteEmployeeHealthInfoAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task DownloadMedicalReport_ReturnsFileResult_WithDecryptedPdf()
        {
            // Arrange
            string fileName = "testReport";
            string encryptedFilePath = Path.Combine(Path.GetTempPath(), "uploads", fileName + ".enc");

            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "uploads"));
            byte[] originalBytes = Encoding.UTF8.GetBytes("Test PDF Content");
            byte[] encryptedBytes = FileEncryption.EncryptFile(originalBytes);
            await File.WriteAllBytesAsync(encryptedFilePath, encryptedBytes);

            // Act
            var result = await _targert.DownloadMedicalReport(fileName);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal($"{fileName}.pdf", fileResult.FileDownloadName);
            Assert.Equal(originalBytes, fileResult.FileContents);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnOkResultWithData()
        {
            //Arragnge
            var expectedEmployee = _fixture.Build<EmployeeHealthInfo>()
                .With(c => c.EmployeeName, "Test")
                .CreateMany(2);
            _mockEmployeeHealthInfoService.Setup(x => x.GetAllEmployeeHealthInfo()).ReturnsAsync(expectedEmployee);

            //Act
            var actual = await _targert.GetAllEmployeeHealthInfo();

            //Assert
            Assert.NotNull(actual);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnEmployees = Assert.IsType<List<EmployeeHealthInfo>>(okResult.Value);
            Assert.Equal(expectedEmployee.Count(), returnEmployees.Count);
            _mockEmployeeHealthInfoService.Verify(x => x.GetAllEmployeeHealthInfo(), Times.Once);
        }
    }
}
