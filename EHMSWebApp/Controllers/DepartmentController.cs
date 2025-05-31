using EHMSModel;
using EHMSWebApp.DesignPattern;
using EHMSWebApp.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHMSWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        private readonly IDepartmentServiceFactory _departmentServiceFactory;
        public DepartmentController(IDepartmentService departmentService, IDepartmentServiceFactory departmentServiceFactory)
        {
            _departmentService = departmentService;
            _departmentServiceFactory = departmentServiceFactory;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            List<DepartmentDetails> employees = (await _departmentService.GetAllDepartments()).ToList();

            var departmentService = _departmentServiceFactory.CreateDepartmentService();

            var departments = await departmentService.GetAllDepartments();
            return Ok(employees);
        }

        [HttpPost("CreateDepartments")]
        public async Task<IActionResult> CreateDepartments(DepartmentDetails departmentDetails)
        {
            int res = await _departmentService.CreateDepartments(departmentDetails);
            return Ok(res);
        }

        [HttpPut("UpdateDepartments")]
        public async Task<IActionResult> UpdateDepartments(DepartmentDetails departmentDetails)
        {
            int res = await _departmentService.UpdateDepartments(departmentDetails);
            return Ok(res);
        }

        [HttpDelete("DeleteDepartments")]
        public async Task<IActionResult> DeleteDepartments(int id)
        {
            int res = await _departmentService.DeleteDepartments(id);
            return Ok(res);
        }

    }
}
