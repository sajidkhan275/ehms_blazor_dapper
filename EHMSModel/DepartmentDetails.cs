using System.ComponentModel.DataAnnotations;

namespace EHMSModel
{
    public class DepartmentDetails
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please Enter Department Name")]
        public string? DepartmentName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
