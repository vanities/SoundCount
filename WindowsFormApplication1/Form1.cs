using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Http; //http requests
using System.Collections.Generic;
using Newtonsoft.Json; //This requires download of NuGet Newtonsoft's Json.Net
using System.Diagnostics;
using NAudio.Wave;

namespace MeasSpeech
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient postclient = new HttpClient();

        public Form1()
        {

            InitializeComponent();

        }

        /*  waveIn stores wave data from input device   */
        private WaveInEvent waveIn = new WaveInEvent();
        WaveFileWriter waveWriter = null;   /*  File writer for .wav format.
                                                         *  Is created when user requests to record.  */

        WaveOutEvent wo = new WaveOutEvent();

        /*  instantiate HTTpClient  */
        private static HttpClient client = new HttpClient();
        System.Diagnostics.Process backend = new System.Diagnostics.Process();

        private void Record_btn_Click(object sender, EventArgs e)
        {
            Stop_btn.Show();
            Record_btn.Hide();
            PlayBack_btn.Hide();
            PlaybackStop_btn.Hide();
            /*  Set path variables for writing file.
             *  Creates a directory called AudioFiles in ../User/Desktop    */
            var outF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundCount");
            Directory.CreateDirectory(outF);
            var outFP = Path.Combine(outF, "rec.wav");  /*  destination filename    */
            //try { waveWriter = new WaveFileWriter(outFP, waveIn.WaveFormat); }
            //catch { waveWriter?.Dispose();
            //    waveWriter = null;
            //    waveWriter = new WaveFileWriter(outFP, waveIn.WaveFormat);
            //}
            waveWriter = new WaveFileWriter(outFP, waveIn.WaveFormat);
            waveIn = new WaveInEvent();

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
            Stop_btn.Hide();
            PlaybackStop_btn.Hide();
            PlayBack_btn.Hide();

            //start python process on load.
            //call backend application

            backend.StartInfo.FileName = @"C:\Users\Imagi\AppData\Local\Programs\Python\Python36\python.exe";
            backend.StartInfo.Arguments = @"api.py"; //argument
            backend.StartInfo.CreateNoWindow = true; //Set to true so Python window does not appear upon backend.Start()
            backend.StartInfo.UseShellExecute = false;
            


            backend.Start();

            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            backend.Kill(); //When program is closed, also close python backend
        }

        private void ProcessAudio_btn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            postRecordedFile();
            //I need to add code to account for no file being present
            
        }

        private void PlayBack_btn_Click(object sender, EventArgs e)
        {
            PlaybackStop_btn.Show();
            //file path of audio file to play
            var outF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundCount");
            Directory.CreateDirectory(outF);
            var outFP = Path.Combine(outF, "rec.wav");  /*  destination filename    */

            //create audiofile object
            var af = new AudioFileReader(outFP);
            wo.PlaybackStopped += (s, a) => { { wo.Dispose(); af.Dispose(); } };
            try { wo.Init(af); } catch { wo.Play(); }
            wo.Play();
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            PlayBack_btn.Show();
            Record_btn.Show();
            Stop_btn.Hide();
            //Stop recording
            waveIn.StopRecording();

            /*  Properly dispose of the file writer
             *  in order for .wav headers to be written correctly    */
            waveWriter?.Dispose();
            waveWriter = null;
            waveIn.Dispose();
        }

        //--------------------------------------------------------

        //This should be called by the Process Audio button
        //Sends a POST Request to the python script running flask at localhost:5000
        async static void postRecordedFile()
        {
            var pathenv = @"C:\Users\Imagi\AppData\Roaming\SoundCount\rec.wav";
            //var pathenv = @" % USERPROFILE%\AppData\Roaming\SoundCount\rec.wav";
            var path = Environment.ExpandEnvironmentVariables(pathenv);
            string url = "http://localhost:5000";

            
            MultipartFormDataContent form = new MultipartFormDataContent();


            form.Add(new ByteArrayContent(File.ReadAllBytes(pathenv)), "file", "rec.wav");
            

            //This sends the POST Request
            HttpResponseMessage result = await postclient.PostAsync(url, form);

            //This returns the POST response status. Should say 200 OK
            //Console.WriteLine(result.ToString()); //currently writes to Console, but can be modified to output to a string variable


            //Everything below still inside the function is for reading the content
            Stream theStream = await result.Content.ReadAsStreamAsync(); //read in the response to a stream
            StreamReader theReader = new StreamReader(theStream); //create a reader for the stream

            //Parsing JSON
            string theLine = theReader.ReadToEnd(); //Gets POST Request Response Message
            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(theLine); //Deserialize the JSON into our created classes
            updateText(jsonObject); //this updates the View Data textbox with the relevant output
            

        }

        //This outputs data to the rich text box in 
        private static void updateText(RootObject temp)
        {
            Console.WriteLine(temp.meta.text.ToString());
            var thisForm = Form.ActiveForm as Form1;
            string output = "Status: " + temp.status + "\nCount: " + temp.count + "\nWords: ";
            foreach (var outer in temp.meta.text)
            {
                foreach (var word in outer)
                {
                    output += word[0] + ' ';
                }
            }
            output += "\nAge: " + temp.meta.age + "\nGender: " + temp.meta.gender + "\nDialect: " + temp.meta.dialect;
            output += "\nDuration: " + temp.meta.duration + "s";

            thisForm.richTextBox1.Text = output; //display contents
            thisForm.Cursor = Cursors.Default;
        }


        private void PlaybackStop_btn_Click(object sender, EventArgs e)
        {
            wo.Stop();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //---------------------------------------------------------------------
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
    //----------------------------------------------


}
