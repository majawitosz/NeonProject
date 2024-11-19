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

namespace NeonProject
{
    public unsafe partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Maja\Documents\Studia\JA\ProjektWindowsForms\NeonProject\x64\Debug\Asm.dll")]
        static extern int MyProc1(int a, int b);
        private int[] threadOptions = { 1, 2, 4, 8, 16, 32, 64 };
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

        private void applyNeon_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            Bitmap original = new Bitmap(pictureBoxOriginal.Image);
            Bitmap edges = new Bitmap(original.Width, original.Height);
            Bitmap result = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(original, 0, 0);
            }

            // Edge detection
            for (int x = 1; x < original.Width - 1; x++)
            {
                for (int y = 1; y < original.Height - 1; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    Color rightPixel = original.GetPixel(x + 1, y);
                    Color bottomPixel = original.GetPixel(x, y + 1);
                    Color diagonalPixel = original.GetPixel(x + 1, y + 1);

                    int diffH = Math.Abs(pixel.R - rightPixel.R) +
                               Math.Abs(pixel.G - rightPixel.G) +
                               Math.Abs(pixel.B - rightPixel.B);

                    int diffV = Math.Abs(pixel.R - bottomPixel.R) +
                               Math.Abs(pixel.G - bottomPixel.G) +
                               Math.Abs(pixel.B - bottomPixel.B);

                    int diffD = Math.Abs(pixel.R - diagonalPixel.R) +
                               Math.Abs(pixel.G - diagonalPixel.G) +
                               Math.Abs(pixel.B - diagonalPixel.B);

                    int maxDiff = Math.Max(Math.Max(diffH, diffV), diffD);
                    int edgeValue = MyProc1(maxDiff, 0);
                    edges.SetPixel(x, y, Color.FromArgb(edgeValue, edgeValue, edgeValue));
                }
            }

            // Smoother glow effect
            for (int x = 2; x < edges.Width - 2; x++)
            {
                for (int y = 2; y < edges.Height - 2; y++)
                {
                    Color edgePixel = edges.GetPixel(x, y);
                    if (edgePixel.R > 80)
                    {
                        // White edge
                        result.SetPixel(x, y, Color.White);

                        // Pink glow with gradual falloff
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
                                    // More gradual alpha falloff
                                    int alpha = (int)(150 * Math.Exp(-distance / 2));
                                    if (alpha > 0)
                                    {
                                        // Pink glow
                                        Color glowColor = Color.FromArgb(
                                            alpha,
                                            255, // R
                                            0,   // G
                                            255  // B
                                        );
                                        Color currentColor = result.GetPixel(newX, newY);
                                        // Blend glow with original
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

            pictureBoxNeon.Image?.Dispose();
            pictureBoxNeon.Image = result;
        }
       
    }
}
