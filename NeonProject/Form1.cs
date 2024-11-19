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
        static extern int MyProc1(int a, int b);
        private int[] threadOptions = { 1, 2, 4, 8, 16, 32, 64 };
        private Bitmap original, edges, result;
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
        private void ProcessImageSection(object parameters)
        {
            var (startY, endY, original, edges, result) = ((int, int, BitmapData, BitmapData, BitmapData))parameters;

            int bytesPerPixel = 4;
            int stride = original.Stride;

            byte* originalPtr = (byte*)original.Scan0;
            byte* edgesPtr = (byte*)edges.Scan0;

            for (int y = startY; y < endY; y++)
            {
                if (y < 1 || y >= original.Height - 1) continue;

                for (int x = 1; x < original.Width - 1; x++)
                {
                    int pos = y * stride + x * bytesPerPixel;
                    int rightPos = y * stride + (x + 1) * bytesPerPixel;
                    int bottomPos = (y + 1) * stride + x * bytesPerPixel;
                    int diagPos = (y + 1) * stride + (x + 1) * bytesPerPixel;

                    int diffH = Math.Abs(originalPtr[pos + 2] - originalPtr[rightPos + 2]) +
                               Math.Abs(originalPtr[pos + 1] - originalPtr[rightPos + 1]) +
                               Math.Abs(originalPtr[pos] - originalPtr[rightPos]);

                    int diffV = Math.Abs(originalPtr[pos + 2] - originalPtr[bottomPos + 2]) +
                               Math.Abs(originalPtr[pos + 1] - originalPtr[bottomPos + 1]) +
                               Math.Abs(originalPtr[pos] - originalPtr[bottomPos]);

                    int diffD = Math.Abs(originalPtr[pos + 2] - originalPtr[diagPos + 2]) +
                               Math.Abs(originalPtr[pos + 1] - originalPtr[diagPos + 1]) +
                               Math.Abs(originalPtr[pos] - originalPtr[diagPos]);

                    int maxDiff = Math.Max(Math.Max(diffH, diffV), diffD);
                    int edgeValue = MyProc1(maxDiff, 0);

                    edgesPtr[pos] = (byte)edgeValue;
                    edgesPtr[pos + 1] = (byte)edgeValue;
                    edgesPtr[pos + 2] = (byte)edgeValue;
                    edgesPtr[pos + 3] = 255;
                }
            }
        }

        private void applyNeon_Click(object sender, EventArgs e)
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
            Bitmap result = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(original, 0, 0);
            }

            int threadCount = threadOptions[trackBarThreads.Value];
            int heightPerThread = original.Height / threadCount;

            BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData edgesData = edges.LockBits(new Rectangle(0, 0, edges.Width, edges.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                int startY = i * heightPerThread;
                int endY = (i == threadCount - 1) ? original.Height : (i + 1) * heightPerThread;

                threads[i] = new Thread(ProcessImageSection);
                threads[i].Start((startY, endY, originalData, edgesData, resultData));
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            original.UnlockBits(originalData);
            edges.UnlockBits(edgesData);
            result.UnlockBits(resultData);

            // Apply glow effect (can be similarly optimized if needed)
            ApplyGlowEffect(edges, result);

            stopwatch.Stop();
            timeLabel.Text = $"Processing time: {stopwatch.ElapsedMilliseconds}ms using {threadOptions[trackBarThreads.Value]} threads";

            pictureBoxNeon.Image?.Dispose();
            pictureBoxNeon.Image = result;
        }

        private void ApplyGlowEffect(Bitmap edges, Bitmap result)
        {
            for (int y = 2; y < edges.Height - 2; y++)
            {
                for (int x = 2; x < edges.Width - 2; x++)
                {
                    Color edgePixel = edges.GetPixel(x, y);
                    if (edgePixel.R > 80)
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

    }
}
