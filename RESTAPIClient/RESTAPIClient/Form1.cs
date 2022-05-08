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
            if (rbnRYO.Checked)
            {
                restClient.authTech = Enums.AuthenticationTechnique.authenticationTechnique.RollYourOwn;
                debugOutput("AuthTechnique: Roll Your Own");
                debugOutput("AuthType: Basic");
            }
            else 
            {
                restClient.authTech = Enums.AuthenticationTechnique.authenticationTechnique.NetworkCredentials;
                debugOutput("AuthTechnique: Network Credentials");
                debugOutput("AuthType: ??? - Netcred decides");
            }
            //restClient.authTech = Enums.AuthenticationTechnique.authenticationTechnique.RollYourOwn;
            //restClient.authType = Enums.AuthenticationType.authenticationType.Basic;
            restClient.username = txtUsername.Text;
            restClient.password = txtPassword.Text;

            debugOutput("Client object has been created");
            try
            {
                JToken jToken = JToken.Parse(restClient.MakeRequest());
                var response = jToken.ToString(Newtonsoft.Json.Formatting.Indented);
                debugOutput(response);
            }
            catch (Exception ex)
            {
                //TODO add logger
            }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResponse.Clear();
        }
    }
}
