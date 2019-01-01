using ClinicWebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWebAPI.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ClinicContext>();
                context.Database.EnsureCreated();

                if (context.Ailments != null && context.Ailments.Any())
                    return; // DB has already been seeded

                var ailments = GetAilments().ToArray();
                context.Ailments.AddRange(ailments);
                context.SaveChanges();

                var medications = GetMedications().ToArray();
                context.Medications.AddRange(medications);
                context.SaveChanges();

                var visits = GetVisits().ToArray();
                context.Visits.AddRange(visits);
                context.SaveChanges();

                var doctors = GetDoctors().ToArray();
                context.Doctors.AddRange(doctors);
                context.SaveChanges();

                var patients = GetPatients(context).ToArray();
                context.Patients.AddRange(patients);
                context.SaveChanges();

            }
        }

        public static List<Ailment> GetAilments()
        {
            List<Ailment> ailments = new List<Ailment>
            {
                new Ailment {Description = "Ból głowy"},
                new Ailment {Description = "Angina"},
                new Ailment {Description = "Grypa"},
                new Ailment {Description = "Lekki kaszel"},
            };
            return ailments;
        }

        public static List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>
            {
                new Doctor {Name = "Jan Nowak"},
                new Doctor {Name = "Andrzej Kowalski"},
                new Doctor {Name = "Piotr Pawlak"},
                new Doctor {Name = "Adam Janicki"},
            };
            return doctors;
        }

        public static List<Medication> GetMedications()
        {
            List<Medication> medications = new List<Medication>
            {
                new Medication { Name = "Tylenol", Doses = "2"},
                new Medication { Name = "Aspirin", Doses = "4"},
                new Medication { Name = "Advil", Doses = "3"},
                new Medication { Name = "Robaxin", Doses = "2"},
                new Medication { Name = "Voltaren", Doses = "1"},
            };
            return medications;
        }

        public static List<Visit> GetVisits()
        {
            List<Visit> visits = new List<Visit>
            {
                new Visit { VisitTime = new DateTime(2018, 1, 18), Description = "Wizyta kontrola" },
                new Visit { VisitTime = new DateTime(2017, 5, 15), Description = "Problemy z snem" },
                new Visit { VisitTime = new DateTime(2016, 3, 1), Description = "Wysoka temperatura" },
                new Visit { VisitTime = new DateTime(2015, 12, 21), Description = "Brak dolegliwości" },
            };
            return visits;
        }

        public static List<Patient> GetPatients(ClinicContext db)
        {
            List<Patient> patients = new List<Patient>
            {
                new Patient
                {
                    Name = "Szymon Szymczak",
                    Ailments = new List<Ailment>(db.Ailments.Take(2)),
                    Medications = new List<Medication>(db.Medications.Take(2)),
                    Visits = new List<Visit>(db.Visits.Take(2)),
                    Doctor = new Doctor { Id = 1, Name = "Jan Nowak" }
                },
                new Patient
                {
                    Name = "Anna Nowak",
                    Ailments = new List<Ailment>(db.Ailments.Take(1)),
                    Medications = new List<Medication>(db.Medications.Skip(1).Take(2)),
                    Visits = new List<Visit>(db.Visits.Take(1)),
                    Doctor = new Doctor { Id = 2, Name = "Andrzej Kowalski" }
                },
                new Patient
                {
                    Name = "Karolina Olejniczak",
                    Ailments = new List<Ailment>(db.Ailments.Skip(3).Take(1)),
                    Medications = new List<Medication>(db.Medications.Skip(2).Take(1)),
                    Visits = new List<Visit>(db.Visits.Skip(2).Take(2)),
                    Doctor = new Doctor { Id = 4, Name = "Adam Janicki" }
                }
            };
            return patients;
        }
    }
}
