using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EHMSWebApp.Utility
{
    public static class CustomAuthenticationStateProvider 
    {
        public static string GetUserEmail(AuthenticationState authState)
        {
            return authState.User.Identity?.Name ?? string.Empty;
        }

        public static string GetEntraId(AuthenticationState authState)
        {
            return authState.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? string.Empty;
        }

        public static string GetUserName(AuthenticationState authState)
        {
            return authState.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? string.Empty;
        }

        public static List<string> GetUserRoles(AuthenticationState authState)
        {
            return authState.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }
       
    }
}
