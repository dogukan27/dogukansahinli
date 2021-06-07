using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tuning.Models;
namespace tuning.ViewModels
{
    public class HizmetModel
    {
        public List<benzinchipuygulamasi> BenzinChip { get; set; }
        public List<boxuygulamasi> Box { get; set; }
        public List<chipyazilimiuygulamasi> ChipYazilim { get; set; }
        public List<pedaluygulamasi> Pedal { get; set; }
        public List<torkvegucartisi> Tork { get; set; }


    }
}