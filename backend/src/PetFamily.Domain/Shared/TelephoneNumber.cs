using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared
{
    public record TelephoneNumber
    {
        private TelephoneNumber(string number)
        {
            Number = number;
        }
        public string Number { get; }

        public static Result<TelephoneNumber, string> Create(string number)
        {
            if (!IsPhoneNumber(number))
            {
                return "Telephone number is invalid";
            }

            return new TelephoneNumber(number);
        }

        public static bool IsPhoneNumber(string number)
        {
            //Russia
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }
    }
}
