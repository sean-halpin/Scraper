using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scraper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void docCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = ((WebBrowser)sender);
            HtmlElementCollection tables = browser.Document.GetElementsByTagName("table");
            foreach (HtmlElement TBL in tables)
            {
                outputTextBox.Text += Environment.NewLine + (rawTextToCSVFormat(TBL.InnerText));
                Console.WriteLine(TBL.InnerText);
            }
            browser.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in registrationTextBox.Lines)
            {
                var browser = new WebBrowser();
                browser.ScriptErrorsSuppressed = true;
                browser.DocumentCompleted += docCompleted;
                browser.Navigate("https://www.cartell.ie/ssl/servlet/beginStarLookup?registration=" + item.ToString());
            }
        }

        private string rawTextToCSVFormat(string raw)
        {
            return raw.Replace(Environment.NewLine + "Engine Capacity", Environment.NewLine + "EngineCapacity\t")
                .Replace(Environment.NewLine + "Make", Environment.NewLine + "Make\t")
                .Replace(Environment.NewLine + "Model", Environment.NewLine + "Model\t")
                .Replace(Environment.NewLine + "Description", Environment.NewLine + "Description\t");
        }
    }
}
