using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDNDemo
{
    public partial class Detalhar : Form
    {
        public Detalhar()
        {
            InitializeComponent();
        }

        public Detalhar(string nome , string titulo, string data, string pergunta/*, string resposta*/)
        {
            InitializeComponent();

            lbAutor.Text = nome;
            lbTitulo.Text = titulo;
            lbDataPostagem.Text = data;
            lbPerguntaListBox.Text = pergunta;
            //label3.Text = resposta;

        }

        
    }
}
