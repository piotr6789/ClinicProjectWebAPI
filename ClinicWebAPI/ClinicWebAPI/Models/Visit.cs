using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWebAPI.Models
{
    public class Visit
    {
        [Key]
        public DateTime VisitTime { get; set; }
        public string Description { get; set; }
    }
}
