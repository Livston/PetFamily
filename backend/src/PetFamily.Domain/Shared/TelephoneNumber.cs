using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared
{
    public record TelephoneNumber
    {
        public const int MAX_LENGH = 100;
        private TelephoneNumber(string number)
        {
            Number = number;
        }
        public string Number { get; }

        public static Result<TelephoneNumber, Error> Create(string number)
        {
            if (!IsPhoneNumber(number))
            {
                return Errors.General.ValueIsInvalid("Telephone number");
            }

            return new TelephoneNumber(number);
        }

        public static bool IsPhoneNumber(string number)
        {
            //Russia
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        public static TelephoneNumber FromString(string number) => new TelephoneNumber(number);
    }
}
