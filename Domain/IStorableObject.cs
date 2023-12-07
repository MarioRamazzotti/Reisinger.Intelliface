using Microsoft.EntityFrameworkCore;

namespace Reisinger_Intelliface_1_0.Domain;

public interface IStorableObject
{
    public Guid ID { get; set; }
}