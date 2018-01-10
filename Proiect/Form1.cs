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
            Graf graf = new Graf();
            graf.Dijkstra_Modificat(1, 12, "2018-10-10");
           // TimeSpan h1 = Utility.OraAjungAvionStatie("Av1", "Lisbon");
            Utility.Adauga_Bilete(graf.StatiiFinale, "2018-10-10", "ana", "maria", "12345677");
            this.textBox1.Clear();
            this.textBox1.Text = "dffijdfidjikdjfdsikf";
            //this.textBox1.Text = h1.ToString();
            
        }
    }
   
}
