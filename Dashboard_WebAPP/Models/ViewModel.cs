using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard_WebAPP.Models
{
    public class ViewModel
    {
        public string SelectedInverter { get; set; }
        public List<Inverter> Inverters
        {
            get
            {
                return new List<Inverter>() {
                    new Inverter() {Place = "Sg Besi", Serial="2130242191"},
                    new Inverter() {Place = "Bangsar", Serial="123"},
                    new Inverter() {Place = "Viet", Serial="3"}
                };
            }
        }

        public IEnumerable<SelectListItem> GetInverterListItems()
        {
            return Inverters.Select(c => new SelectListItem()
            {
                Text = c.Place + "  -  " + c.Serial,
                Value = c.Serial,
                Selected = (c.Serial == SelectedInverter)
            });
        }
    }
}