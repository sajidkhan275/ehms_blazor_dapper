using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;

namespace EHMSWebApp.Pages
{
    public partial class EmpRole
    {
        private List<EmployeeRole> empRole = [];
        private List<EmployeeRole> filteredempRole = [];
        private List<EmployeeRole> pagedempRole = [];

        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private EmployeeRole currentEmployeeRole = new EmployeeRole();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "EmployeeName";
        private bool ascending = true;

        private List<EmployeeRole> allRole = [];
        private List<string> SelectedRoles = [];
        private AuthenticationState? authenticationState;
        protected override async Task OnInitializedAsync()
        {
            authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            await LoadEmployees();
        }

        private async Task LoadEmployees()
        {
            allRole = (await employeeService.GetAllRole()).ToList();
            empRole = (await employeeService.GetEmpRole()).OrderBy(x => x.EmployeeName).ToList();
            FilterEmployees();
            PaginateEmployees();
        }
        private void OnSelectionChanged(ChangeEventArgs e)
        {
            var selectedValues = ((IEnumerable<string>)e.Value!).ToList();
            SelectedRoles = selectedValues;
        }
        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredempRole = empRole;
            }
            else
            {
                filteredempRole = empRole.Where(e => e.EmployeeName!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.Name!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
        private void PaginateEmployees()
        {
            totalPages = (int)Math.Ceiling((double)filteredempRole.Count / pageSize);
            pagedempRole = filteredempRole.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private void OnSearchChange(ChangeEventArgs e)
        {
            searchQuery = e.Value?.ToString() ?? string.Empty;
            currentPage = 1;
            FilterEmployees();
            PaginateEmployees();
        }

        private void SortEmployees(string column)
        {
            if (sortColumn == column)
            {
                ascending = !ascending; // Toggle sort order if the same column is clicked
            }
            else
            {
                sortColumn = column;
                ascending = true; // Default to ascending if a new column is clicked
            }

            switch (sortColumn)
            {
                case "EmployeeName":
                    filteredempRole = ascending ? filteredempRole.OrderBy(e => e.EmployeeName).ToList() : filteredempRole.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                case "Name":
                    filteredempRole = ascending ? filteredempRole.OrderBy(e => e.Name).ToList() : filteredempRole.OrderByDescending(e => e.Name).ToList();
                    break;
                default:
                    filteredempRole = ascending ? filteredempRole.OrderBy(e => e.EmployeeName).ToList() : filteredempRole.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
            }

            PaginateEmployees();
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
                PaginateEmployees();
            }
        }
        private void PrevPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                PaginateEmployees();
            }
        }

        private void OpenEditDialog(EmployeeRole empRole)
        {
            currentEmployeeRole = empRole;
            isEdit = true;
            showAddEditDialog = true;
        }

        private async Task SaveEmployee()
        {
          
            List<int> Roles1 = SelectedRoles.Select(x => int.Parse(x.Trim())).ToList();
         

            if (Roles1.Count > 0)
            {
                var roles = CustomAuthenticationStateProvider.GetUserRoles(authenticationState)[0];
                var uname = CustomAuthenticationStateProvider.GetUserName(authenticationState);
                if (int.Parse(roles) == (int)UserRole.Admin && !Roles1.Contains((int)UserRole.Admin) && currentEmployeeRole.EmployeeName == uname)
                {
                    Roles1.Add(1);
                }
                await employeeService.DeletRoleEmpWiseAsync(currentEmployeeRole);
            }
            foreach (int role1 in Roles1)
            {
                EmployeeRole employeeRole = currentEmployeeRole;
                employeeRole.RoleId = role1;
                await employeeService.AddRoleAsync(employeeRole);
            }
            showAddEditDialog = false;
            await LoadEmployees();
            StateHasChanged();
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }

        private void CloseDialog()
        {
            showAddEditDialog = false;
        }

    }
}
