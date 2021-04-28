using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDNDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void btnAtualizarLista_Click(object sender, EventArgs e)
        {
            var wc = new WebClient();

            string pagina = wc.DownloadString
             ("https://social.msdn.microsoft.com/Forums/pt-br/home?filter=alltypes&sort=lastpostdesc");

            var htmlDocument = new HtmlAgilityPack.HtmlDocument();

            htmlDocument.LoadHtml(pagina);

            dataGridView1.Rows.Clear();

            string id = string.Empty;
            string titulo = string.Empty;
            string postagem = string.Empty;
            string exibicao = string.Empty;
            string resposta = string.Empty;
            string link = string.Empty;


            foreach (HtmlNode node in htmlDocument.GetElementbyId("threadList").ChildNodes)
            {
                if (node.Attributes.Count > 0)
                {
                    id = node.Attributes["data-threadid"].Value;
                    link = "https://social.msdn.microsoft.com/Forums/pt-br/" + id;
                    titulo = node.Descendants().First(x => x.Id.Equals("threadTitle_" + id)).InnerText;

                    postagem = WebUtility.HtmlDecode(node.Descendants()
                                   .First(x => x.Attributes["class"] != null &&
                                   x.Attributes["class"].Value.Equals("lastpost")).InnerText.Replace("\n", "")/*.Replace(" ", "")*/);

                    exibicao = WebUtility.HtmlDecode(node.Descendants()
                                   .First(x => x.Attributes["class"] != null &&
                                   x.Attributes["class"].Value.Equals("viewcount")).InnerText);

                    resposta = WebUtility.HtmlDecode(node.Descendants()
                                   .First(x => x.Attributes["class"] != null &&
                                   x.Attributes["class"].Value.Equals("replycount")).InnerText);

                    if (!string.IsNullOrEmpty(titulo))
                    {
                        dataGridView1.Rows.Add(titulo, postagem, exibicao, resposta, link);
                    }

                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                Process.Start(new ProcessStartInfo(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
            }
        }

        private void Detalhes(object sender, EventArgs e)
        {


            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView1.SelectedRows[0];

            string pagina = row.Cells[4].Value.ToString();


            var web1 = new HtmlWeb();
            var doc1 = web1.LoadFromBrowser(pagina, o =>
            {
                var webBrowser = (WebBrowser)o;

                    return !string.IsNullOrEmpty(webBrowser.Document.GetElementById("threadPageContentContainer").InnerText);
            });

            var titulo = doc1.DocumentNode.SelectSingleNode("//span[@name='subject']").InnerText;
            var nome = doc1.DocumentNode.SelectSingleNode("//a[@class='profile-mini-display-name-link']").InnerText;
            var data = doc1.DocumentNode.SelectSingleNode("//div[@class='date']").InnerText;
            var pergunta = doc1.DocumentNode.SelectSingleNode("//div[@class='messageContent']").InnerText;
            //var pergunta = doc1.DocumentNode.SelectSingleNode("//ul[@id='rootMessage']").InnerText
            //                                .Replace("Entrar para Votar", "").Replace("Pergunta","");
            //var respostas =  doc1.DocumentNode.SelectSingleNode("//ul[@class='messageContent']").InnerText.Replace("Entrar para Votar","");
            //var z = pergunta.SelectSingleNode("//div[@class='body']").InnerText;


            //var resposta = doc1.DocumentNode.Descendants().First(x => x.Attributes["class"] != null &&
            //                       x.Attributes["class"].Value.Equals("message  answer")).InnerText;


            Detalhar detalhar = new Detalhar(nome, titulo, data, pergunta);
            detalhar.Show();

            Console.WriteLine();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
