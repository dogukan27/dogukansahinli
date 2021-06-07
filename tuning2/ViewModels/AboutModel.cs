using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tuning.Models;

namespace tuning.ViewModels
{
    public class AboutModel
    {
        public List<misyonumuzSayfasi> Misyon { get; set; }
        public List<vizyonumuzSayfasi> Vizyon { get; set; }
        public List<hakkimizdaSayfasi> Hakkinda { get; set; }

        public List<sssSayfasi> SssSayfasis { get; set; }

        public List<iletisimSayfasi> IletisimSayfasi { get; set; }




    }
}