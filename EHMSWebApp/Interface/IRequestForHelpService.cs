using EHMSModel;

namespace EHMSWebApp.Interface
{
    public interface IRequestForHelpService
    {
        Task<int> CreateRequestForHelpAsync(RequestForHelp request);
        Task<IEnumerable<RequestForHelp>> GetRequestsByEmployeeIdAsync(int empId);
        Task<int> UpdateHRRequestAsync(RequestForHelp request);
        Task<IEnumerable<RequestForHelp>> GetRequestsByEmployeeAsync();
        Task<int> DeleteRequestForHelpAsync(int RequestForHelpId);
    }
}
