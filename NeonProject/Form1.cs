using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeonProject
{
    public partial class Form1 : Form
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



        private void button1_Click(object sender, EventArgs e)
        {
            int x = 5, y = 3;
            int retVal = MyProc1(x, y);

            // Create or update label to show result
            Label resultLabel = new Label();
            resultLabel.Text = $"Moja pierwsza wartość obliczona w asm to: {retVal}";
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(10, 50);
            this.Controls.Add(resultLabel);

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
