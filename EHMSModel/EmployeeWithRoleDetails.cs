namespace EHMSModel
{
    public class EmployeeWithRoleDetails
    {
        public EmployeeDetails? EmployeeDetails {  get; set; }
        public List<EmployeeRole>? EmployeeRoles { get; set; }
    }
}
