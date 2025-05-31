using EHMSWebApp.Interface;
using EHMSModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EHMSWebApp.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                List<EmployeeDetails> employees = (await _employeeService.GetAllEmployeesAsync()).ToList();
                return Ok(employees);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all employees.");
                return Ok(new List<EmployeeDetails>());
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDetails employee)
        {
            try
            {
                int res = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(res);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating employees.");
                return Ok(0);
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDetails employee)
        {
            try
            {
                int res = await _employeeService.UpdateEmployeeAsync(employee);
                return Ok(res);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employees.");
                return Ok(0);
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                int res = await _employeeService.DeleteEmployeeAsync(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employees.");
                return Ok(0);
            }
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(EmployeeRole role)
        {
            try
            {
                int res = await _employeeService.AddRoleAsync(role);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while add role.");
                return Ok(0);
            }
        }

        [HttpGet("GetRoleEmpWise")]
        public async Task<IActionResult> GetRoleEmpWiseAsync(string entraId)
        {
            try
            {
                EmployeeWithRoleDetails employees = (await _employeeService.GetRoleEmpWiseAsync(entraId));
                _logger.LogError("getting all employees role wise.");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting role employees.");
                return Ok(new EmployeeWithRoleDetails());
            }
        }

        [HttpDelete("DeletRoleEmpWise")]
        public async Task<IActionResult> DeletRoleEmpWiseAsync(EmployeeRole role)
        {
            try
            {
                int res = await _employeeService.DeletRoleEmpWiseAsync(role);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting role.");
                return Ok(0);
            }
        }

        [HttpGet("GetEmpRole")]
        public async Task<IActionResult> GetEmpRole()
        {
            try
            {
                List<EmployeeRole> employees = (await _employeeService.GetEmpRole()).ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all employees role.");
                return Ok(new List<EmployeeDetails>());
            }
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                List<EmployeeRole> employees = (await _employeeService.GetAllRole()).ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all role.");
                return Ok(new List<EmployeeDetails>());
            }
        }
    }
}
