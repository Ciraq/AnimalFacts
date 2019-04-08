using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace AnimalsFact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Item> GetList()
        {
            List<Item> items = new List<Item>()
            {
                new Item(1,"Dog"),
                new Item(2,"Horse"),
                new Item(3,"Cat")
            };

            return items;
        }

        private static async Task<List<T>> DeserializeJsonData<T>(string url) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody.Substring(0, 1) == "{")
                    {
                        responseBody = "[" + responseBody + "]";
                    }
                    var result = JsonConvert.DeserializeObject<List<T>>(responseBody);
                    return result;
                }
            }

            //Another Way
            //using (WebClient webClient = new WebClient())
            //{

            //    string jsondata = await webClient.DownloadStringTaskAsync(url);
            //    if (jsondata.Substring(0, 1) == "{")
            //    {
            //        jsondata = "[" + jsondata + "]";
            //    }

            //    var result = JsonConvert.DeserializeObject<List<T>>(jsondata);

            //    return result;
            //}
        }
        private static void DeserializeAndDisplay<T>(DataGridView dataGrid, List<T> list) where T : class
        {
            dataGrid.DataSource = list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            switch (Settings1.Default.Language)
            {
                case "Azerbaijani":
                    ChangeLanguage("");
                    break;
                case "English":
                    ChangeLanguage("en-US");
                    break;
                default:
                    break;
            }

            comboBox1.DataSource = GetList();
            comboBox1.DisplayMember = "AnimalName";
            comboBox1.ValueMember = "Id";
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            string type = comboBox1.Text;
            decimal amount = numericUpDown1.Value;

            string url = $"https://cat-fact.herokuapp.com/facts/random?animal_type={type}&amount={amount}";
            var result = await DeserializeJsonData<Animal>(url);

            DeserializeAndDisplay(dataGridView1, result);
        }

        private void ChangeLanguage(string cult)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cult);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cult);
            btnShow.Text = language.btnShow;
            dilSeçimiToolStripMenuItem.Text = language.menu;
            azərbaycancaToolStripMenuItem.Text = language.AzToolStripMenuItem;
            ingiliscəToolStripMenuItem.Text = language.EnToolStripMenuItem;
        }

        private void azərbaycancaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ChangeLanguage("");
            Settings1.Default.Language = "Azerbaijani";
            Settings1.Default.Save();
        }

        private void ingiliscəToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("en-US");
            Settings1.Default.Language = "English";
            Settings1.Default.Save();
        }
    }
}  

