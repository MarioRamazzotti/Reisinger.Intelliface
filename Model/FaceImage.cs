using Microsoft.EntityFrameworkCore;
using Reisinger_Intelliface_1_0.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reisinger_Intelliface_1_0.Model;

public class FaceImage : IStorableObject
{

    public Guid ID { get; set; }

    public string? ImageData { get; set; }

    public User? Employee { get; set; }


    public Guid EmployeeId { get; set; }


    public FaceImage(string imageData, Guid employeeId)
    {
        ImageData = imageData;
        EmployeeId = employeeId;
    }


    public FaceImage()
    {

    }




}