using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityEmulationService.models
{
    public class Timezone
    {
        public int Id { get; set; }
        public string Tz { get; set; }
        public string Long_variant { get; set; } 
        
        public Timezone()
        {
        }
        public Timezone(int id, string tz, string long_variant)
        {
            Id = id;
            Tz = tz;
            Long_variant = long_variant;
        }
    }
}
