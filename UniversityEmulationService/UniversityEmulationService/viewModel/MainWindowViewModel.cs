using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityEmulationService.models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UniversityEmulationService.viewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private List<University> _universities;
        private ResultJson _resultJson;
        private List<string> _universityVariant;
        private List<string> _monthVariant;
        private string _currentUniversityName;
        private string _currentDate;
        private string _currentDateSTZ;
        private int _numberAddedStudents;
        private int _numberExpeltStudents;
        private int _largestNumberOfIncoming;
        private string _largestNumberOfEntrants;
        private string _serverStatus;
        private University _currentUniversity;
        private int _simulationDay = 1;
        private int _simulationMonth = 1;
        private int _simulationStartYear = 2012;
        private int _simulationEndYear = 2016;
        private DateTime _simulationStart;
        private DateTime _simulationEnd;
        private DateTime _startEntrance;
        private DateTime _endEntrance;
        private bool _isRunning = false;
        private float _progress = 0;
        private static string Host = "http://192.168.2.74:8888";

        // ICommand
        private ICommand _startButton;
        private ICommand _stopButton;

        // Binding fields
        public string BindUniversityName { get { return _currentUniversityName; } }
        public string ServerStatus { get { return _serverStatus; } }
        public List<string> BindUniversityVariant { get { return _universityVariant; } }
        public List<string> BindMonthVariant { get { return _monthVariant; } }
        public int BindStartYear { get { return _simulationStartYear; } set { _simulationStartYear = value; } }
        public int BindEndYear { get { return _simulationEndYear; } set { _simulationEndYear = value; } }
        public string BindUniversitySelected
        {
            set
            {
                for (int i = 0; i < _universities.Count; i++)
                {
                    if (value == _universities[i].Name)
                    {
                        _currentUniversity = _universities[i];
                        _startEntrance = _universities[i].StartCompainDate;
                        _endEntrance = _universities[i].EndCompainDate;
                    }
                }
            }
        }
        public string BindMonthSelected
        {
            set
            {
                for (int i = 0; i < _monthVariant.Count; i++)
                {
                    if (value == _monthVariant[i])
                    {
                        _simulationMonth = i;
                    }
                }
            }
        }
        public string BindCurrentDate { get { return _currentDate; } }
        public string BindCurrentDateSTZ { get { return _currentDateSTZ; } }
        public int BindNumberAddedStudents { get { return _numberAddedStudents; } }
        public int BindNumberExpeltStudents { get { return _numberExpeltStudents; } }
        public int BindLargestNumberOfIncoming { get { return _largestNumberOfIncoming; } }
        public string BindLargestNumberOfEntrants { get { return _largestNumberOfEntrants; } }
        public float BindProgress { get { return _progress; } }

        public MainWindowViewModel()
        {
            _serverStatus = "Waiting for the connection to the server...";
            _monthVariant = new List<string>();
            _monthVariant.Add("January");
            _monthVariant.Add("February");
            _monthVariant.Add("March");
            _monthVariant.Add("April");
            _monthVariant.Add("May");
            _monthVariant.Add("June");
            _monthVariant.Add("July");
            _monthVariant.Add("August");
            _monthVariant.Add("September");
            _monthVariant.Add("October");
            _monthVariant.Add("November");
            _monthVariant.Add("December");
            GetAllUniversities();
        }

        private async Task GetAllUniversities()
        {
            await Task.Run(() =>
            {
                new Rest().GetAllUniversities((List<University> universities) =>
                {
                    _universities = universities;
                    _universityVariant = new List<string>();
                    if (universities == null)
                    {
                        _serverStatus = "The server is not responding!";
                    }
                    else
                    {
                        foreach (University univer in universities)
                        {
                            _universityVariant.Add(univer.Name);
                        }
                        _serverStatus = "Connected to server";
                    }
                    RaisePropertyChanged("ServerStatus");
                    RaisePropertyChanged("BindUniversityVariant");
                },
                Host + "/api/get?request=University");
            });
        }

        private async Task Emulation()
        {
            _currentUniversityName = _currentUniversity.Name;
            int year = _simulationStartYear;
            bool semaf = true;
            float percent = 100 / (_simulationEndYear - _simulationStartYear);
            float percent2 = percent / 12;
            await Task.Run(() =>
            {
                if (true) // int year = _simulationStartYear; year <= _simulationEndYear; year++
                {
                    for (DateTime date = new DateTime(year, 7, 1, 13, 1, 1); date < new DateTime(_simulationEndYear, 6, 1); date = date.AddMonths(1))
                    {
                        do
                        {
                            semaf = false;
                            StartEmulation((bool resp) =>
                            {
                                semaf = resp;
                                Console.WriteLine(resp);
                            }, date);
                        }
                        while (semaf);
                        System.Threading.Thread.Sleep(2000);
                        if (_isRunning == false) break;
                        _progress += percent2;
                        RaisePropertyChanged("BindProgress");
                        if (_isRunning == false)
                        {
                            break;
                        }
                    }
                    if (_isRunning == false)
                    {
                        MessageBox.Show("Emulation is stoped!");
                    }
                    else
                    {
                        MessageBox.Show("Emulation is successful finished!");
                    }
                }
                _progress = 0;
                RaisePropertyChanged("BindProgress");
            });
        }

        public DateTime ConvertFromUtc(DateTime sourceDateTime, string targetZoneInfo)
        {
            /*return sourceDateTime.Kind == DateTimeKind.Local
                ? TimeZoneInfo.ConvertTime(sourceDateTime, TimeZoneInfo.Utc, targetZoneInfo)
                : TimeZoneInfo.ConvertTime(new DateTime(sourceDateTime.Ticks, DateTimeKind.Local), targetZoneInfo);*/
            DateTime tmp = new DateTime(sourceDateTime.Ticks,DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(tmp, targetZoneInfo);

        }

        public async Task StartEmulation(Action<bool> action, DateTime date)
        {
            DateTime timeUtc = DateTime.UtcNow;
            _simulationStart = date;
            _simulationEnd = _simulationStart.AddMonths(1);
            try
            {
                await Task.Run(() =>
                {
                    new Rest().GetRequest((ResultJson result) =>
                    {
                        bool ret;

                        Timezone _tz = new Rest().GetTimeZone(Host + "/api/tz?id=" + _currentUniversity.Timezone); // info about timezone
                        
                        DateTime _tz_university = ConvertFromUtc(result.currentDateDate, _tz.Long_variant);
                        DateTime _now = ConvertFromUtc(result.currentDateDate, TimeZoneInfo.Local.Id);
                        
                        _resultJson = result;
                        _currentDate = _tz_university.ToString("dd-MM-yyyy HH:mm"); // to local time zone
                        _currentDateSTZ = _now.ToString("dd-MM-yyyy HH:mm"); // to university time zone

                        //Console.WriteLine(TimeZoneInfo.ConvertTimeFromUtc(new DateTime(result.currentDateDate.Ticks, DateTimeKind.Utc), TimeZoneInfo.Local));

                        _numberAddedStudents = result.new_students_number;
                        _numberExpeltStudents = result.expel_students_number;
                        _largestNumberOfIncoming = result.largest_number_of_incoming;
                        //_largestNumberOfEntrants = new DateAndTimeConvert().ConvertationInLocalTimeZone(_currentUniversity.Timezone, result.compain).ToString()+" hours   "+result.largest_number_of_entrants.day+"."+ result.largest_number_of_entrants.month;
                        
                        if (result == null)
                        {
                            ret = false;
                        }
                        else
                        {
                            ret = true;
                        }
                        action(ret);
                        RaisePropertyChanged("BindCurrentDate");
                        RaisePropertyChanged("BindCurrentDateSTZ");
                        RaisePropertyChanged("BindNumberAddedStudents");
                        RaisePropertyChanged("BindNumberExpeltStudents");
                        RaisePropertyChanged("BindLargestNumberOfIncoming");
                        RaisePropertyChanged("BindLargestNumberOfEntrants");
                    },
                    Host + "/api/emulation?startCompain=" + _startEntrance + "&endCompain=" + _endEntrance + "&start=" + _simulationStart + "&end=" + _simulationEnd);
                });
            }
            catch (Exception e)
            {
                action(false);
                Console.WriteLine("ret false ======= " + e.ToString());
            }
        }

        public ICommand actionStart
        {
            get
            {
                return _startButton ?? (_startButton = new CommandHandler(() =>
                {
                    _isRunning = true;
                    Emulation();
                }, true));
            }
        }
        public ICommand actionStop
        {
            get
            {
                return _stopButton ?? (_stopButton = new CommandHandler(() =>
                {
                    _isRunning = false;
                }, true));
            }
        }
    }
}