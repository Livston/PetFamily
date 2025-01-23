using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "Value";
                return Error.Validation("value.is.invalid", $"{label} is invalid");
            }
            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for id: {id}";
                return Error.NotFound("record.not.found", $"Record not found{forId}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var value = name ?? "Value";
                return Error.Validation("value.is.required", $"{value} is invalid");
            }

            public static Error AlreadyExist()
            {
                return Error.Failure("record.already.exist", $"Record exist");
            }
        }

    }
}
