using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityEmulationService.models
{
    public class ResultJson
    {
        public int new_students_number { get; set; }
        public int expel_students_number { get; set; }
        public int changed_form_of_study { get; set; }
        public int largest_number_of_incoming { get; set; }
        public DateTime compain { get; set; }
        public string currentDate { get { return currentDateDate.ToString(); } set { currentDateDate = Convert.ToDateTime(value); } }
        public DateTime currentDateDate { get; set; }

        public ResultJson()
        {
        }
        public ResultJson(int _new_students_number, int _expel_students_number, int _changed_form_of_study, DateTime _compain, string _currentDate)
        {
            new_students_number = _new_students_number;
            expel_students_number = _expel_students_number;
            changed_form_of_study = _changed_form_of_study;
            compain = _compain;
            currentDate = _currentDate;
        }
    }
}
