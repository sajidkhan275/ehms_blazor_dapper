using System.ComponentModel.DataAnnotations;

namespace EHMSModel
{
    public class EmployeeHealthInfo
    {
        public int EmployeeHealthInfoId { get; set; }
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Blood Group")]
        public string? BloodGroup { get; set; }

        public string? MedicalReportFileName { get; set; }

        public string? RecentMedicalReportPath { get; set; }
        public bool Disability { get; set; }

        public string? EmployeeName { get; set; }
    }
}
