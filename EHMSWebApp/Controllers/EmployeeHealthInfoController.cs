using EHMSWebApp.Interface;
using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EHMSWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeHealthInfoController : ControllerBase
    {
        private readonly IEmployeeHealthInfoService _employeeHealthInfoService;
        private readonly IWebHostEnvironment _environment;
        public EmployeeHealthInfoController(IEmployeeHealthInfoService employeeHealthInfoService, IWebHostEnvironment environment)
        {
            _employeeHealthInfoService = employeeHealthInfoService;
            _environment = environment;
        }

        [HttpGet("GetAllEmployeeHealthInfo")]
        public async Task<IActionResult> GetAllEmployeeHealthInfo()
        {
            List<EmployeeHealthInfo> employees = (await _employeeHealthInfoService.GetAllEmployeeHealthInfo()).ToList();
            return Ok(employees);
        }

        [HttpPost("CreateEmployeeHealthInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateEmployeeHealthInfo([FromForm] EmployeeHealthInfo _employeeHealthInfo, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            if (!file.ContentType.Equals("application/pdf", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file type. Only PDF files are allowed.");
            }
            const long maxFileSize = 10 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds 10 MB. Please upload a smaller file.");
            }

            string? uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            byte[] encryptedBytes = FileEncryption.EncryptFile(fileBytes);
            string? filePath = Path.Combine(uploadsPath, file.FileName + ".enc");
            await System.IO.File.WriteAllBytesAsync(filePath, encryptedBytes);

            _employeeHealthInfo.RecentMedicalReportPath = uploadsPath;
            _employeeHealthInfo.MedicalReportFileName = file.FileName;

            int res = await _employeeHealthInfoService.CreateEmployeeHealthInfoAsync(_employeeHealthInfo);
            return Ok(res);
        }

        [HttpPut("UpdateEmployeeHealthInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateEmployeeHealthInfo([FromForm] EmployeeHealthInfo _employeeHealthInfo, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Invalid file type. Only PDF files are allowed.");
            }
            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("File size exceeds the 10MB limit.");
            }

            string? uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            string filenameguid = Guid.NewGuid()+ file.FileName;

            byte[] encryptedBytes = FileEncryption.EncryptFile(fileBytes);
            string? filePath = Path.Combine(uploadsPath, filenameguid + ".enc");
            await System.IO.File.WriteAllBytesAsync(filePath, encryptedBytes);

            _employeeHealthInfo.RecentMedicalReportPath = filePath;
            _employeeHealthInfo.MedicalReportFileName = file.FileName;

            int res = await _employeeHealthInfoService.UpdateEmployeeHealthInfoAsync(_employeeHealthInfo);
            return Ok(res);
        }

        [HttpDelete("DeleteEmployeeHealthInfo")]
        public async Task<IActionResult> DeleteEmployeeHealthInfo(int id, string medicalReportFileName)
        {
            int res = await _employeeHealthInfoService.DeleteEmployeeHealthInfoAsync(id);
            string? encryptedFilePath = Path.Combine(_environment.WebRootPath, "uploads", medicalReportFileName + ".enc");
            if (System.IO.File.Exists(encryptedFilePath))
            {
                System.IO.File.Delete(encryptedFilePath);
            }
            return Ok(res);
        }

        [HttpGet("DownloadMedicalReport/{fileName}")]
        public async Task<IActionResult> DownloadMedicalReport(string fileName)
        {
            string? encryptedFilePath = Path.Combine(_environment.WebRootPath, "uploads", fileName + ".enc");
            byte[] encryptedBytes = await System.IO.File.ReadAllBytesAsync(encryptedFilePath);
            byte[] decryptedBytes = FileEncryption.DecryptFile(encryptedBytes);
            return File(decryptedBytes, "application/pdf", $"{fileName}.pdf");
        }

    }
}
