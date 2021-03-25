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

        public int indicepazzo;

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gigio.Utenti.Count; i++)
            {
                if (gigio.Utenti[i].Username == textBox1.Text & gigio.Utenti[i].Password == textBox2.Text)
                {
                    Aggiuntacarte();
                    User.BringToFront();
                    Login.Enabled = false;
                    astolfo = gigio.Utenti[i];
                    indicepazzo = i;                   
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
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registrazione.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)  //registrazione
        {
            string username, password;            
            
            if (string.IsNullOrWhiteSpace(textBox3.Text) && string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("I campi non possono essere vuoti");
                return;
            }

            username = textBox3.Text;
            password = textBox4.Text;

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Le password non coincidono");
                return;
            }

            /////fine//////
            for (int i = 0; i < gigio.Utenti.Count; i++)
            {
                if (gigio.Utenti[i].Username == username)
                {
                    MessageBox.Show("Username non disponibile");
                    return;
                }
            }

            astolfo.Username = username;
            astolfo.Password = password;
            astolfo.Carte = new List<Carta>();
            gigio.Utenti.Add(astolfo);
            File.WriteAllText("utenti.json", JsonConvert.SerializeObject(gigio, Formatting.Indented));

            Carte.BringToFront();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)  //aggiunta carte
        {
            string carta, nome, cognome, cvc, mese, anno, circuito = "";
            json = File.ReadAllText("utenti.json");
            Persona Carte = JsonConvert.DeserializeObject<Persona>(json);

            if (string.IsNullOrWhiteSpace(textBox6.Text) && string.IsNullOrWhiteSpace(textBox7.Text) && string.IsNullOrWhiteSpace(textBox8.Text)
                && string.IsNullOrWhiteSpace(textBox9.Text) && string.IsNullOrWhiteSpace(textBox10.Text) && string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("I campi non possono essere vuoti");
                return;
            }

            carta = textBox6.Text;
            nome = textBox7.Text;
            cognome = textBox8.Text;
            cvc = textBox9.Text;
            mese = textBox10.Text;
            anno = textBox11.Text;

            //////controllo finale///////

            switch (textBox6.Text.Substring(0,4))
            {
                case "4023":
                    circuito = "MasterCard";
                    break;

                case "1234":
                    circuito = "ViXa";
                    break;

                case "9876":
                    circuito = "AmericanExpress";
                    break;
                    
            }

            if (circuito == "")
            {
                MessageBox.Show("I dati della carta non sono validi");
                return;
            }

            for (int i = 0; i < astolfo.Carte.Count; i++)
            {
                if (astolfo.Carte[i].Numero == carta)
                {
                    MessageBox.Show("La carta non può essere doppia");
                    return;
                }
            }

            astolfo.Carte.Add(new Carta { Nome = nome, Cognome = cognome, Circuito = circuito, Numero = carta, CVC = cvc, Anno = anno, Mese = mese});
            for (int i = 0; i < gigio.Utenti.Count; i++)
            {
                if (gigio.Utenti[i].Username == astolfo.Username)
                {
                    gigio.Utenti[i] = astolfo;
                }
            }
            File.WriteAllText("utenti.json", JsonConvert.SerializeObject(gigio, Formatting.Indented));

            MessageBox.Show("La carta è stata aggiunta");
            Login.BringToFront();
        }

        internal void Aggiuntacarte()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < astolfo.Carte.Count; i++)
            {
                dataGridView1.Rows.Add(astolfo.Carte[i].Numero, astolfo.Carte[i].Nome + " " +
                    astolfo.Carte[i].Cognome, astolfo.Carte[i].Mese + "/" + astolfo.Carte[i].Anno);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Carte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            VistaCarte.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Spese.BringToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void TabellaCarte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            User.BringToFront();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Login.BringToFront();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            User.BringToFront();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            User.BringToFront();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Login.BringToFront();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        public List<Carta> Carte { get; set; }
    }

    public class Carta
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Numero { get; set; }
        public string CVC { get; set; }
        public string Mese { get; set; }
        public string Anno { get; set; }
        public string Circuito { get; set; }
    }
}
