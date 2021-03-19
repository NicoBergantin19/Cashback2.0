using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Cashback2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gigio = JsonConvert.DeserializeObject<Deserializzazione>(File.ReadAllText("utenti.json"));
        }

        public Deserializzazione gigio = new Deserializzazione();   //tutto il file
        public Persona astolfo = new Persona();     //singola persona
        internal static string json;

        private void Login_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gigio.Utenti.Count; i++)
            {
                if (gigio.Utenti[i].Username == textBox1.Text & gigio.Utenti[i].Password == textBox2.Text)
                {

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string username, password;
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
    public class Deserializzazione
    {
        public List<Persona> Utenti { get; set; }
    }
    public class Persona
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Carta1 { get; set; }
        public string Carta2 { get; set; }
        public string Carta3 { get; set; }
    }
}
