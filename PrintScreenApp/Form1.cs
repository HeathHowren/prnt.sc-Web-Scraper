using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;

namespace PrintScreenApp
{
    public partial class Form1 : Form
    {
        string letters;
        string numbers;
        
        
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "ab";
            textBox2.Text = "1337";
        }

        public void search()
        {
            string path = letters + numbers;
            string url = "https://prnt.sc/" + path;

            using (WebClient wc = new WebClient())
            {
                //Getting src file to image
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows; Windows NT 5.1; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4");
                string html = wc.DownloadString(url);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"screenshot-image\"]");
                //Loading image to picturebox
                if (node.Attributes["src"].Value.Contains("https"))
                {
                    var request = WebRequest.Create(node.Attributes["src"].Value);
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        pictureBox1.Image = Bitmap.FromStream(stream);
                    }
                    label1.Text = url;
                    outputlog.Text = "Source URL: " + node.Attributes["src"].Value;
                }
                else
                {
                    label1.Text = url;
                    outputlog.Text = "ERROR - Invalid link: " + node.Attributes["src"].Value;
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            numbers = textBox2.Text;
            letters = textBox1.Text;
            search();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            letters = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            numbers = textBox2.Text;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            int numbersInt = Int16.Parse(numbers);
            ++numbersInt;
            numbers = numbersInt.ToString();
            search();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            int numbersInt = Int16.Parse(numbers);
            --numbersInt;
            numbers = numbersInt.ToString();
            search();
        }

        private void Random_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            numbers = (rnd.Next(1000, 9999)).ToString();
            int abcOne = rnd.Next(1, 26);
            int abcTwo = rnd.Next(1, 26);
            var chars = "abcdefghijklmnopqrstuvwyxz";
            letters = chars[abcOne].ToString() + chars[abcTwo].ToString();
            search();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void outputlog_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
