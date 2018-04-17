using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Newtonsoft.Json; //This requires download of NuGet Newtonsoft's Json.Net
using System.Diagnostics;
using NAudio.Wave;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int cycle = 0;
        private static readonly HttpClient client = new HttpClient();

        private WaveFileWriter waveWriter = null;
        private WaveOutEvent wo = new WaveOutEvent();

        public MainWindow()
        {
            System.Diagnostics.Process backend = new System.Diagnostics.Process();

            backend.StartInfo.FileName = @"python.exe";
            backend.StartInfo.Arguments = @"api.py"; //argument
            backend.StartInfo.CreateNoWindow = true;
            backend.StartInfo.UseShellExecute = false;
            backend.StartInfo.RedirectStandardOutput = true;
            backend.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            backend.Start();

            InitializeComponent();
        }

        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;

        private void recButton_callback(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            var outF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundCount");

            if (cycle == 0)
            {
                Directory.CreateDirectory(outF);
            }

            cycle++;
            if (cycle % 2 == 0)
            {
                waveSource.StopRecording();
                waveFile.Close();
                send_request();
                clickedButton.Content = "Record";
            }
            else
            {
                waveSource = new WaveIn();
                waveSource.WaveFormat = new WaveFormat();
                waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
                waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

                waveFile = new WaveFileWriter(@"rec.wav", waveSource.WaveFormat);

                waveSource.StartRecording();

                clickedButton.Content = "Stop";
            }
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        private async void send_request()
        {
            var the_spot = Path.Combine(Directory.GetCurrentDirectory(), "rec.wav");

            MultipartFormDataContent form = new MultipartFormDataContent();
            //form.Add(new ByteArrayContent(File.ReadAllBytes(the_spot)), "file", "rec.wav");
            var bytes = File.ReadAllBytes(the_spot);
            form.Add(new ByteArrayContent(bytes, 0, bytes.Length), "file", "rec.wav");
            HttpResponseMessage response = await client.PostAsync("http://127.0.0.1:5000", form);

            Stream theStream = await response.Content.ReadAsStreamAsync(); //read in the response to a stream
            StreamReader theReader = new StreamReader(theStream); //create a reader for the stream

            string theLine = theReader.ReadToEnd(); //Gets POST Request Response Message
            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(theLine); //Deserialize the JSON into our created classes

            display(jsonObject);
        }

        private void display(RootObject rootobject)
        {
            string words = "Words: ";

            foreach (var outer in rootobject.meta.text)
            {
                foreach (var word in outer)
                {
                    words += word[0] + ' ';
                }
            }

            countLbl.Content = "Count: " + rootobject.count;
            textLbl.Content = words;
            analysisLbl.Content = rootobject.meta.age + ' ' + rootobject.meta.gender + ", " + rootobject.meta.dialect;
            durationLbl.Content = rootobject.meta.duration + " seconds";





        }
    }

    //The following are Classes necessary for parsing JSON
    public class Meta
    {
        public List<List<List<string>>> text { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string dialect { get; set; }
        public double duration { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public int count { get; set; }
        public Meta meta { get; set; }
    }
}
