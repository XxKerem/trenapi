using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrenApi.Models;

namespace TrenApi.Controllers
{
    public class VagonController : ApiController
    {
        static Vagon[] vagon1 = new Vagon[]
        {
            new Vagon {Ad="vagon1",Kapasite=100,DoluKoltukAdet=50},
            new Vagon {Ad="vagon2",Kapasite=90,DoluKoltukAdet=80},
            new Vagon {Ad="vagon3",Kapasite=80,DoluKoltukAdet=80}
        };

        static tren tren1 = new tren {
            Ad = "Tren1",
            Vagonlar = vagon1
        };

        rezervasyon rezerv = new rezervasyon
        {
            tren = tren1,
            RezervasyonYapilacakKisiSayisi = 3,
            KisilerFarkliVagonlaraYerlestirilebilir = true
        };

        [HttpGet]
        public rezervasyon GetSonuc()
        {
            return rezerv;
        }

        public IHttpActionResult PostRezervasyon(rezervasyon rezerv)
        {
            returnClass returnSonuc = new returnClass();
            rezervasyon rezervTemp = rezerv;
            yerlesim[] yerlesimYeni = new yerlesim[rezerv.tren.Vagonlar.Length];
            int kisiSayi=0;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            if (rezerv.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                int sayac = 0,sayac2=0,sayac3=0;
                foreach(Vagon temp in rezerv.tren.Vagonlar)
                {
                    sayac2 = 0;
                    if(rezerv.RezervasyonYapilacakKisiSayisi != sayac)
                    {
                        kisiSayi = rezerv.RezervasyonYapilacakKisiSayisi - sayac;
                        for (int i = 1; i <= kisiSayi; i++)
                        {
                            if (temp.DoluKoltukAdet + 1 <= (temp.Kapasite * 7 / 10))
                            {
                                temp.DoluKoltukAdet++;
                                sayac++;
                                sayac2++;
                            }
                            else
                            {
                                if(sayac2 != 0)
                                {
                                    yerlesimYeni[sayac3] = new yerlesim { VagonAdi = temp.Ad, KisiSayisi = sayac2 };
                                }
                                else
                                {
                                    sayac3--;
                                }
                                break;
                            }
                        }
                        if(rezerv.RezervasyonYapilacakKisiSayisi == sayac)
                        {
                            yerlesimYeni[sayac3] = new yerlesim { VagonAdi = temp.Ad, KisiSayisi = sayac2 };
                        }
                    }
                    else
                    {
                        break;
                    }
                    sayac3++;


                }
                if(rezerv.RezervasyonYapilacakKisiSayisi-sayac == 0)
                {
                    Array.Resize(ref yerlesimYeni, sayac3);
                    returnSonuc.RezervasyonYapilabilir = true;
                    returnSonuc.YerlesimAyrinti = yerlesimYeni;
                }
                else
                {
                    Array.Resize(ref yerlesimYeni, 0);
                    returnSonuc.RezervasyonYapilabilir = false;
                    returnSonuc.YerlesimAyrinti = yerlesimYeni;
                }
            }
            else
            {
                foreach (Vagon temp in rezerv.tren.Vagonlar)
                {
                    if((temp.DoluKoltukAdet+rezerv.RezervasyonYapilacakKisiSayisi) <= (temp.Kapasite * 7 / 10))
                    {
                        returnSonuc.RezervasyonYapilabilir = true;
                        yerlesimYeni[0] = new yerlesim { VagonAdi = temp.Ad, KisiSayisi = rezerv.RezervasyonYapilacakKisiSayisi };
                        Array.Resize(ref yerlesimYeni, 1);
                        returnSonuc.YerlesimAyrinti = yerlesimYeni;
                        break;
                    }
                    else
                    {
                        Array.Resize(ref yerlesimYeni, 0);
                        returnSonuc.RezervasyonYapilabilir = false;
                        returnSonuc.YerlesimAyrinti = yerlesimYeni;
                    }
                }
            }
            return Ok(returnSonuc);
        }
    }
}
