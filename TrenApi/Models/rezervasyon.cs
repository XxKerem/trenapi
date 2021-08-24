using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrenApi.Models
{
    public class rezervasyon
    {
        public tren tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}