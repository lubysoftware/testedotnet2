using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public interface IIdentityEntity
    {
        Guid Id { get; set; }

        [Required]
        DateTime CreatedAt { get; set; }

        [Required]
        DateTime UpdatedAt { get; set; }
    }
}
