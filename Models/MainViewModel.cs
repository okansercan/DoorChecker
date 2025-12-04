using AndroidX.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.App.Assist.AssistStructure;

namespace DoorChecker.Models
{
    public class MainViewModel
    {
        public string Location { get; set; }
        public string Door { get; set; }
        public bool Check1 { get; set; }
        public bool Check2 { get; set; }
        public bool Check3 { get; set; }
        public bool Check4 { get; set; }
        public bool Check5 { get; set; }
        public bool Check6 { get; set; }
        public bool Check7 { get; set; }
        public bool Check8 { get; set; }
        public bool Check9 { get; set; }
        public bool Check10 { get; set; }
        public bool Check11 { get; set; }
        public bool Check12 { get; set; }
        public IList<string> Locations { get; }
        public IList<string> Doors { get; }

        public MainViewModel()
        {
            Locations = new List<string>
            {
                "Location A",
                "Location B",
                "Location C"
            };
            Doors = new List<string>
            {
                "Door 1",
                "Door 2",
                "Door 3"
            };
        }     
    }
}
