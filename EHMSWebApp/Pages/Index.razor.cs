using EHMSModel;
using EHMSWebApp.Utility;
using Microsoft.AspNetCore.Components.Authorization;

namespace EHMSWebApp.Pages
{
    public partial class Index
    {
        public string? sname { get; set; }
        AuthenticationState? authenticationState;
        protected override async Task OnInitializedAsync()
        {
            authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            sname = CustomAuthenticationStateProvider.GetUserName(authenticationState);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                EmployeeDetails emp = new EmployeeDetails();
                emp.Email = CustomAuthenticationStateProvider.GetUserEmail(authenticationState!);
                emp.EmployeeName = CustomAuthenticationStateProvider.GetUserName(authenticationState!);
                emp.AzureEntraId = CustomAuthenticationStateProvider.GetEntraId(authenticationState!);

                var roles = CustomAuthenticationStateProvider.GetUserRoles(authenticationState!);
                if (roles.Count == 0)
                {
                    navigation.NavigateTo("/account/logout", forceLoad: true);
                }
                emp.RoleId = Convert.ToInt32(roles[0]);
                await employeeService.CreateEmployeeAsync(emp);
            }
        }
    }
}
