using Microsoft.Build.Framework;
using Reisinger_Intelliface_1_0.Domain;

namespace Reisinger_Intelliface_1_0.Model.DTOs
{
    public class AddEmployeeForm
    {
        [Required]
        public int EmployeeNumber { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public DateTimeOffset? DateOfBirth { get; set; }
        [Required]
        public string? Nationality { get; set; }
        [Required]
        public string? Notes { get; set; }

    }
}
