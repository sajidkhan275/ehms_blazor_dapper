using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components;

namespace EHMSWebApp.Pages
{
    public partial class PhysicalFitnessManagement
    {
        private List<EmployeePhysicalFitness> physicalFitness = [];
        private List<EmployeePhysicalFitness> filteredphysicalFitness = [];
        private List<EmployeePhysicalFitness> pagedphysicalFitness = [];
        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private EmployeePhysicalFitness currentphysicalFitness = new EmployeePhysicalFitness();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "EmployeeName";
        private bool ascending = true;
        private List<int> UserRoles { get; set; } = new List<int>();
        private int EmpId;
        EmployeeWithRoleDetails employeeWithRoleDetails = new();
        private bool physicalFitnessExist { get; set; }

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

            if (UserRoles.Any(role => role == (int)UserRole.Admin || role == (int)UserRole.HR))
            {
                physicalFitness = (await physicalFitnessService.GetAllEmployeePhysicalFitness()).ToList();
            }
            else
            {
                physicalFitness = (await physicalFitnessService.GetAllEmployeePhysicalFitnessByEmpId(EmpId)).ToList();
            }
          

            physicalFitnessExist = physicalFitness.Any(pf => pf.EmpId == EmpId);
            FilterData();
            PaginateData();
        }

        private void FilterData()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredphysicalFitness = physicalFitness;
            }
            else
            {
                filteredphysicalFitness = physicalFitness.Where(e => e.EmployeeName!.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.Weight.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.Height.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                                          e.BMI.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private void PaginateData()
        {
            totalPages = (int)Math.Ceiling((double)filteredphysicalFitness.Count / pageSize);
            pagedphysicalFitness = filteredphysicalFitness.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private void OnSearchChange(ChangeEventArgs e)
        {
            searchQuery = e.Value?.ToString() ?? string.Empty;
            currentPage = 1;
            FilterData();
            PaginateData();
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
                    filteredphysicalFitness = ascending ? filteredphysicalFitness.OrderBy(e => e.EmployeeName).ToList() : filteredphysicalFitness.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                case "Height":
                    filteredphysicalFitness = ascending ? filteredphysicalFitness.OrderBy(e => e.Height).ToList() : filteredphysicalFitness.OrderByDescending(e => e.Height).ToList();
                    break;
                case "Weight":
                    filteredphysicalFitness = ascending ? filteredphysicalFitness.OrderBy(e => e.Weight).ToList() : filteredphysicalFitness.OrderByDescending(e => e.Weight).ToList();
                    break;
                case "BMI":
                    filteredphysicalFitness = ascending ? filteredphysicalFitness.OrderBy(e => e.BMI).ToList() : filteredphysicalFitness.OrderByDescending(e => e.BMI).ToList();
                    break;
                default:
                    filteredphysicalFitness = ascending ? filteredphysicalFitness.OrderBy(e => e.EmployeeName).ToList() : filteredphysicalFitness.OrderByDescending(e => e.EmployeeName).ToList();
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
            currentphysicalFitness = new EmployeePhysicalFitness();
            isEdit = false;
            showAddEditDialog = true;
        }

        private void OpenEditDialog(EmployeePhysicalFitness physicalFitness)
        {
            currentphysicalFitness = physicalFitness;
            isEdit = true;
            showAddEditDialog = true;
        }

        private async Task SavePhysicalFitness()
        {
            currentphysicalFitness.EmpId = EmpId;
            if (isEdit)
            {
                await physicalFitnessService.UpdateEmployeePhysicalFitnessAsync(currentphysicalFitness);
            }
            else
            {
                await physicalFitnessService.CreateEmployeePhysicalFitnessAsync(currentphysicalFitness);
            }
            showAddEditDialog = false;
            await LoadDetails();
        }

        private void CloseDialog()
        {
            showAddEditDialog = false;
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
            await physicalFitnessService.DeleteEmployeePhysicalFitnessAsync(id);
            showDeleteDialog = false;
            deleteRecordId = 0;
            await LoadDetails();
        }
    }
}
