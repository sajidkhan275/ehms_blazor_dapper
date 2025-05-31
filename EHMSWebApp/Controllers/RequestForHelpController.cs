using EHMSModel;
using EHMSWebApp.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHMSWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestForHelpController : ControllerBase
    {
        private readonly IRequestForHelpService _requestForHelpService;
        public RequestForHelpController(IRequestForHelpService requestForHelpService)
        {
            _requestForHelpService = requestForHelpService;
        }
        [HttpPost("CreateRequestForHelpAsync")]
        public async Task<IActionResult> CreateRequestForHelpAsync(RequestForHelp requestForHelpService)
        {
            int res = await _requestForHelpService.CreateRequestForHelpAsync(requestForHelpService);
            return Ok(res);
        }

        [HttpGet("GetRequestsByEmployeeIdAsync")]
        public async Task<IActionResult> GetRequestsByEmployeeIdAsync(int empId)
        {
            List<RequestForHelp> employees = (await _requestForHelpService.GetRequestsByEmployeeIdAsync(empId)).ToList();
            return Ok(employees);
        }

        [HttpGet("GetRequestsByEmployeeAsync")]
        public async Task<IActionResult> GetRequestsByEmployeeAsync()
        {
            List<RequestForHelp> employees = (await _requestForHelpService.GetRequestsByEmployeeAsync()).ToList();
            return Ok(employees);
        }



        [HttpPost("UpdateHRRequestAsync")]
        public async Task<IActionResult> UpdateHRRequestAsync(RequestForHelp requestForHelpService)
        {
            int res = await _requestForHelpService.UpdateHRRequestAsync(requestForHelpService);
            return Ok(res);
        }

        [HttpDelete("DeleteRequestForHelpAsync")]
        public async Task<IActionResult> DeleteRequestForHelpAsync(int requestForHelpId)
        {
            int res = await _requestForHelpService.DeleteRequestForHelpAsync(requestForHelpId);
            return Ok(res);
        }

    }
}
