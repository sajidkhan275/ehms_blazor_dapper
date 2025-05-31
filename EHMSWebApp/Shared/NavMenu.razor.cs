using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components.Authorization;

namespace EHMSWebApp.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private List<int>UserRoles { get; set; } = new List<int>();
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            string? entraId = CustomAuthenticationStateProvider.GetEntraId(authState);
            EmployeeWithRoleDetails employeeWithRoleDetails = (await employeeService.GetRoleEmpWiseAsync(entraId));
            UserRoles = employeeWithRoleDetails.EmployeeRoles!.Select(x => x.RoleId).ToList();
        }
    }
}
