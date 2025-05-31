using System.ComponentModel.DataAnnotations;

namespace EHMSModel
{
    public class EmployeePhysicalFitness
    {
        public int EmployeePhysicalFitnessId { get; set; }

        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Weight")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Weight")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Please Enter Height")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Height")]
        public double Height { get; set; }
        public double BMI => Math.Round(Weight / (Height * Height), 2);
        public string? EmployeeName { get; set; }
    }
}
