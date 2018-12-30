using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWebAPI.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}
