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
    public partial class Form3 : Form
    {
        Graf grafF3;

        private void button1_Click(object sender, EventArgs e)
        {
            var context = new AvioaneDataContext();
            int ok = 1;
            var client = from c in context.Calatoris
                         where (c.CNP == this.textBox3.Text && c.Nume == this.textBox1.Text && c.Prenume == this.textBox2.Text)
                         select c;
            if(client.ToList().Count!=0)
            {
                var bileteok = from c in context.Biletes
                               where (c.ID_Calator == client.ToList().First().ID_Calator &&
                               (c.Data > Convert.ToDateTime(this.textBox4.Text)))
                               select c;
                foreach (var item in bileteok)
                {
                    if(TimeSpan.Compare((DateTime)item.Data-Convert.ToDateTime(this.textBox4.Text), new TimeSpan(24*3, 0, 0))<0)
                    {
                        ok = 0;
                        break;
                    }
                        
                }
                if(ok==0)
                {
                    MessageBox.Show("Nu se poate face checkin mai devreme de 3 zi");
                }
                else
                {
                    var id = from c in context.Calatoris
                             where c.CNP.Equals(this.textBox3.Text)
                             select c;
                    Form form4 = new Form4(Convert.ToInt32(id.ToList().First().ID_Calator));
                    form4.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Data Gresite");
                this.Close();
            }
        }

        public Form3(ref Graf graf)
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
