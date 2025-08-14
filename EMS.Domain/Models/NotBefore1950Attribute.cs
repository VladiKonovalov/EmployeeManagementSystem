using System.ComponentModel.DataAnnotations;

namespace EMS.Domain.Models
{
    public class NotBefore1950Attribute : ValidationAttribute
    {
        public NotBefore1950Attribute() : base("The date cannot be before 1950.")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // Let Required attribute handle null validation

            if (value is DateTime dateValue)
            {
                var minDate = new DateTime(1950, 1, 1);
                return dateValue >= minDate;
            }

            return false;
        }
    }
}
