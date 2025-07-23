using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Mr_J_Vers_2._0.ApiCalls;

namespace Mr_J_Vers_2._0
{
    public partial class MainForm : Form
    {
        SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer Marvel = new SpeechSynthesizer();
        bool wake = true;

        public MainForm()
        {
            InitializeComponent();
            //event handler for recognized text
            speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Mainevent_SpeechRecognized);
            //Load grammar for speach engin 
            LoadGrammarAndCommands();

            //use the system mic
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            //start listening
           // Start up
            Marvel.SelectVoiceByHints(VoiceGender.Male);
            Marvel.SpeakAsync("Loading");
            Marvel.SpeakAsync("Checking Connections");
            Marvel.SpeakAsync("Were ready to roll!");
            Marvel.SpeakAsync("Welcom.  I am Mr. J,  a PA made by Jamey and were here to show case the use of using  Windowsform");

            
            
        }

        private void LoadGrammarAndCommands()
        {

            // read the default commands 
            try
            {

                string constring = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
                using (var con = new SqlConnection(constring))
                using (var sc = new SqlCommand("SELECT * FROM DefaultTable", con))
                {
                    con.Open();
                    //sc.CommandType = CommandType.TableDirect;
                    SqlDataReader sdr = sc.ExecuteReader();
                    while (sdr.Read())
                    {
                        var Loadcmd = sdr["DefaultCommands"].ToString();
                        Grammar commandgrammar = new Grammar(new GrammarBuilder(new Choices(Loadcmd)));
                        speechRecognitionEngine.LoadGrammarAsync(commandgrammar);
                    }
                    sdr.Close();
                }

            }
            catch(Exception ex) {
                Marvel.SpeakAsync("I have found an issue in your commands, possibly a blank line." + ex.Message);
            } 
        }

    

        private void Mainevent_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            string Name = Environment.UserName;
            string speech = e.Result.Text;
            Debug_Commands.AppendText(speech + "\n");
            


            switch (speech)
            {
                case "hello":
                    Marvel.SpeakAsync("Why hello" + Name);
                    break;
                case "what time is it":
                    System.DateTime now = System.DateTime.Now;
                    string time = now.GetDateTimeFormats('t')[0];
                    Marvel.SpeakAsync("it is" + time);
                    break;
                case "what is todays date":
                    string date;
                    date = "the date is, " + System.DateTime.Now.ToString("M/d/yyy");
                    Marvel.SpeakAsync(date);
                    break;
                case "what are you":
                    Marvel.SpeakAsync("My name is Mr. J and i am a personal assistant made by Jamey to show case his coding skills");
                    Marvel.SpeakAsync("There is many things that i can do for you!");
                    break;
                case "what can you do":
                    Marvel.SpeakAsync("So many things look to the left side of the screen and look at the menu lets pick something to do");
                    break;
                case "lets jam":
                    Marvel.SpeakAsync("Nice lets Jam, loading it up for You Sir");
                    Process.Start(@"Spotify.exe");
                    break;
                case "play":
                    Marvel.SpeakAsync("Yes Sir");
                    SendKeys.Send(" ");
                    break;
                case "pause":
                    Marvel.SpeakAsync("Yes Sir");
                    SendKeys.Send(" ");
                    break;
                case "next":
                    Marvel.SpeakAsync("Right Away");
                    SendKeys.Send("^{RIGHT}^ ");
                    break;
                case "Lets get some code wars going":
                    Marvel.SpeakAsync("Good Luck Sir");
                    Process.Start("http://www.codewars.com");
                    break;
                case "whats a chuck norris fact":
                    var Fact = ChuckNorise.Chuckcall();
                    Marvel.SpeakAsync(Fact);
                    break;
                //case "tell me a joke":
                //    var joke = JokeApiCall.Jokecall();
                //    Marvel.SpeakAsync(joke);
                    //break;
                case "tell me a geeky joke":
                    var Gjoke = GeekyApi.GeekApi();
                    Marvel.SpeakAsync(Gjoke);
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LeftSideMenu.Width = 60;
          
            Title_Lbl.Text = "J";
            
          
        }

        private void LeftSideMenuBtn_Click(object sender, EventArgs e)
        {
            if(LeftSideMenu.Width == 60)
            {
                LeftSideMenu.Width = 250;
                Title_Lbl.Text = "Mr.J Ver 2.0";
            }
            else
            {
                LeftSideMenu.Width = 60;
                Title_Lbl.Text = "J";
            }
        }

        private void RightSideMenuBtn_Click(object sender, EventArgs e)
        {
            if (RightSideMenu.Width == 0)
            {
                RightSideMenu.Width = 250;
                RightSideMenu.Visible = true;
            }
            else
            {
                RightSideMenu.Width = 60;
                RightSideMenu.Visible = false;
            }
        }

  
    }
}
