using ClinicWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWebAPI.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>()
                   .HasOne(d => d.Doctor)
                   .WithMany(p => p.Patients)
                   .HasForeignKey(d => d.DoctorId);

            builder.Entity<Ailment>()
                .HasOne(p => p.Patient)
                .WithMany(a => a.Ailments)
                .HasForeignKey(p => p.PatientId);

            builder.Entity<Medication>()
                .HasOne(p => p.Patient)
                .WithMany(a => a.Medications)
                .HasForeignKey(p => p.PatientId);

            builder.Entity<Visit>()
                .HasOne(p => p.Patient)
                .WithMany(a => a.Visits)
                .HasForeignKey(p => p.PatientId);

        }

        public DbSet<Ailment> Ailments { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}