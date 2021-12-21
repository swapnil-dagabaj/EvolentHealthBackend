using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthTracker.Core.Model
{
    public class Contact : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        public bool Status { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNo))
                yield return new ValidationResult("Either Phone or Email must be provided", new[] { nameof(PhoneNo), nameof(Email) });
        }
    }
}
