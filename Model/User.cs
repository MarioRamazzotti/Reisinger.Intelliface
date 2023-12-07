using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reisinger_Intelliface_1_0.Domain;

namespace Reisinger_Intelliface_1_0.Model;

public class User : IStorableObject
{

    public Guid ID { get; set; }

    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Email { get; set; }
    [Required]
    public string Gender { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTimeOffset DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string? Notes { get; set; }

    public List<FaceImage> Images { get; set; }

    public bool IsTeached { get; set; }


    public string? Collections { get; set; }

    public bool? IsBulkInsert { get; set; }

    public User()
    {
        ID = Guid.NewGuid();
        Images = new List<FaceImage>();

    }


}