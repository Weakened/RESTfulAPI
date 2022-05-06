using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using RESTAPIClient.Classes;

namespace RESTAPIClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            RestClient restClient = new RestClient();
            restClient.endPoint = txtUrl.Text;
            debugOutput("Client object has been created");
            JToken jToken =JToken.Parse(restClient.MakeRequest());
            var response = jToken.ToString(Newtonsoft.Json.Formatting.Indented);
            debugOutput(response);
        }

        private void debugOutput(string debugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(Environment.NewLine + debugText);
                txtResponse.Text += debugText + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.Text.Length;
                txtResponse.ScrollToCaret();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Environment.NewLine + ex.Message);
            }
        }
    }
}
