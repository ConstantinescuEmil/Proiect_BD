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
        DataTable alfa=new DataTable();
         Graf graf;
        //alfa= tabel ce va tine biletele pentru a le transmite altui form
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = true;
            var context = new AvioaneDataContext();
            var results = from c in context.Destinatiis
                          select c.Nume;
            foreach (var result in results)
            {
                this.Dest1C.Items.Add(result);
                this.Dest2C.Items.Add(result);
            }
            alfa.Columns.Add("Zbor_Nr");
            alfa.Columns.Add("Destinatie1");
            alfa.Columns.Add("Ora1");
            alfa.Columns.Add("Destinetie2");
            alfa.Columns.Add("Ora2");
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'avioaneDataSet.Bilete' table. You can move, or remove it, as needed.
            this.bileteTableAdapter.Fill(this.avioaneDataSet.Bilete);
           

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Graf graf = new Graf();
            graf.Dijkstra_Modificat(5, 9, "2018-10-10");
            // TimeSpan h1 = Utility.OraAjungAvionStatie("Av1", "Lisbon");
            //Utility.Afiseaza_posibile_bilete(graf.StatiiFinale, "2018-10-10");
             Utility.Adauga_Bilete(graf.StatiiFinale, "2018-10-10", "ana", "maria", "12345677");
            // Utility.Afiseaza_posibile_bilete(graf.StatiiFinale, "2018-10-10");
            this.textBox1.Clear();
            this.textBox1.Text = "dffijdfidjikdjfdsikf";
            //this.textBox1.Text = h1.ToString();
           // this.textBox1.Text =Convert.ToDateTime(this.dateTimePicker1.Text).ToString();
            
        }

        private void Dest1C_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CautaB_Click(object sender, EventArgs e)
        {
            this.alfa.Clear();
            this.CumparaB.Enabled = true;
            var context = new AvioaneDataContext();
            graf = new Graf();
            string statie1 = this.Dest1C.Text;
            string statie2 = this.Dest2C.Text;
            graf.Dijkstra_Modificat(Utility.GetIdStatie(statie1),Utility.GetIdStatie(statie2),Convert.ToDateTime(this.dateTimePicker1.Text).ToString());
            List<Bilete> Posibile=Utility.Afiseaza_posibile_bilete(graf.StatiiFinale, Convert.ToDateTime(this.dateTimePicker1.Text).ToString());
            for(int i=0;i<Posibile.Count;i++)
            {
                alfa.Rows.Add(Posibile.ElementAt(i).ID_Avion.ToString(),
                   Posibile.ElementAt(i).Destinatie_1.ToString(),
                   Posibile.ElementAt(i).Ora_Decolare.ToString(),
                 Posibile.ElementAt(i).Destinatie_2.ToString(),
                   Posibile.ElementAt(i).Ora_Aterizare.ToString());
            }
            this.dataGridView1.DataSource = alfa;
            int nok = 0;
            foreach (var item in Posibile)
            {
                var dateREV = from c in context.Avioanes
                              where (c.Data_Reciclare<(item.Data)&& c.ID_Avion.Equals(item.ID_Avion))
                              select c;
                if(dateREV.ToList().Count!=0)
                {
                    nok = 1;
                    break;
                }
            }
            if(nok==1)
            {
                this.CumparaB.Enabled = false;
                MessageBox.Show("Exista avioane in service la acea data. Reprogramati");
            }
        }

        private void CumparaB_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2(this.Dest1C.Text, this.Dest2C.Text,Convert.ToDateTime( this.dateTimePicker1.Text).ToString() ,alfa,ref graf);
            form2.ShowDialog();
            this.alfa.Clear();
        }

        private void CheckinB_Click(object sender, EventArgs e)
        {
            Form form3 = new Form3(ref this.graf);
            form3.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
   
}
