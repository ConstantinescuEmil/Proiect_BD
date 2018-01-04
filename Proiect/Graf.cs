using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Proiect
{
    public class Graf
    {
        private List<List<List<int>>> Compozitie;
        public List<int> StatiiFinale;
        public List<int> Optiuni;
        public Graf()
        {
            StatiiFinale = new List<int>();
            Optiuni = new List<int>();
            var context = new AvioaneDataContext();
            var Destinatii = from c in context.Destinatiis
                             select c.ID_Destinatie;
            var Rute = from c in context.Rutes
                       select c.ID_Ruta;
            Compozitie = new List<List<List<int>>>();
            for(int i=0;i<=Destinatii.ToList().Count;i++)
            {
                Compozitie.Add(new List<List<int>>());
                for(int j=0;j<=Destinatii.ToList().Count;j++)
                {
                    Compozitie.ElementAt(i).Add(new List<int>());
                }
            }
            foreach( var indruta in Rute)
            {
                var PeUndeTrece = from c in context.Rutes
                                  join v in context.Compozitie_Rutes on c.ID_Ruta equals v.ID_Ruta
                                  where v.ID_Ruta.Equals(indruta)
                                  select v.ID_Destinatie;
                var AvioaneCuRutaX = from c in context.Avioanes
                                     join v in context.Rutes on c.ID_Ruta equals v.ID_Ruta
                                     where v.ID_Ruta.Equals(indruta)
                                     select c.ID_Avion;
                for(int i=1;i<PeUndeTrece.ToList().Count;i++)
                {
                    for(int j=0;j<AvioaneCuRutaX.ToList().Count;j++)
                    {
                        if (!Compozitie.ElementAt(PeUndeTrece.ToList().ElementAt( i - 1)).ElementAt(PeUndeTrece.ToList().ElementAt(i)).Contains(AvioaneCuRutaX.ToList().ElementAt(j)))
                            Compozitie.ElementAt(PeUndeTrece.ToList().ElementAt(i - 1)).ElementAt(PeUndeTrece.ToList().ElementAt(i)).Add(AvioaneCuRutaX.ToList().ElementAt(j));
                    }
                   
                }
               
            }
            
        }
        public void Dijkstra_Modificat(int Statie1, int Statie2,string Data)
        {
            //atentie la constructorul da copiere al lui tamespan
            int NrNoduriAdaugate = 0;
            StatiiFinale.Clear();
            Optiuni.Clear();

            List<Tuple<int,int,int>> noduri = new List<Tuple<int,int,int>>();
            List<int> Caretrec = Utility.AvioaneCeTrecPrinStatie(Utility.GetStatieName(Statie1));
            Tuple<TimeSpan, int> min=new Tuple<TimeSpan, int>(new TimeSpan(23,59,59),0);
            for(int i=0;i<Caretrec.ToList().Count;i++)
            {
                if(Utility.NumarLocuriLibereLaDestinatie(
                    Utility.GetNumeAvion(Caretrec.ToList().ElementAt(i)),
                    Data,Utility.GetStatieName(Statie1))
                    >0)
                {
                    TimeSpan p = Utility.OraAjungAvionStatie(Utility.GetNumeAvion(Caretrec.ToList().ElementAt(i)),
                        Utility.GetStatieName(Statie1));
                        if(p<min.Item1)
                        {
                        min = new Tuple<TimeSpan, int>(p, Caretrec.ToList().ElementAt(0));
                        }
                }
            }
            if(min!=new Tuple<TimeSpan, int>(new TimeSpan(23, 59, 59), 0))
            {
                NrNoduriAdaugate++;
                Optiuni.Add(min.Item2);
                StatiiFinale.Add(Statie1);
                noduri.Add(new Tuple<int, int,int>(Statie1, 0,min.Item2));
            }
            int u = AvionulCelMaiConvenabil(noduri.ElementAt(0), 2, Data);
            while(NrNoduriAdaugate!=0)
            {
                NrNoduriAdaugate = 0;
                for(int i=0;i<noduri.Count;i++)
                {
                   
                }
            }
        }
          private bool EsteInNOduri(int nod, List<Tuple<int, int,int>> noduri)
        {
            int ok = 0;
            for(int i=0;i<noduri.Count;i++)
            {
                if (noduri.ElementAt(i).Item1 == nod) ok = 1;
            }
            if (ok == 1) return true;
            else return false;
        }
        private int AvionulCelMaiConvenabil(Tuple<int, int, int> nod1,int nod2,string Data)
        {
            Tuple<TimeSpan, int> min = new Tuple<TimeSpan, int>(new TimeSpan(23, 59, 59), 0);
            for(int k=0;k<Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).Count;k++)
            {
                if(
                    Utility.NumarLocuriLibereLaDestinatie(
                       Utility.GetNumeAvion( Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).ElementAt(k)),
                       Data,
                       Utility.GetStatieName(nod2))
                    !=0)
                 {
                    TimeSpan orasec = Utility.OraAjungAvionStatie(
                        Utility.GetNumeAvion(Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).ElementAt(k)),
                        Utility.GetStatieName(nod2)
                        );
                    if(
                        Utility.OraAjungAvionStatie( Utility.GetNumeAvion(nod1.Item3),Utility.GetStatieName( nod1.Item1))<
                        orasec
                        )
                    {
                        if(orasec<min.Item1)
                        {
                            min = new Tuple<TimeSpan, int>(orasec, Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).ElementAt(k));
                        }
                    }
                 }
            }
            if(min.Item1!=new TimeSpan(23,59,59))
            {
                return min.Item2;
            }
            return -1;
        }
    }
}
