using PetFamily.Application.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.CreateVolunteer;
using FluentValidation;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateHelpDetails;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.MarkDeleted;
using PetFamily.Application.Providers;
using PetFamily.Application.Files.Upload;
using PetFamily.Application.Files.DeleteFile;
using PetFamily.Application.Files.GetFile;

namespace PetFamily.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateHelpDetailsHandler>();
            services.AddScoped<MarkDeletedVolunteerHandler>();
            services.AddScoped<DeleteVolunteerHandler>();
            
            services.AddScoped<UploadFileHandler>();
            services.AddScoped<DeleteFileHandler>();
            services.AddScoped<GetFileHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
