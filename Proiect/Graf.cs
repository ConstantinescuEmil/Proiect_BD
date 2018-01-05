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
        public List<Tuple<int,int,int,TimeSpan>> StatiiFinale;
        int ok;
        public Graf()
        {
            StatiiFinale = new List<Tuple<int, int, int, TimeSpan>>();
            ok = 0;
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
            ok = 0;
            StatiiFinale.Clear();
            List<Tuple<int,int,int,TimeSpan>> noduri = new List<Tuple<int,int,int,TimeSpan>>();
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
                noduri.Add(new Tuple<int, int,int,TimeSpan>(Statie1, -1,min.Item2,min.Item1));
            }
            List<Tuple<int, int, int, TimeSpan>> Potentiale_muchii = new List<Tuple<int, int, int, TimeSpan>>();
            //cat timp mai pot adauga noduri in arborele de cost minim
            while(NrNoduriAdaugate!=0)
            {
                //nu am inca variante potentiale
                Potentiale_muchii.Clear();
                NrNoduriAdaugate = 0;
                //pentru fiecar nod deja in arbore
                for (int i = 0; i < noduri.Count; i++)
                {
                    int nod_curent = noduri.ElementAt(i).Item1;
                    int parinte = noduri.ElementAt(i).Item2;
                    int avion = noduri.ElementAt(i).Item3;
                    //pentru fiecare nod vecin al lui nod curent;
                    for (int j = 1; j < Compozitie.ElementAt(nod_curent).Count; j++)
                    {
                        //daca vecinul nu exista in arbore
                        //daca sunt rute posibile intre nodcurent si j
                        if (Compozitie.ElementAt(nod_curent).ElementAt(j).Count != 0 && (!EsteInNOduri(j, noduri)))
                        {
                            //adaug o variante potentiala de muchie
                            Potentiale_muchii.Add(AvionulCelMaiConvenabil(noduri.ElementAt(i), j, Data));
                        }
                    }
                }
                if(Potentiale_muchii.Count!=0)
                {
                    Tuple<int, int, int, TimeSpan> CelMaiOptim = MuchiaPentrArbore(Potentiale_muchii);
                    if(CelMaiOptim.Item4!=new TimeSpan(23,59,59))
                     {
                      NrNoduriAdaugate++;
                      noduri.Add(CelMaiOptim);
                     }
                }
               
            }
            Reconstituire_Cale(noduri, Statie2, Statie1);
        }
          private bool EsteInNOduri(int nod, List<Tuple<int, int,int,TimeSpan>> noduri)
        {
            int ok = 0;
            for(int i=0;i<noduri.Count;i++)
            {
                if (noduri.ElementAt(i).Item1 == nod) ok = 1;
            }
            if (ok == 1) return true;
            else return false;
        }
        private Tuple<int,int,int,TimeSpan> AvionulCelMaiConvenabil(Tuple<int, int, int,TimeSpan> nod1,int nod2,string Data)
        {
            Tuple<TimeSpan, int> min = new Tuple<TimeSpan, int>(new TimeSpan(23, 59, 59), 0);
            for(int k=0;k<Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).Count;k++)
            {
                if(
                    Utility.NumarLocuriLibereLaDestinatie(
                       Utility.GetNumeAvion( Compozitie.ElementAt(nod1.Item1).ElementAt(nod2).ElementAt(k)),
                       Data,
                       Utility.GetStatieName(nod2))
                    >0)
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
                return new Tuple<int, int,int, TimeSpan>(nod2,nod1.Item1,min.Item2,min.Item1);
            }
            else
            return new Tuple<int, int, int,TimeSpan>(-1, -1,-1, new TimeSpan(23,59,59));
        }
        private Tuple<int,int,int,TimeSpan> MuchiaPentrArbore(List<Tuple<int,int,int,TimeSpan>>Potentiali)
        {
            TimeSpan Tmin = Potentiali.ElementAt(0).Item4;
            int index = 0;
            for(int k=1;k<Potentiali.Count;k++)
            {
                if(Tmin>Potentiali.ElementAt(k).Item4)
                {
                    index = k;
                    Tmin = Potentiali.ElementAt(k).Item4;
                }
            }
            return Potentiali.ElementAt(index);
        }
        private void Reconstituire_Cale(List<Tuple<int,int,int,TimeSpan>>Arbore,int Destinatie,int Inceput)
        {
            int result = GetIndexOfNode(Destinatie,Arbore);
            if(result!=-1)
            {
                StatiiFinale.Add(Arbore.ElementAt(result));
                if (result == Inceput) ok = 1;
                result = GetIndexOfNode(Arbore.ElementAt(result).Item2, Arbore);
            }
            
            while(result!=-1)
            {
                if (result == Inceput) ok = 1;
                StatiiFinale.Add(Arbore.ElementAt(result));
                result = GetIndexOfNode(Arbore.ElementAt(result).Item2, Arbore);
            }
        }
        private int GetIndexOfNode(int Node,List<Tuple<int,int,int, TimeSpan>>Noduri)
        {
            for(int k=0;k<Noduri.Count;k++)
            {
                if (Noduri.ElementAt(k).Item1 == Node) return k;
            }
            return -1;
        }
    }
   
}
