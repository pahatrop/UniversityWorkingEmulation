using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityEmulationService.models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Level { get; set; }
        public int Timezone { get; set; }
        public string StartCompain { get { return StartCompainDate.ToString(); } set { StartCompainDate = Convert.ToDateTime(value); } }
        public string EndCompain { get { return EndCompainDate.ToString(); } set { EndCompainDate = Convert.ToDateTime(value); } }
        public DateTime StartCompainDate { get; set; }
        public DateTime EndCompainDate { get; set; }
        public int EndMonth
        { get; set; }
        public University()
        {
        }
        public University(int id, string name, string address, int level, int timezone, string compainstart, string compainend)
        {
            Id = id;
            Name = name;
            Address = address;
            Level = level;
            Timezone = timezone;
            StartCompain = compainstart;
            EndCompain = compainend;
        }
    }
}
