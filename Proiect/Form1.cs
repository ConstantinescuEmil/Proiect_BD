using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> result = Utility.AvioaneCeTrecPrinStatie("Amsterdam");
            string res="";
            foreach (var i in result)
            {
                res = res + " " + i.ToString();
            }
            this.textBox1.Clear();
            this.textBox1.Text = res;
        }
    }
    public static class Utility
    {
        public static int NumarLocuriLibereLaDestinatie(string NumeAvion, string Data, string Destinatie)
        {
            var context = new AvioaneDataContext();
           
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
    }
}
