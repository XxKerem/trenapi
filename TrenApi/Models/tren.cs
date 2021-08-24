using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrenApi.Models
{
    public class tren
    {
        public string Ad { get; set; }
        public Vagon[] Vagonlar { get; set; }
    }
}