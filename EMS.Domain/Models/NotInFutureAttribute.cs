using System.ComponentModel.DataAnnotations;

namespace EMS.Domain.Models
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        public NotInFutureAttribute() : base("The date cannot be in the future.")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // Let Required attribute handle null validation

            if (value is DateTime dateValue)
            {
                return dateValue <= DateTime.Today;
            }

            return false;
        }
    }
}
