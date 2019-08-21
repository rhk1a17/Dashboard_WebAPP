using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard_WebAPP.Models
{
    public class PortalViewModel
    {
        public string SelectedInverter { get; set; }
        public List<SunnyPortal> Inverters
        {
            get
            {
                return new List<SunnyPortal>() {
                    new SunnyPortal() {Place = "TR Energy 0.99MWp", Id="TR Energy 0.99MWp"},
                    new SunnyPortal() {Place = "TCMA 1MWp", Id="TCMA 1MWp"},
                    new SunnyPortal() {Place = "Toyo Tires Manufacturing (M)", Id="Toyo Tires Manufacturing (M)"},
                    new SunnyPortal() {Place = "Choo Lay Khuan 5kWp", Id="Choo Lay Khuan 5kWp"},
                    new SunnyPortal() {Place = "Nasharuddin 2017", Id="Nasharuddin 2017"},
                    new SunnyPortal() {Place = "ChuaChinPeng", Id="ChuaChinPeng"},
                    new SunnyPortal() {Place = "tingchenhunt", Id="tingchenhunt"},
                    new SunnyPortal() {Place = "Mohamad Shahrir", Id="Mohamad Shahrir"},
                    new SunnyPortal() {Place = "Loh Kiat Yoong", Id="Loh Kiat Yoong"},
                    new SunnyPortal() {Place = "Krishna", Id="Krishna"},
                    new SunnyPortal() {Place = "Affandi", Id="Affandi"},
                    new SunnyPortal() {Place = "Lee Szed Kee", Id="Lee Szed Kee"},
                    new SunnyPortal() {Place = "Ooi Lee Kean", Id="Ooi Lee Kean"},
                    new SunnyPortal() {Place = "SS15", Id="SS15"},
                    new SunnyPortal() {Place = "Soo Kai Soon", Id="Soo Kai Soon"},
                    new SunnyPortal() {Place = "Chua Eng Hin", Id="Chua Eng Hin"},
                    new SunnyPortal() {Place = "Ooi Hoon Kong", Id="Ooi Hoon Kong"},
                    new SunnyPortal() {Place = "William Lin Kee Wai", Id="William Lin Kee Wai"},
                    new SunnyPortal() {Place = "CP Wong", Id="CP Wong"},
                    new SunnyPortal() {Place = "Ng Wei Xin", Id="Ng Wei Xin"},
                    new SunnyPortal() {Place = "secret-garden 6.3kWp Solar PV", Id="secret-garden 6.3kWp Solar PV"},
                    new SunnyPortal() {Place = "Kuvendran", Id="Kuvendran"},
                    new SunnyPortal() {Place = "Jack Ong Seng Leong", Id="Jack Ong Seng Leong"},
                    new SunnyPortal() {Place = "Tan Chun Weng", Id="Tan Chun Weng"},
                    new SunnyPortal() {Place = "Yim Kien Wai 11.925kWP", Id="Yim Kien Wai 11.925kWP"},
                    new SunnyPortal() {Place = "Lau Ken Poh", Id="Lau Ken Poh"},
                    new SunnyPortal() {Place = "Lee Khek Mui", Id="Lee Khek Mui"},
                    new SunnyPortal() {Place = "CM Yee", Id="CM Yee"},
                    new SunnyPortal() {Place = "Jerome Heah", Id="Jerome Heah"},
                    new SunnyPortal() {Place = "ChocoPOW", Id="ChocoPOW"},
                    new SunnyPortal() {Place = "ChengPK 22 S.Tropika U13/20B", Id="ChengPK 22 S.Tropika U13/20B"},
                    new SunnyPortal() {Place = "Klang Presbyterian Church", Id="Klang Presbyterian Church"},
                    new SunnyPortal() {Place = "1773 Aviva Green", Id="1773 Aviva Green"},
                    new SunnyPortal() {Place = "Tan Yu Wea", Id="Tan Yu Wea"},
                    new SunnyPortal() {Place = "Fresh Fishery 132kWp", Id="Fresh Fishery 132kWp"},
                    new SunnyPortal() {Place = "River Of Life", Id="River Of Life"},
                    new SunnyPortal() {Place = "Kraiburg-TPE", Id="Kraiburg-TPE"},
                    new SunnyPortal() {Place = "Hercules F2", Id="Hercules F2"},
                    new SunnyPortal() {Place = "SSL 180kwp", Id="SSL 180kwp"},
                    new SunnyPortal() {Place = "Shea Fatt Hardware (M) Sdn Bhd", Id="Shea Fatt Hardware (M) Sdn Bhd"},
                    new SunnyPortal() {Place = "SYW INDUSTRY", Id="SYW INDUSTRY"},
                    new SunnyPortal() {Place = "ICP Ulu Choh", Id="ICP Ulu Choh"},
                    new SunnyPortal() {Place = "Thumbprints Utd Sdn Bhd", Id="Thumbprints Utd Sdn Bhd"},
                    new SunnyPortal() {Place = "FIRMA ODESI 1MW", Id="FIRMA ODESI 1MW"},

                };
            }
        }

        public IEnumerable<SelectListItem> GetInverterListItems()
        {
            return Inverters.Select(c => new SelectListItem()
            {
                Text = c.Place,
                Value = c.Id,
                Selected = (c.Id == SelectedInverter)
            });
        }
    }
}