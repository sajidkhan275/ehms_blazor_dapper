using EHMSWebApp.Interface;
using EHMSModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHMSWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeePhysicalFitnessController : ControllerBase
    {
        private readonly IEmployeePhysicalFitnessService _employeePhysicalFitnessService;
        public EmployeePhysicalFitnessController(IEmployeePhysicalFitnessService employeePhysicalFitnessService)
        {
            _employeePhysicalFitnessService = employeePhysicalFitnessService;
        }

        [HttpGet("GetAllEmployeePhysicalFitness")]
        public async Task<IActionResult> GetAllEmployeePhysicalFitness()
        {
            List<EmployeePhysicalFitness> employees = (await _employeePhysicalFitnessService.GetAllEmployeePhysicalFitness()).ToList();
            return Ok(employees);
        }

        [HttpPost("CreateEmployeePhysicalFitness")]
        public async Task<IActionResult> CreateEmployeePhysicalFitness(EmployeePhysicalFitness physicalFitness)
        {
            int res = await _employeePhysicalFitnessService.CreateEmployeePhysicalFitnessAsync(physicalFitness);
            return Ok(res);
        }

        [HttpPut("UpdateEmployeePhysicalFitness")]
        public async Task<IActionResult> UpdateEmployeePhysicalFitness(EmployeePhysicalFitness physicalFitness)
        {
            int res = await _employeePhysicalFitnessService.UpdateEmployeePhysicalFitnessAsync(physicalFitness);
            return Ok(res);
        }

        [HttpDelete("DeleteEmployeePhysicalFitness")]
        public async Task<IActionResult> DeleteEmployeePhysicalFitness(int id)
        {
            int res = await _employeePhysicalFitnessService.DeleteEmployeePhysicalFitnessAsync(id);
            return Ok(res);
        }
    }
}
