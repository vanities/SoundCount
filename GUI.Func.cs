using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Http; //http requests
using System.Collections.Generic;
using Newtonsoft.Json; //This requires download of NuGet Newtonsoft's Json.Net

namespace MeasSpeech
{
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
    //----------------------------------------------

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*  waveIn stores wave data from input device   */
        private NAudio.Wave.WaveInEvent waveIn = new NAudio.Wave.WaveInEvent();
        NAudio.Wave.WaveFileWriter waveWriter = null;   /*  File writer for .wav format.
                                                         *  Is created when user requests to record.  */

        /*  instantiate HTTpClient  */
        private static HttpClient client = new HttpClient();

        private void Record_btn_Click(object sender, EventArgs e)
        {
            /*  Set path variables for writing file.
             *  Creates a directory called AudioFiles in ../User/Desktop    */
            var outF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundCount");
            Directory.CreateDirectory(outF);
            var outFP = Path.Combine(outF, "rec.wav");  /*  destination filename    */
            waveWriter = new NAudio.Wave.WaveFileWriter(outFP, waveIn.WaveFormat);

            /*  start   */
            waveIn.StartRecording();

            /*  Write audio buffer to file.
             *  This happens dynamically as input is being accepted.    */
            waveIn.DataAvailable += (s, a) =>
            {
                waveWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ProcessAudio_btn_Click(object sender, EventArgs e)
        {
            postRecordedFile();
            //I need to add code to account for no file being present
        }

        private void PlayBack_btn_Click(object sender, EventArgs e)
        {

        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            /*  stop  */
            waveIn.StopRecording();

            /*  Properly dispose of the file writer
             *  in order for .wav headers to be written correctly    */
            waveIn.RecordingStopped += (s, a) =>
            {
                waveWriter?.Dispose();
                waveWriter = null;
                waveIn.Dispose();
            };
        }

        
        //--------------------------------------------------------

        //This should be called by the Process Audio button
        //Sends a POST Request to the python script running flask at localhost:5000
        async static void postRecordedFile()
        {
            var pathenv = @"%USERPROFILE%\AppData\Roaming\SoundCount\rec.wav";
            var path = Environment.ExpandEnvironmentVariables(pathenv);
            //string path = @"C:\Users\Imagi\Desktop\test1.txt";
            string url = "http://localhost:5000";

            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();


            content.Add(new StreamContent(File.Open(path, FileMode.Open)), "Audio", "test1.txt");
            content.Add(new StringContent("Place string content here"), "Content-Id in the HTTP");

            //This sends the POST Request
            HttpResponseMessage result = await client.PostAsync(url, content);

            //This returns the POST response status. Should say 200 OK
            Console.WriteLine(result.ToString()); //currently writes to Console, but can be modified to output to a string variable


            //Everything below still inside the function is for reading the content
            Stream theStream = await result.Content.ReadAsStreamAsync(); //read in the response to a stream
            StreamReader theReader = new StreamReader(theStream); //create a reader for the stream

            //Parsing JSON
            string theLine = theReader.ReadToEnd(); //Gets POST Request Response Message
            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(theLine); //Deserialize the JSON into our created classes


            //Test of JSON Parse
            Console.WriteLine(jsonObject.status);
        }
        //---------------------------------------------------------------------
    }
}
