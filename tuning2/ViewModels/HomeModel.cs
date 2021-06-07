using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuning.ViewModels
{
    public class HomeModel
    {
        public List<tuning.Models.slider> Sliders { get; set; }

        public List<tuning.Models.ArabaMarka> Markalar { get; set; }
        public List<tuning.Models.ArabaModel> Modeller { get; set; }
        public List<tuning.Models.ArabaYil> Yillar { get; set; }
        public List<tuning.Models.ArabaMotor> Motorlar { get; set; }
        public List<tuning.Models.tuningliaraclar> Tuningliaraclars { get; set; }


    }
}