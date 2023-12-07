using System.ComponentModel.DataAnnotations;

namespace Reisinger_Intelliface_1_0.Model.ViewModels
{
    public class UserFrontViewModel
    {

        public Guid Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string? Notes { get; set; }


        public List<string> ImagesAsBase64 { get; set; } = new List<string>();

        public string? Collections { get; set; }

        public bool? IsBulkInsert { get; set; }

    }
}
