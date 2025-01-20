using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constans.MAX_NAMES_LENGH);

            builder.Property(p => p.Description)
                 .IsRequired()
                 .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);
            
            builder.Property(p => p.HealthDescription)
                 .IsRequired(false)
                 .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);
            
            builder.Property(p => p.Color)
                 .IsRequired(false)
                 .HasMaxLength(Pet.MAX_COLOR_LENGH);

            builder.Property(p => p.Weigth);
            
            builder.Property(p => p.Height);

            builder.Property(p => p.OwnerTelephonNumber)
                .IsRequired(false)
                .HasConversion(
                    tn => tn.Number,
                    number => TelephoneNumber.FromString(number))
                .HasMaxLength(TelephoneNumber.MAX_LENGH);

            //adress
            //builder.ComplexProperty(p => p.Adress);
            builder.OwnsOne(p => p.Adress, a =>
            {
                a.ToJson();
                a.Property(a => a.City).IsRequired();
                a.Property(a => a.Street).IsRequired();
                a.Property(a => a.Home).IsRequired();
                a.Property(a => a.Index).IsRequired();
            });

            builder.Property(p => p.Neutered);
            
            builder.Property(p => p.Vaccinated);

            builder.Property(p => p.DateOfBirth);

            builder.Property(p => p.CreationAt);

            builder.Property(p => p.HelpStatus)
                .HasConversion<int>();

            builder.OwnsOne(p => p.HelpDetails, pb =>
            {
                pb.ToJson();
                pb.OwnsMany(hd => hd.HelpRequisites, hdb =>
                {
                    hdb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_NAMES_LENGH);

                    hdb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);

                });
            });

            builder.OwnsOne(p => p.SpeciesBreed, sb =>
            {
                sb.ToJson();
                sb.Property(s => s.SpeciesId).IsRequired();
                sb.Property(s => s.BreedId).IsRequired();
            });
        }
    }
}
