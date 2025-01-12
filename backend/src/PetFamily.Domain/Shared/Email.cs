using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public record Email
    {
        private Email(string emailAdress)
        {
            EmailAdress = emailAdress;
        }
        public string EmailAdress { get; }

        public static Result<Email, string> Create(string emailAdress)
        {
            if (!IsEmailAdress(emailAdress))
            {
                return "emailAdress is invalid";
            }

            return new Email(emailAdress);
        }

        public static bool IsEmailAdress(string emailAdress)
        {
            return Regex.Match(emailAdress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }
    }
}
