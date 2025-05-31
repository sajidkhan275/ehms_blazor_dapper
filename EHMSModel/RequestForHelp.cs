using System.ComponentModel.DataAnnotations;

namespace EHMSModel
{
    public class RequestForHelp
    {
        public int RequestForHelpId { get; set; }
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Request details are required.")]
        [StringLength(1000, ErrorMessage = "Request details cannot exceed 1000 characters.")]
        public string? RequestDetails { get; set; }
        public string? Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? RespondedAt { get; set; }
        public string? RespondedStatus { get; set; }
        public string? EmployeeName { get; set; }
    }
}
