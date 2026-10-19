using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(7, 150, ErrorMessage = "You should have a name with a minimum value of 7 and a maximum of 150")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1,90)]
        [Display(Name="Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
        
    }
}
