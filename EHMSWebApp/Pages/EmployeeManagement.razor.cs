using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components;
using System.Data;

namespace EHMSWebApp.Pages
{
    public partial class EmployeeManagement
    {
        private List<EmployeeDetails> employees = [];
        private List<EmployeeDetails> filteredEmployees = [];
        private List<EmployeeDetails> pagedEmployees = [];
        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private EmployeeDetails currentEmployee = new EmployeeDetails();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "EmployeeCode";
        private bool ascending = true;
        private List<int> UserRoles { get; set; } = new List<int>();
        private List<DepartmentDetails> departmentDetails = [];
        protected override async Task OnInitializedAsync()
        {
            await LoadEmployees();
        }

        private async Task LoadEmployees()
        {

            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            string? entraId = CustomAuthenticationStateProvider.GetEntraId(authState);

            //var response = await httpClient.GetAsync("api/employee/GetRoleEmpWise?entraId=" + entraId);
            EmployeeWithRoleDetails employeeWithRoleDetails = (await employeeService.GetRoleEmpWiseAsync(entraId));
            UserRoles = employeeWithRoleDetails.EmployeeRoles!.Select(x => x.RoleId).ToList();

            employees = (await employeeService.GetAllEmployeesAsync()).OrderBy(x => x.EmployeeCode).ToList();
            departmentDetails = (await departmentService.GetAllDepartments()).OrderBy(x => x.DepartmentName).ToList();
            FilterEmployees();
            PaginateEmployees();
        }
        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredEmployees = employees;
            }
            else
            {
                filteredEmployees = employees.Where(e => e.EmployeeName!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.EmployeeCode!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private void PaginateEmployees()
        {
            totalPages = (int)Math.Ceiling((double)filteredEmployees.Count / pageSize);
            pagedEmployees = filteredEmployees.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
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
                case "EmployeeCode":
                    filteredEmployees = ascending ? filteredEmployees.OrderBy(e => e.EmployeeCode).ToList() : filteredEmployees.OrderByDescending(e => e.EmployeeCode).ToList();
                    break;
                case "EmployeeName":
                    filteredEmployees = ascending ? filteredEmployees.OrderBy(e => e.EmployeeName).ToList() : filteredEmployees.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                case "DepartmentName":
                    filteredEmployees = ascending ? filteredEmployees.OrderBy(e => e.DepartmentName).ToList() : filteredEmployees.OrderByDescending(e => e.DepartmentName).ToList();
                    break;
                case "JobTitle":
                    filteredEmployees = ascending ? filteredEmployees.OrderBy(e => e.JobTitle).ToList() : filteredEmployees.OrderByDescending(e => e.JobTitle).ToList();
                    break;
                default:
                    filteredEmployees = ascending ? filteredEmployees.OrderBy(e => e.EmployeeCode).ToList() : filteredEmployees.OrderByDescending(e => e.EmployeeCode).ToList();
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

        private void OpenEditDialog(EmployeeDetails employee)
        {
            currentEmployee = employee;
            isEdit = true;
            showAddEditDialog = true;
        }

        private async Task SaveEmployee()
        {
            if (isEdit)
            {
                _logger.LogError("employee details save");
                await employeeService.UpdateEmployeeAsync(currentEmployee);
            }
            else
            {
                await employeeService.CreateEmployeeAsync(currentEmployee);
            }

            showAddEditDialog = false;
            await LoadEmployees();
        }

        private void CloseDialog()
        {
            showAddEditDialog = false;
            currentEmployee  = new EmployeeDetails();
        }

        private bool showDeleteDialog = false;
        private int deleteRecordId;
        private void ShowDeleteDialog(int id)
        {
            showDeleteDialog = true;
            deleteRecordId = id;
        }
        private void CloseDeleteDialog()
        {
            showDeleteDialog = false;
            deleteRecordId = 0;
        }

        private async Task HandleDeleteConfirmed(int id)
        {
            await employeeService.DeleteEmployeeAsync(id);
            showDeleteDialog = false;
            deleteRecordId = 0;
            await LoadEmployees();
        }
    }
}
