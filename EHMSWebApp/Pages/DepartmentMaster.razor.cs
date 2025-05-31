using EHMSModel;
using EHMSWebApp.Interface;
using EHMSWebApp.Services;
using Microsoft.AspNetCore.Components;

namespace EHMSWebApp.Pages
{
    public partial class DepartmentMaster
    {

        private List<DepartmentDetails> departmentDetails = [];
        private List<DepartmentDetails> filtereddepartmentDetails = [];
        private List<DepartmentDetails> pageddepartmentDetails = [];

        private string searchQuery = string.Empty;
        private bool showAddEditDialog = false;
        private bool isEdit = false;
        private DepartmentDetails currentDepartmentDetails = new DepartmentDetails();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages;
        private string sortColumn = "DepartmentName";
        private bool ascending = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadEmployees();
        }

        private async Task LoadEmployees()
        {
            departmentDetails = (await departmentService.GetAllDepartments()).OrderBy(x => x.DepartmentName).ToList();
            FilterEmployees();
            PaginateEmployees();
        }
        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filtereddepartmentDetails = departmentDetails;
            }
            else
            {
                filtereddepartmentDetails = departmentDetails.Where(e => e.DepartmentName!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
        private void PaginateEmployees()
        {
            totalPages = (int)Math.Ceiling((double)filtereddepartmentDetails.Count / pageSize);
            pageddepartmentDetails = filtereddepartmentDetails.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
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
                case "DepartmentName":
                    filtereddepartmentDetails = ascending ? filtereddepartmentDetails.OrderBy(e => e.DepartmentName).ToList() : filtereddepartmentDetails.OrderByDescending(e => e.DepartmentName).ToList();
                    break;
                case "CreatedAt":
                    filtereddepartmentDetails = ascending ? filtereddepartmentDetails.OrderBy(e => e.CreatedAt).ToList() : filtereddepartmentDetails.OrderByDescending(e => e.CreatedAt).ToList();
                    break;
                case "UpdatedAt":
                    filtereddepartmentDetails = ascending ? filtereddepartmentDetails.OrderBy(e => e.UpdatedAt).ToList() : filtereddepartmentDetails.OrderByDescending(e => e.UpdatedAt).ToList();
                    break;
                default:
                    filtereddepartmentDetails = ascending ? filtereddepartmentDetails.OrderBy(e => e.DepartmentName).ToList() : filtereddepartmentDetails.OrderByDescending(e => e.DepartmentName).ToList();
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

        private void OpenAddDialog()
        {
            currentDepartmentDetails = new DepartmentDetails();
            isEdit = false;
            showAddEditDialog = true;
        }

        private void OpenEditDialog(DepartmentDetails department)
        {
            currentDepartmentDetails = department;
            isEdit = true;
            showAddEditDialog = true;
        }

        private async Task SaveDepartment()
        {
            if (isEdit)
            {
                await departmentService.UpdateDepartments(currentDepartmentDetails);
            }
            else
            {
                await departmentService.CreateDepartments(currentDepartmentDetails);
            }


            showAddEditDialog = false;
            await LoadEmployees();
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
            await departmentService.DeleteDepartments(id);
            showDeleteDialog = false;
            deleteRecordId = 0;
            await LoadEmployees();
        }


        private void CloseDialog()
        {
            showAddEditDialog = false;
        }
    }
}
