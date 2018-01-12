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
    public partial class Form4 : Form
    {
        int ClientId;
        public Form4(int id)
        {
            InitializeComponent();
            this.ClientId = id;
            this.textBox4.Text = 0.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             this.textBox4.Text = 0.ToString();
            int k = 0;
            foreach (var itemChecked in checkedListBox1.CheckedItems)
            {
                k += 300;
            }
            this.textBox4.Text = k.ToString();
        }
        void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var contex = new AvioaneDataContext();
            string optiuni="";
            var calator = (from c in contex.Calatoris
                           where c.ID_Calator.Equals(this.ClientId)
                           select c).First();
            calator.Adresa = this.textBox1.Text;
            calator.Telefon=this.textBox2.Text;
            calator.Detalii_Buletin = this.textBox3.Text;
            var bilete = from c in contex.Biletes
                         where c.ID_Calator.Equals(this.ClientId)
                         select c;
            foreach (var item in bilete)
            {
                item.Checkin =1;
                foreach (var itemChecked in checkedListBox1.CheckedItems)
                {
                    optiuni += itemChecked.ToString();
                    optiuni += " ";
                }
                item.Optiuni = optiuni;
                var id_avion = (from c in contex.Biletes
                                where c.ID_Bilet.Equals(item.ID_Bilet)
                                select c.ID_Avion).First();
                item.Loc = get_loc(Convert.ToInt32(id_avion),item.Data.ToString());
                contex.SubmitChanges();
            }
            this.Close();
        }
        private int get_loc(int avion_id, string data)
        {
            var context = new AvioaneDataContext();
            var max_loc_avion = (from c in context.Avioanes
                         join v in context.Biletes on c.ID_Avion equals v.ID_Avion
                         where v.Data.Equals(Convert.ToDateTime( data))
                         orderby v.Loc
                         select v.Loc);
            return Convert.ToInt32(max_loc_avion.ToList().Last()) + 1;

        }
    }
}
