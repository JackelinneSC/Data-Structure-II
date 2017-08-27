using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_StartUp_Guatemala_Salazar
{
    public partial class CreatPlayList : Form
    {
        public string namelist;
        public string description;
        static int i;

        public CreatPlayList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            namelist = textBox1.Text;
            description = textBox2.Text;
            if (String.IsNullOrEmpty(textBox1.Text))
                namelist = "new playlist" + i.ToString();
            i++;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
