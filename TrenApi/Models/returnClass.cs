using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrenApi.Models
{
    public class returnClass
    {
        public bool RezervasyonYapilabilir { get; set; }
        public yerlesim[] YerlesimAyrinti { get; set; }
    }
}