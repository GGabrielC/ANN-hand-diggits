using ANN;
using ExtensionMethods;
using MNIST.IO;
using MNIST_SOLVER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class App : Form
    {
        Network cnn = new MNISTNetworkBuilder().build();
        byte[] labels;
        byte[][,] images;


        public App()
        {
            InitializeComponent();

            loadMNISTDataImgs();
            ImgIndex.Minimum = ImgIndex.Value = 0;
            ImgIndex.Maximum = labels.Length;
            ImgIndex_ValueChanged(null, null);
        }

        private void App_Load(object sender, EventArgs e)
        {

        }

        private void ImgIndex_ValueChanged(object sender, EventArgs e)
        {
            var index = (int)ImgIndex.Value;
            var netLabel = cnn.classify(images[index]);
            DiggitLabel.Text = "Label: {0}, {1} expected".format( netLabel, labels[index]);
            drawDiggit();
        }

        private void button_TrainNet_Click(object sender, EventArgs e)
            => cnn.train(images, labels, (int)nudTrainIterations.Value, (int)nudTrainIterationSize.Value);

        private void buttun_Draw_Click(object sender, EventArgs e)
            => drawDiggit();

        private void loadMNISTDataImgs()
        {
            String FolderPath = "C:\\Users\\Gabriel\\source\\repos\\ANN_MNIST\\ANN_MNIST\\MNIST_solver\\Input Files MNIST\\";
            String imagesPath = FolderPath + "t10k-images-idx3-ubyte.gz";
            String labelsPath = FolderPath + "t10k-labels-idx1-ubyte.gz";
            labels = FileReaderMNIST.LoadLabel(labelsPath);
            images = FileReaderMNIST.LoadImages(imagesPath).ToArray();
        }

        private void drawDiggit()
            => DiggitImage.Image = arrayToBitmap(images[int.Parse(ImgIndex.Text)]);
        
        private Bitmap arrayToBitmap(byte[,] data)
        {
            Bitmap bmp = new Bitmap(data.GetLength(0), data.GetLength(1));
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    bmp.SetPixel(j, i, Color.FromArgb( data[i, j], data[i, j], data[i, j]));
            return bmp;
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
