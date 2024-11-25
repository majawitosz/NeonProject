using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NeonProject
{
    public unsafe partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Maja\Documents\Studia\JA\ProjektWindowsForms\NeonProject\x64\Debug\Asm.dll")]
        static extern int MyProc1(byte* inputPixels,
        byte* outputPixels,
        int length);
        private int[] threadOptions = { 1, 2, 4, 8, 16, 32, 64 };
        private int numberOfThreadsTests = 1; 
        //private Bitmap original, edges, result;
        public Form1()
        {
            InitializeComponent();
            InitializeTrackBar();
        }
        private void InitializeTrackBar()
        {
            // Set dynamic Maximum value
            trackBarThreads.Maximum = threadOptions.Length - 1;
            // Initialize label with default value
           threadLabel.Text = $"Number of Threads: {threadOptions[trackBarThreads.Value]}";
        }

        private void chooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select Image";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.webp|" +
                               "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                               "PNG (*.png)|*.png|" +
                               "GIF (*.gif)|*.gif|" +
                               "Bitmap (*.bmp)|*.bmp|" +
                               "TIFF (*.tiff)|*.tiff|" +
                               "WebP (*.webp)|*.webp";
            openFileDialog1.FilterIndex = 1;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imagePathTextBox.Text = openFileDialog1.FileName;
                    using (var stream = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open))
                    {
                        Image img = Image.FromStream(stream);
                        chooseImage.Image?.Dispose(); // Zwolnienie poprzedniego obrazu
                        chosenImage.Image = new Bitmap(img);
                        pictureBoxOriginal.Image?.Dispose();

                        // Przypisanie nowego obrazu do PictureBox
                        pictureBoxOriginal.Image = new Bitmap(img);
                    }
                }
                catch (Exception ex) {

                   MessageBox.Show($"Błąd podczas wczytywania pliku: {ex.Message}",
                   "Błąd wczytywania obrazu",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
    
        }

        private void trackBarThreads_Scroll(object sender, EventArgs e)
        {
            threadLabel.Text = $"Number of Threads: {threadOptions[trackBarThreads.Value]}";


        }

        // Create a struct to hold the parameters
        private unsafe struct ThreadParameters
        {
            public int StartPixel;
            public int EndPixel;
            public byte* OriginalPtr;
            public byte* EdgesPtr;

            public ThreadParameters(int start, int end, byte* original, byte* edges)
            {
                StartPixel = start;
                EndPixel = end;
                OriginalPtr = original;
                EdgesPtr = edges;
            }
        }

        private unsafe void ProcessImageSegment(object parameters)
        {
            var threadParams = (ThreadParameters)parameters;
            MyProc1(
                threadParams.OriginalPtr + (threadParams.StartPixel * 4),
                threadParams.EdgesPtr + (threadParams.StartPixel * 4),
                threadParams.EndPixel - threadParams.StartPixel);
        }

        private unsafe void applyNeon_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Bitmap original = new Bitmap(pictureBoxOriginal.Image);
            Bitmap edges = new Bitmap(original.Width, original.Height);

            BitmapData originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData edgesData = edges.LockBits(
                new Rectangle(0, 0, edges.Width, edges.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                byte* ptrOrig = (byte*)originalData.Scan0.ToPointer();
                byte* ptrEdges = (byte*)edgesData.Scan0.ToPointer();

                int totalPixels = original.Width * original.Height;
                int pixelsPerThread = totalPixels / numberOfThreadsTests;

                // Create and start threads
                var threads = new List<Thread>();

                for (int i = 0; i < numberOfThreadsTests; i++)
                {
                    int startPixel = i * pixelsPerThread;
                    int endPixel = (i == numberOfThreadsTests - 1) ? totalPixels : (i + 1) * pixelsPerThread;

                    var parameters = new ThreadParameters(startPixel, endPixel, ptrOrig, ptrEdges);
                    var thread = new Thread(ProcessImageSegment);
                    threads.Add(thread);
                    thread.Start(parameters);
                }

                // Wait for all threads to complete
                foreach (var thread in threads)
                {
                    thread.Join();
                }

                // Debug output for first few pixels
                Console.WriteLine("Output pixels:");
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"Pixel {i}: {ptrEdges[i * 4]}, {ptrEdges[i * 4 + 1]}, {ptrEdges[i * 4 + 2]}, {ptrEdges[i * 4 + 3]}");
                }
            }
            finally
            {
                original.UnlockBits(originalData);
                edges.UnlockBits(edgesData);
            }

            stopwatch.Stop();
            timeLabel.Text = $"Processing time: {stopwatch.ElapsedMilliseconds}ms using {threadOptions[trackBarThreads.Value]} threads";

            if (pictureBoxNeon.Image != null)
            {
                pictureBoxNeon.Image.Dispose();
            }
            pictureBoxNeon.Image = edges;
            original.Dispose();
        }

        // Add this method to update the number of threads
        //public void SetNumberOfThreads(int threads)
        //{
        //    numberOfThreads = Math.Max(1, Math.Min(threads, Environment.ProcessorCount));
        //}

        private void ApplyGlowEffect(Bitmap edges, Bitmap result)
        {
            for (int y = 2; y < edges.Height - 2; y++)
            {
                for (int x = 2; x < edges.Width - 2; x++)
                {
                    Color edgePixel = edges.GetPixel(x, y);
                    if (edgePixel.R > 30)
                    {
                        result.SetPixel(x, y, Color.White);

                        for (int i = -4; i <= 4; i++)
                        {
                            for (int j = -4; j <= 4; j++)
                            {
                                if (i == 0 && j == 0) continue;
                                int newX = x + i;
                                int newY = y + j;
                                if (newX >= 0 && newX < result.Width && newY >= 0 && newY < result.Height)
                                {
                                    double distance = Math.Sqrt(i * i + j * j);
                                    int alpha = (int)(150 * Math.Exp(-distance / 2));
                                    if (alpha > 0)
                                    {
                                        Color glowColor = Color.FromArgb(alpha, 255, 0, 255);
                                        Color currentColor = result.GetPixel(newX, newY);
                                        result.SetPixel(newX, newY, Color.FromArgb(
                                            Math.Min(255, ((currentColor.R * (255 - alpha) + glowColor.R * alpha) / 255)),
                                            Math.Min(255, ((currentColor.G * (255 - alpha) + glowColor.G * alpha) / 255)),
                                            Math.Min(255, ((currentColor.B * (255 - alpha) + glowColor.B * alpha) / 255))
                                        ));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TestsButton_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }
            double[] averageTimes = new double[threadOptions.Length];
            for (int i=0; i< threadOptions.Length; i++)
            {
                Label currentLabel = this.Controls.Find($"t{threadOptions[i]}Asm", true).FirstOrDefault() as Label;
                numberOfThreadsTests = threadOptions[i];
                long totalTime = 0;
                for (int j=0; j < 10; j++)
                {

                   
                    var stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();

                    applyNeon_Click(null, null);

                    stopwatch.Stop();
                    totalTime += stopwatch.ElapsedMilliseconds;

                    Application.DoEvents();
                }
                double averageTime = totalTime / 10.0;
                averageTimes[i] = averageTime;
                currentLabel.Text = $"{threadOptions[i]} threads: {averageTime:F2}ms";
            }
        }
    }
}
