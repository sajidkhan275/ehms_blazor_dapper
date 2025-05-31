namespace EHMSWebApp.Shared
{
    public partial class MainLayout
    {
        private void Login()
        {
            navigation.NavigateTo("account/login", forceLoad: true);
        }

        private void Logout()
        {
            navigation.NavigateTo("/account/logout", forceLoad: true);
        }
    }
}
