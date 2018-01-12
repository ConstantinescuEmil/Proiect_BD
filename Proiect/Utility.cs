using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public static int Verif_data_reciclare(string date,int avion)
        {
            var context = new AvioaneDataContext();
            var data = from c in context.Avioanes
                       where c.ID_Avion.Equals(avion)
                       select c.Data_Reciclare;
            DateTime DataBilet = new DateTime(Int32.Parse (date.Split(new char[] { '-'}).ElementAt(0)),
               Int32.Parse( date.Split(new char[] { '-'}).ElementAt(1))
               ,Int32.Parse(date.Split(new char[] { '-'}).ElementAt(2))
                );
            if (DataBilet >= data.ToList().ElementAt(0))
            {
                return 0;
            }
            else return 1;
        }
        public static int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next();
        }
        public static int GetIdStatie(string statiei)
        {
            var context = new AvioaneDataContext();
            var statie = (from c in context.Destinatiis
                          where c.Nume.Equals(statiei)
                          select c).First();
            return statie.ID_Destinatie;
        }
        public static List<Bilete> Afiseaza_posibile_bilete(List<Tuple<int, int, int, TimeSpan>> statii,string data)
        {
            statii.Reverse();
            List<Bilete> results = new List<Bilete>();
            int i = 1;
            int index = 0;
            while(i<statii.Count)
            {
                if (statii.ElementAt(i).Item3 == statii.ElementAt(index).Item3)
                {
                    if (i == statii.Count - 1)
                    {
                        //fac un posibil bilet
                        var bilet1 = new Bilete
                        {
                            ID_Avion = statii.ElementAt(index).Item3,
                            Data = Convert.ToDateTime(data),
                            Cod = Utility.GetRandomNumber(),
                            Destinatie_1 = Utility.GetStatieName(statii.ElementAt(index-1).Item1),
                            Destinatie_2 = Utility.GetStatieName(statii.ElementAt(i).Item1),
                            Ora_Decolare = statii.ElementAt(index-1).Item4,
                            Ora_Aterizare = statii.ElementAt(i).Item4,
                            ID_Calator =0

                        };
                        results.Add(bilet1);
                        index = i;
                    }
                }
                else
                {
                    //fac bilet
                    var bilet1 = new Bilete
                    {
                        ID_Avion = statii.ElementAt(index).Item3,
                        Data = Convert.ToDateTime(data),
                        Cod = Utility.GetRandomNumber(),
                        Destinatie_1 = Utility.GetStatieName(statii.ElementAt(index).Item1),
                        Destinatie_2 = Utility.GetStatieName(statii.ElementAt(i-1).Item1),
                        Ora_Decolare = statii.ElementAt(index).Item4,
                        Ora_Aterizare = statii.ElementAt(i-1).Item4,
                        ID_Calator =0
                    };
                    results.Add(bilet1);
                    index = i;
                }
                i++;
                
            }
            return results;

        }
        public static  void Adauga_Bilete(List<Tuple<int,int,int,TimeSpan>> statii,string data,string NumeClient,string Prenume,string CNPclient)
        {
            statii.Reverse();
            var context = new AvioaneDataContext();
            int i = 1;
            int index = 0;
            int OkBilet = 1;
           
                for (int j = 0; j < statii.Count; j++)
                {
                    if (Verif_data_reciclare(data, statii.ElementAt(j).Item3) == 0)
                    {
                        OkBilet = 0;
                        break;
                    }
                }
            
                if (OkBilet == 1)
                {
                //fac un client nou cu numele si prenumele
                //daca acesta nu si-a cumparad deja bilete pentru o a alta data
                var Cnp_De_Cautat = from c in context.Calatoris
                                    where c.CNP.Equals(CNPclient)
                                    select c.CNP;
                if (Cnp_De_Cautat.ToList().Count == 0)
                {
                    var client1 = new Calatori
                    {
                        Nume = NumeClient,
                        Prenume = Prenume,
                        CNP = CNPclient
                    };
                    context.Calatoris.InsertOnSubmit(client1);
                    context.SubmitChanges();
                }
                    var IDulCalatorIntrodus = from c in context.Calatoris
                                              where c.CNP.Equals(CNPclient)
                                              select c.ID_Calator;
                using (var scope = new TransactionScope())
                {
                    try
                    {

                        while (i < statii.Count)
                        {
                            if (statii.ElementAt(i).Item3 == statii.ElementAt(index).Item3)
                            {

                                if (i == statii.Count - 1)
                                {
                                    //fac bilet
                                    var bilet1 = new Bilete
                                    {
                                        ID_Avion = statii.ElementAt(index).Item3,
                                        Data = Convert.ToDateTime(data),
                                        Cod = Utility.GetRandomNumber(),
                                        Destinatie_1 = Utility.GetStatieName(statii.ElementAt(index-1).Item1),
                                        Destinatie_2 = Utility.GetStatieName(statii.ElementAt(i).Item1),
                                        Ora_Decolare = statii.ElementAt(index-1).Item4,
                                        Ora_Aterizare = statii.ElementAt(i).Item4,
                                        ID_Calator = IDulCalatorIntrodus.ToList().ElementAt(0)

                                    };
                                    context.Biletes.InsertOnSubmit(bilet1);
                                   
                                    index = i;
                                }

                            }
                            else
                            {//fac bilet
                                var bilet1 = new Bilete
                                {
                                    ID_Avion = statii.ElementAt(index).Item3,
                                    Data = Convert.ToDateTime(data),
                                    Cod = Utility.GetRandomNumber(),
                                    Destinatie_1 = Utility.GetStatieName(statii.ElementAt(index).Item1),
                                    Destinatie_2 = Utility.GetStatieName(statii.ElementAt(i-1).Item1),
                                    Ora_Decolare = statii.ElementAt(index).Item4,
                                    Ora_Aterizare = statii.ElementAt(i-1).Item4,
                                    ID_Calator = IDulCalatorIntrodus.ToList().ElementAt(0)

                                };
                                context.Biletes.InsertOnSubmit(bilet1);
                                //context.SubmitChanges();

                                index = i;
                            }
                            i++;
                        }
                        context.SubmitChanges();
                        scope.Complete();
                    }
                    catch
                    {
                       scope.Dispose();
                        var client = from c in context.Calatoris
                                     where c.CNP.Equals(CNPclient)
                                     select c;
                        context.Calatoris.DeleteOnSubmit(client.First());
                        context.SubmitChanges(); 
                    }
                }
                
            }

            
            //daca a m ok la bilete fac un client cu numele si prenumele
            //asignez la bilete clientul acela
            //dau submit
            // altfel dau rollbak 
            
        }
       /*
        * pentur proceduri stocate:
        * adaugare statie
        * adaugare conexiune intre statii la un anumit avion (cu verificare de coliziuni pentru ca un avion sa
        * ajunga doar intr-o singura statie)
        * adaugare dea vion
        *   AM AMANAT adaugarea de bilet simplu
        * procedura pentru simularea trecerii unei zi
        * procedure stergere bilete din acea zi
        * stargera unui avion din cauza uzurii (adica sa nu se mai poata sa se cumpere bilet dupa o anumita data pentru
        *  un avion ca nu osa mai circule)
        * stergerea o sa se faca cascadat cu o procedura stocata
        * AGAUGAREA DE BILETE SE FACE PRIN INTERMEDIUL TRANZACTIEI
        * implementare calculare pret
        * generarea vodului aleator pentru fiecare bilet
        * implementarea check-in (daca nu s-a facut si se simuleaza zborul se vor sterge biletele asociate )
        * un client trebuie sa isi faca checkinul pentru fiecare blet in parte
        * modalitate de stocare a datei curente pentru ca sa pot simula trecerea unei zile
        * modalitate de simulare a trecerii unei ore 
        *   daca a trecut o ora pentru fiecare avion daca a trecut printr-o statie sa se afiseze cine a intrat si cine a 
        *   iesit
        * implementarea pentru un client a sumei platite in cadrul biletului (extensie de camp la bilete)
        * afisarea unei liste de opriuni pentru fiecare bilet care sa adauge la pret
        * adaugarea de optiune e tot o tranzactie
        * jurnalizarea profiturilor pentru zile si avioane(extensie la baza de data)
        */

    }
}
