using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components;

namespace EHMSWebApp.Pages
{
    public partial class RequestForEmployee
    {
        private List<RequestForHelp> requestForHelp = [];
        private List<RequestForHelp> filteredrequestForHelp = [];
        private List<RequestForHelp> pagedrequestForHelp = [];
        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private RequestForHelp currentrequestForHelp = new RequestForHelp();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "RequestForHelpId";
        private bool ascending = true;

        private List<int> UserRoles { get; set; } = new List<int>();
        private int EmpId;
        EmployeeWithRoleDetails employeeWithRoleDetails = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDetails();
        }

        private async Task LoadDetails()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            string? entraId = CustomAuthenticationStateProvider.GetEntraId(authState);
            employeeWithRoleDetails = (await employeeService.GetRoleEmpWiseAsync(entraId));
            UserRoles = employeeWithRoleDetails.EmployeeRoles!.Select(x => x.RoleId).ToList();
            EmpId = employeeWithRoleDetails.EmployeeDetails!.EmpId;
            if (UserRoles.Any(role => role == (int)UserRole.HR || role == (int)UserRole.Admin))
            {
                requestForHelp = (await requestForHelpService.GetRequestsByEmployeeAsync()).ToList();
            }
            else
            {
                requestForHelp = (await requestForHelpService.GetRequestsByEmployeeIdAsync(EmpId)).ToList();
            }
            FilterData();
            PaginateData();
        }

        private void FilterData()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredrequestForHelp = requestForHelp;
            }
            else
            {
                filteredrequestForHelp = requestForHelp.Where(e => e.RequestDetails!.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.EmployeeName!.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.Status!.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
        private void PaginateData()
        {
            totalPages = (int)Math.Ceiling((double)filteredrequestForHelp.Count / pageSize);
            pagedrequestForHelp = filteredrequestForHelp.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private void OnSearchChange(ChangeEventArgs e)
        {
            searchQuery = e.Value?.ToString() ?? string.Empty;
            currentPage = 1;
            FilterData();
            PaginateData();
        }

        private void Sort(string column)
        {
            if (sortColumn == column)
            {
                ascending = !ascending;
            }
            else
            {
                sortColumn = column;
                ascending = true;
            }

            switch (sortColumn)
            {
                case "EmployeeName":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.EmployeeName).ToList() : filteredrequestForHelp.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                case "RequestDetails":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.RequestDetails).ToList() : filteredrequestForHelp.OrderByDescending(e => e.RequestDetails).ToList();
                    break;
                case "Status":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.Status).ToList() : filteredrequestForHelp.OrderByDescending(e => e.Status).ToList();
                    break;
                case "CreatedAt":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.CreatedAt).ToList() : filteredrequestForHelp.OrderByDescending(e => e.CreatedAt).ToList();
                    break;
                case "RespondedStatus":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.RespondedStatus).ToList() : filteredrequestForHelp.OrderByDescending(e => e.RespondedStatus).ToList();
                    break;
                case "RespondedAt":
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.RespondedAt).ToList() : filteredrequestForHelp.OrderByDescending(e => e.RespondedAt).ToList();
                    break;
                default:
                    filteredrequestForHelp = ascending ? filteredrequestForHelp.OrderBy(e => e.RequestForHelpId).ToList() : filteredrequestForHelp.OrderByDescending(e => e.RequestForHelpId).ToList();
                    break;
            }
            PaginateData();
        }

        private string GetSortIcon(string column)
        {
            if (sortColumn != column)
            {
                return ""; // No icon for unselected column
            }

            return ascending ? "↑" : "↓"; // Return arrow based on sort direction
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                PaginateData();
            }
        }
        private void PrevPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                PaginateData();
            }
        }
        private void OpenAddDialog()
        {
            currentrequestForHelp = new RequestForHelp();
            isEdit = false;
            showAddEditDialog = true;
        }

        private void OpenEditDialog(RequestForHelp requestForHelp)
        {
            currentrequestForHelp = requestForHelp;
            isEdit = true;
            showAddEditDialog = true;
        }

        private async Task SavePhysicalFitness()
        {
            if (isEdit)
            {
                currentrequestForHelp.RespondedAt = DateTime.Now;
                await requestForHelpService.UpdateHRRequestAsync(currentrequestForHelp);
            }
            else
            {
                currentrequestForHelp.Status = "Pending";
                currentrequestForHelp.EmpId = EmpId;
                await requestForHelpService.CreateRequestForHelpAsync(currentrequestForHelp);
            }

            showAddEditDialog = false;
            await LoadDetails();
        }

        private void CloseDialog()
        {
            showAddEditDialog = false;
        }

        private async Task DeletePhysicalFitness(int id)
        {
            await requestForHelpService.DeleteRequestForHelpAsync(id);
            await LoadDetails();
        }

    }
}
