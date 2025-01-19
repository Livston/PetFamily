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
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(Constans.MAX_NAMES_LENGH);

            builder.Property(v => v.LastName)
                .IsRequired()
                .HasMaxLength(Constans.MAX_NAMES_LENGH);

            builder.Property(v => v.SecondName)
                .IsRequired(false)
                .HasMaxLength(Constans.MAX_NAMES_LENGH);

            builder.Property(v => v.Description)
                .IsRequired(false)
                .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property(v => v.ExperienceInYears);

            builder.Property(v => v.TelephoneNumber)
                .IsRequired(false)
                .HasConversion(
                    tn => tn.Number,
                    number => TelephoneNumber.FromString(number))
                .HasMaxLength(TelephoneNumber.MAX_LENGH);

            builder.OwnsOne(v => v.socialNetworksDetails, vb =>
            {
                vb.ToJson();
                vb.OwnsMany(sn => sn.SocialNetworks, snb =>
                {
                    snb.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_NAMES_LENGH);

                    snb.Property(s => s.Description)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);

                });
            });

            builder.OwnsOne(v => v.HelpDetails, vb =>
            {
                vb.ToJson();
                vb.OwnsMany(hd => hd.HelpRequisites, hdb =>
                {
                    hdb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_NAMES_LENGH);

                    hdb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constans.MAX_DESCRIPTIONS_LENGH);

                });
            });

        }
    }
}
