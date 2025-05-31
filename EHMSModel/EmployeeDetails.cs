using System.ComponentModel.DataAnnotations;

namespace EHMSModel
{
    public class EmployeeDetails
    {
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Code")]
        [StringLength(100, ErrorMessage = "Request details cannot exceed 100 characters.")]
        public string? EmployeeCode { get; set; }

        [StringLength(100, ErrorMessage = "Request details cannot exceed 100 characters.")]
        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Please Enter Job Title")]
        [StringLength(100, ErrorMessage = "Request details cannot exceed 100 characters.")]
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? AzureEntraId { get; set; }
        public int? RoleId { get; set; }
    }
}
