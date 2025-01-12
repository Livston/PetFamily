using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            //test

            string name = "Bony";
            string description = "Bony is not agressive";

            var PetResult = Pet.Create(Guid.NewGuid(), name, description);

            if (PetResult.IsFailure)
            {
                return ValidationProblem(PetResult.Error);
            }

            Pet pet = PetResult.Value;

            //
            var result = TelephoneNumber.Create("");
            if (result.IsFailure)
            {
                Console.WriteLine(result.Error);

            }

            var result2 = Volunteer.Create(Guid.NewGuid(), "Edgard", "Zap");

            string fio = result2.Value.FIO;

            Volunteer volunteer = result2.Value;
            volunteer.AddPet(pet);

            int petsNeedHelp = volunteer.PetsNeedHelpCount;

            return Ok();

        }
    }
}
