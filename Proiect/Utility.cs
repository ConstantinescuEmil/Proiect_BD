using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public static class Utility
    {
        public static int NumarLocuriLibereLaDestinatie(string NumeAvion, string Data, string Destinatie)
        {
            var context = new AvioaneDataContext();
            int LocuriOcupate = 0;
            var RutaAvion = StatiiRutaAvion(NumeAvion);
            foreach (var statie in RutaAvion)
            {
                var SeUrcaStatiaX = from c in context.Biletes
                                    join v in context.Avioanes on c.ID_Avion equals v.ID_Avion
                                    where (v.Nume.Equals(NumeAvion)) && (c.Data.Equals(Data)) && (c.Destinatie_1.Equals(statie))
                                    select c.ID_Bilet;
                var SeDauJosStatiaX = from c in context.Biletes
                                      join v in context.Avioanes on c.ID_Avion equals v.ID_Avion
                                      where (v.Nume.Equals(NumeAvion)) && (c.Data.Equals(Data)) && (c.Destinatie_2.Equals(statie))
                                      select c.ID_Bilet;
                LocuriOcupate += SeUrcaStatiaX.Count();
                LocuriOcupate -= SeDauJosStatiaX.Count();

                if (statie == Destinatie)
                {
                    break;
                }
            }

            return CapacitateAvion(NumeAvion) - LocuriOcupate;
        }
        public static List<int> AvioaneCeTrecPrinStatie(string NumeStatie)
        {
            var context = new AvioaneDataContext();
            List<int> result = new List<int>();

            var RuteCuStX = from c in context.Rutes
                            join o in context.Compozitie_Rutes
                            on c.ID_Ruta equals o.ID_Ruta
                            join w in context.Destinatiis on o.ID_Destinatie equals w.ID_Destinatie
                            where w.Nume.Equals(NumeStatie)
                            select c.ID_Ruta;
            foreach (var i in RuteCuStX)
            {
                var AvioaneInX = from c in context.Avioanes
                                 where c.ID_Ruta.Equals(i)
                                 select c.ID_Avion;
                foreach (var avi in AvioaneInX)
                {
                    if (!result.Contains(avi)) result.Add(avi);
                }
            }
            return result;
        }
        public static int CapacitateAvion(string Avion)
        {
            var context = new AvioaneDataContext();
            var Capacitate = from c in context.Avioanes
                             where c.Nume.Equals(Avion)
                             select c.Capacitate;

            if (Capacitate.ToList().ElementAt(0).HasValue)
            {
                return (int)Capacitate.ToList().ElementAt(0);
            }
            else return -1;
        }
        public static List<string> StatiiRutaAvion(string Avion)
        {
            var context = new AvioaneDataContext();
            var RutaAvion = from c in context.Avioanes
                            join v in context.Rutes on c.ID_Ruta equals v.ID_Ruta
                            join b in context.Compozitie_Rutes on v.ID_Ruta equals b.ID_Ruta
                            join n in context.Destinatiis on b.ID_Destinatie equals n.ID_Destinatie
                            where c.Nume.Equals(Avion)
                            select n.Nume;
            return RutaAvion.ToList();
        }
        public static int GetDistantaIntreStatii(string statie1, string statie2)
        {
            int distance = 0;
            var context = new AvioaneDataContext();
            var c1 = from c in context.Destinatiis
                     where c.Nume.Equals(statie1)
                     select new
                     {
                         c.Pozitie_x,
                         c.Pozitie_y
                     };
            var c2 = from c in context.Destinatiis
                     where c.Nume.Equals(statie2)
                     select new
                     {
                         c.Pozitie_x,
                         c.Pozitie_y
                     };
            double dx = (double)c1.ToList().ElementAt(0).Pozitie_x - (double)c2.ToList().ElementAt(0).Pozitie_x;
            double dy = (double)c1.ToList().ElementAt(0).Pozitie_y - (double)c2.ToList().ElementAt(0).Pozitie_y;

            distance = (int)Math.Floor(Math.Sqrt(dx * dx + dy * dy));
            return distance;
        }
        public static TimeSpan OraAjungAvionStatie(string Avion, string Statie)
        {
            var context = new AvioaneDataContext();
            var RutaAvion = StatiiRutaAvion(Avion);
            int total = 0;
            if (RutaAvion.ToList().ElementAt(0) != Statie)
            {
                for (var i = 1; i < RutaAvion.Count; i++)
                {
                    total += GetDistantaIntreStatii(RutaAvion.ToList().ElementAt(i), RutaAvion.ToList().ElementAt(i - 1));
                    if (RutaAvion.ToList().ElementAt(i) == Statie) break;
                }
            }
            var viteza_ora = from c in context.Avioanes
                             where c.Nume.Equals(Avion)
                             select new
                             {
                                 c.Viteza_medie,
                                 c.Ora_inceput
                             };
            TimeSpan ajung = (TimeSpan)viteza_ora.ToList().ElementAt(0).Ora_inceput;
            ajung = ajung + TimeSpan.FromHours((double)total / (double)viteza_ora.ToList().ElementAt(0).Viteza_medie);
            return ajung;
        }
        public static string GetStatieName(int ID)
        {
            var context = new AvioaneDataContext();
            var id = from c in context.Destinatiis
                     where c.ID_Destinatie.Equals(ID)
                     select c.Nume;
            return (string)id.ToList().ElementAt(0);
        }
        public static string GetNumeAvion(int ID)
        {
            var context = new AvioaneDataContext();
            var id = from c in context.Avioanes
                     where c.ID_Avion.Equals(ID)
                     select c.Nume;
            return (string)id.ToList().ElementAt(0);
        }

    }
}
