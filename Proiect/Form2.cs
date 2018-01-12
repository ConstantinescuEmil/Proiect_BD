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
    public partial class Form2 : Form
    {
        string Statie1, Statie2, Data;
        DataTable BileteF2;
        Graf grafF2;
        List<int> preturi;
        public int Calcul_Pret()
        {
            var context = new AvioaneDataContext();
            preturi = new List<int>();
            int result = 0;
            foreach (DataRow item in BileteF2.Rows)
            {
                var viteza = (from c in context.Avioanes
                              where c.ID_Avion.Equals(Convert.ToInt32(item["Zbor_Nr"].ToString()))
                              select c.Viteza_medie).First();
                int dist= Utility.GetDistantaIntreStatii(item["Destinatie1"].ToString(), item["Destinetie2"].ToString());
                result = result+Convert.ToInt32(viteza) * dist;
                this.preturi.Add(Convert.ToInt32(viteza) * dist);
            }
            return result;
        }

        private void Pret_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utility.Adauga_Bilete(grafF2.StatiiFinale, Data, this.textBox1.Text, this.textBox3.Text, this.textBox2.Text);
            var contex = new AvioaneDataContext();
            var bilete = from c in contex.Calatoris
                         join v in contex.Biletes on c.ID_Calator equals v.ID_Calator
                         where c.CNP.Equals(this.textBox2.Text)
                         select v;
            int i = 0;
            foreach (var item in bilete)
            {
                item.Pret = preturi.ElementAt(i);
                item.Loc = 0;
                contex.SubmitChanges();
                i++;
            }
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public Form2(string statie1, string statie2, string data,DataTable Bilete, ref Graf graf)
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = true;
            Statie1 = statie1;
            Statie2 = statie2;
            BileteF2 = Bilete;
            Data = data;
            this.Pret.Text = this.Calcul_Pret().ToString();
            grafF2 = graf;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.BileteF2;
        }
    }
}
