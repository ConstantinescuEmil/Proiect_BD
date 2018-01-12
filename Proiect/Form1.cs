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
            var context = new AvioaneDataContext();
            var results = from c in context.Destinatiis
                          select c.Nume;
            foreach (var result in results)
            {
                this.Dest1C.Items.Add(result);
                this.Dest2C.Items.Add(result);
            }
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'avioaneDataSet.Bilete' table. You can move, or remove it, as needed.
            this.bileteTableAdapter.Fill(this.avioaneDataSet.Bilete);

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
            // TimeSpan h1 = Utility.OraAjungAvionStatie("Av1", "Lisbon");
            // Utility.Adauga_Bilete(graf.StatiiFinale, "2018-10-10", "ana", "maria", "12345677");
            // Utility.Afiseaza_posibile_bilete(graf.StatiiFinale, "2018-10-10");
            //this.textBox1.Clear();
            //this.textBox1.Text = "dffijdfidjikdjfdsikf";
            //this.textBox1.Text = h1.ToString();
            this.textBox1.Text =Convert.ToDateTime(this.dateTimePicker1.Text).ToString();
            
        }

        private void Dest1C_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CautaB_Click(object sender, EventArgs e)
        {
            var context = new AvioaneDataContext();
            Graf graf = new Graf();
            string statie1 = this.Dest1C.Text;
            string statie2 = this.Dest2C.Text;
            graf.Dijkstra_Modificat(Utility.GetIdStatie(statie1),Utility.GetIdStatie(statie2),Convert.ToDateTime(this.dateTimePicker1.Text).ToString());
           List<Bilete> Posibile_bilete=Utility.Afiseaza_posibile_bilete(graf.StatiiFinale, Convert.ToDateTime(this.dateTimePicker1.Text).ToString());
            DataTable alfa=new DataTable();
            alfa.Columns.Add("Zbor_Nr");
            alfa.Columns.Add("Destinatie1");
            alfa.Columns.Add("Ora1");
            alfa.Columns.Add("Destinetie2");
            alfa.Columns.Add("Ora2");
            for(int i=0;i<Posibile_bilete.Count;i++)
            {
                alfa.Rows.Add(Posibile_bilete.ElementAt(i).ID_Avion.ToString(),
                    Posibile_bilete.ElementAt(i).Destinatie_1.ToString(),
                    Posibile_bilete.ElementAt(i).Ora_Decolare.ToString(),
                    Posibile_bilete.ElementAt(i).Destinatie_2.ToString(),
                   Posibile_bilete.ElementAt(i).Ora_Aterizare.ToString());
            }
            this.dataGridView1.DataSource = alfa;
         
        }
    }
   
}
