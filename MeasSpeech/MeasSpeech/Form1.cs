using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;

namespace MeasSpeech
{
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
            var outF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AudioFiles");
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
    }
}
