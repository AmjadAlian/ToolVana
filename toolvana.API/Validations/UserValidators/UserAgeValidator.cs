using System.ComponentModel.DataAnnotations;

namespace toolvana.API.Validations.GenderValidator
{
    public class UserAgeValidator : ValidationAttribute
    {
        private readonly int MinimumAge;

        public UserAgeValidator (int minimumAge)
        {
            MinimumAge = minimumAge;
        }
        public override bool IsValid(object? value)
        {
           if(value is DateTime dateTime)
            {
              
                if (DateTime.Now.Year - dateTime.Year >= MinimumAge)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                return false;
            }
        }
    }
}
