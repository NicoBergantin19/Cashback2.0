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
        /*I giorni iniziano a passare dal primo login, cioè quando viene creato l'account,
         poi si può decidere quanti giorni skippare e ovviamente gli utenti sono indipendenti 
         l'uno dall'altro. La possibilità avviene all'interno del pannello dello user così dopo 
         se si vuole rimanere sullo stesso account ma con i giorni passati e poi al massimo se 
         vuole cambiare utente torna indietro*/
        public Form1()
        {
            InitializeComponent();
            gigio = JsonConvert.DeserializeObject<Deserializzazione>(File.ReadAllText("utenti.json"));  //deserializzazione del file degli utenti
            Login.BringToFront();
            comboBox2.Items.AddRange(array);
        }

        public Deserializzazione gigio = new Deserializzazione();   //tutto il file
        public Persona astolfo = new Persona();     //singola persona
        internal static string json;
        internal string[] array = {"1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
        private void Login_Paint(object sender, PaintEventArgs e)
        {

        }
        public int indicepazzo;
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gigio.Utenti.Count; i++)    //Ricerca dell'utente di cui fare il login
            {
                if (gigio.Utenti[i].Username == textBox1.Text & gigio.Utenti[i].Password == textBox2.Text)
                {                       
                    User.BringToFront();
                    astolfo = gigio.Utenti[i];
                    indicepazzo = i;
                    Aggiuntacarte();
                    return;
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
            
            if (string.IsNullOrWhiteSpace(textBox3.Text) && string.IsNullOrWhiteSpace(textBox4.Text))   //Controllo dei textbox
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
            for (int i = 0; i < gigio.Utenti.Count; i++)    //controllo username
            {
                if (gigio.Utenti[i].Username == username)
                {
                    MessageBox.Show("Username non disponibile", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //serializzazione
            astolfo.Username = username;
            astolfo.Password = password;
            astolfo.Carte = new List<Carta>();
            astolfo.Transazione = new List<Transazioni>();
            gigio.Utenti.Add(astolfo);           
            Salva();
            Login.BringToFront();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)  //aggiunta carte
        {
            string carta, nome, cognome, cvc, mese, anno, circuito = "";

            if (string.IsNullOrWhiteSpace(textBox6.Text) && string.IsNullOrWhiteSpace(textBox7.Text) && string.IsNullOrWhiteSpace(textBox8.Text)
                && string.IsNullOrWhiteSpace(textBox9.Text) && string.IsNullOrWhiteSpace(textBox10.Text) && string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("I campi non possono essere vuoti", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            carta = textBox6.Text;
            nome = textBox7.Text;
            cognome = textBox8.Text;
            cvc = textBox9.Text;
            mese = textBox10.Text;
            anno = textBox11.Text;

            //Controllo e assegnazione circuito in base ai primi 4 numeri
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

            //////controllo finale///////
            if (circuito == "")
            {
                MessageBox.Show("I dati della carta non sono validi", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < astolfo.Carte.Count; i++)   //controllo se la carta è già registrata
            {
                if (astolfo.Carte[i].Numero == carta)
                {
                    MessageBox.Show("La carta non può essere doppia", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //serializzazione
            astolfo.Carte.Add(new Carta { Nome = nome, Cognome = cognome, Circuito = circuito, Numero = carta, CVC = cvc, Anno = anno, Mese = mese, Saldo = 10000});
            for (int i = 0; i < gigio.Utenti.Count; i++)
            {
                if (gigio.Utenti[i].Username == astolfo.Username)
                {
                    gigio.Utenti[i] = astolfo;
                }
            }
            File.WriteAllText("utenti.json", JsonConvert.SerializeObject(gigio, Formatting.Indented));

            MessageBox.Show("La carta è stata aggiunta");
            User.BringToFront();
            Aggiuntacarte();
        }

        internal void Aggiuntacarte()   //aggiunge le carte sul datagrid 
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < astolfo.Carte.Count; i++)
            {
                dataGridView1.Rows.Add(astolfo.Carte[i].Numero, astolfo.Carte[i].Nome + " " +
                    astolfo.Carte[i].Cognome, astolfo.Carte[i].Mese + "/" + astolfo.Carte[i].Anno);
            }
        }

        internal void Pagamenti(string esercente, int prezzo)   //permette i pagamenti richiesti dall'utente
        {
            if (comboBox1.Text == "")   //controlla se l'utente ha scelto la carta per i pagamenti
            {
                MessageBox.Show("Non hai selezionato nessuna carta", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Transazioni popi = new Transazioni();
            for (int i = 0; i < astolfo.Carte.Count; i++)   //inserisce nei dati dell'utente la carta e i conseguenti pagamenti ed esercenti
            {
                if (astolfo.Carte[i].Numero == comboBox1.Text)
                {
                    popi.CircuitoTr = astolfo.Carte[i].Circuito;
                    popi.NumCarta = astolfo.Carte[i].Numero;
                    popi.Esercente = esercente;
                    popi.PrezzoTot = prezzo;
                    popi.Giorno = DateTime.Today.ToShortDateString();
                    astolfo.Carte[i].Saldo -= prezzo;
                    label25.Text = "Saldo: " + astolfo.Carte[i].Saldo;
                    break;
                }
            }
            //serializzazione
            astolfo.Transazione.Add(popi);
            MessageBox.Show("Pagamento andato a buon fine");          
        }

        internal void Salva()   //serializzazione
        {
            gigio.Utenti[indicepazzo] = astolfo;
            File.WriteAllText("utenti.json", JsonConvert.SerializeObject(gigio, Formatting.Indented));
        }

        internal void CaricaTransazioni()       //inserisce nella tabella dei pagamenti cosa si è effettuato
        {
            dataGridView2.Rows.Clear();
            for (int i = 0; i < astolfo.Transazione.Count; i++)
            {
                dataGridView2.Rows.Add(astolfo.Transazione[i].NumCarta, astolfo.Transazione[i].PrezzoTot,
                    astolfo.Transazione[i].Esercente, astolfo.Transazione[i].Giorno);
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
            //aggiorna le carte
            comboBox1.Items.Clear();
            for (int i = 0; i < astolfo.Carte.Count; i++)
            {
                comboBox1.Items.Add(astolfo.Carte[i].Numero);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Pagamenti(((Button)sender).Text, 50);
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
            Salva();
            Login.BringToFront();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //assicurazione = 45 euro
            Pagamenti(((Button)sender).Text, 50);
        }

        private void button22_Click(object sender, EventArgs e)
        {           
            CaricaTransazioni();
            Tracciato.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //spesa = 40 euro
            Pagamenti(((Button)sender).Text, 40);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //benzina = 20 euro
            Pagamenti(((Button)sender).Text, 20);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //medicine = 150 euro
            Pagamenti(((Button)sender).Text, 150);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //affitto = 200
            //bollette mensili = 400
            Pagamenti(((Button)sender).Text, 600);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Revisione auto/moto = 60
            Pagamenti(((Button)sender).Text, 60);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Carte.BringToFront();                        
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //uscita = 50 euro
            Pagamenti(((Button)sender).Text, 50);
        }

        private void Spese_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)  //tabella pagamenti
        {
            CaricaTransazioni();
            Tracciato.BringToFront();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string giorno;
            giorno = DateTime.Today.ToShortDateString();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Non hai inserito i giorni da saltare", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            astolfo.Tempo += Convert.ToInt32(comboBox2.Text);
        }
    }
    public class Deserializzazione  //deserializzazione della persona
    {
        public List<Persona> Utenti { get; set; }
    }

    public class Transazioni    //Dati pagamento
    {
        public string CircuitoTr { get; set; }
        public string NumCarta { get; set; }
        public string Giorno { get; set; }
        public int PrezzoTot { get; set; }
        public string Esercente { get; set; }
    }

    public class Persona
    {       
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Carta> Carte { get; set; }
        public List<Transazioni> Transazione { get; set; }
        public int Tempo { get; set; }
    }

    public class Carta  //Dati carta
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Numero { get; set; }
        public string CVC { get; set; }
        public string Mese { get; set; }
        public string Anno { get; set; }
        public string Circuito { get; set; }
        public int Saldo { get; set; }
    }
}
