using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
using System.IO;
using System.Drawing.Drawing2D;
//using System.namespace;

namespace WindowsFormsApp1
{


    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            saveimg.Enabled = false; // disables save button in the beginning
            int[] combo = new int[9];
            numericUpDown1.Maximum = 90;
            numericUpDown1.Minimum = 10;
            numericUpDown2.Maximum = Int32.MaxValue;
            numericUpDown2.Minimum = 1;
            numericUpDown2.Value = 1000;
        }

        /*******************************************
         uploadimg_Click
         Button event that allows a user to add an image of their choice 
         into the picture box above the button
        *******************************************/
        public void uploadimg_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog(); // opens a new dialog for images

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box 
                string path = Path.GetDirectoryName(open.FileName) + "//images";

                // checks if images folder exists to create the mosaic
                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Folder \"images\" does not exist. \nPlease add a folder with the name \"images\" with desired images.");
                    return;
                }

                sourceimg.SizeMode = PictureBoxSizeMode.StretchImage; // gets the image to fit in picture box
                sourceimg.Image = new Bitmap(open.FileName);          // sets the image in the picture box
                Globals.FILE_NAME = Path.GetFileName(open.FileName);  // saves the file name
                Globals.FILE_PATH = Path.GetDirectoryName(open.FileName); // saves the file path without the file name
                begin.Enabled = true;
            }

        }

        /*******************************************
         saveimg_Click
         Button event to save the mosaic from the above picture box
        *******************************************/
        private void saveimg_Click(object sender, EventArgs e)
        {
            // from http://csharp-me.blogspot.com/2014/01/load-and-save-image-in-c.html
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image Files(*.jpg, *.png, *.tiff, *.bmp, *.gif) | *.jpg; *.png; *.tiff; *.bmp; *.gif";

            dialog.Title = "Save an image";
            dialog.AddExtension = true;
            dialog.DefaultExt = "bmp";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = Globals.FILE_PATH + "\\" + Globals.FILE_NAME;
                if (dialog.FileName == path) // checks if the file is in use
                {
                    MessageBox.Show("File is currently in use \nChange the name of the file");
                    return;
                }
                string fName = dialog.FileName;
                showmosaic.Image.Save(fName, ImageFormat.Bmp);
            }
        }

        /*******************************************
         begin_Click
         Button event that calls createM()
         disables other buttons before creating the mosaic
         reenables the buttons after finishing the product
        *******************************************/
        private void begin_Click(object sender, EventArgs e)
        {
            uploadimg.Enabled = false;      // disables button
            begin.Enabled = false;          // disables button
            saveimg.Enabled = false;        // disables button
            numericUpDown1.Enabled = false; // disables Elitism numeric
            numericUpDown2.Enabled = false; // disables Generations numeric

            createM();

            uploadimg.Enabled = true;       // enables button
            begin.Enabled = true;           // enables button
            saveimg.Enabled = true;         // enables button
            numericUpDown1.Enabled = true;  // enables Elitism numeric
            numericUpDown2.Enabled = true;  // enables Generations numeric
        }

        /*******************************************
         createM
         initializes the values from source image and from images folder
         grabs the images folder and puts them in a bitmap array
         initializes pixel values for source and stock images
         calls the generate function to start algorithm
        *******************************************/
        private void createM()
        {
            //throw new NotImplementedException();

            int size = Globals.SIZE;
            int count = (int)Pow(size, 2);
            string[] files = Directory.GetFiles(Globals.FILE_PATH + "//images", "*", SearchOption.AllDirectories);
            int stockSize = 0;

            for (int i = 0; i < files.Length; i++)
            {

                if (Path.GetExtension(files[i]) == ".jpg" || Path.GetExtension(files[i]) == ".jpeg" || Path.GetExtension(files[i]) == ".png"
                    || Path.GetExtension(files[i]) == ".gif" || Path.GetExtension(files[i]) == ".bmp")
                {
                    stockSize++;
                }
            }

            Bitmap[] mySource = new Bitmap[count];
            Bitmap[] stocks = new Bitmap[stockSize];

            InitBMP(mySource);
            getStockIMG(stocks, files);

            uint[,,] sourceScore = new uint[count, 5, 3];
            uint[,,] stockScore = new uint[stockSize, 5, 3];

            IMGValues(sourceScore, mySource, count);
            IMGValues(stockScore, stocks, stockSize);

            generate(mySource, stocks, sourceScore, stockScore);
            // generate(mySource, mySource, sourceScore, sourceScore);
            //  Compare(sourceScore, stockScore, good, stockSize);

            // DisplayIMG(stocks,good);
            // DisplayIMG(mySource,good);
            return;
        }

        /*******************************************
           InitBMP
           gets the sections of the source image based on the radio buttons
        *******************************************/
        public void InitBMP(Bitmap[] source)
        {
            Bitmap thesource = new Bitmap(sourceimg.Image);
            int size = Globals.SIZE;
            int height = (thesource.Height) / size;
            int width = (thesource.Width) / size;

            int total = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Rectangle section = new Rectangle(width * j, height * i, width, height);
                    source[total] = CropImage(thesource, section);

                    total++; // for the source array to keep order by rows
                }

            }
        }

        /********************************************
            IMGValues
            inputs - values, mySource, total
            outputs - values is a filled array
                      mySource doesn't change
                      total doesn't change
            Grabs the values of top left, top right, middle
            bottom left, and bottom right
        ********************************************/
        private void IMGValues(uint[,,] values, Bitmap[] mySource, int total)
        {
            //MessageBox.Show("img vals");
            //int total = (int) Pow( size, 2);

            for (int i = 0; i < total; i++)
            {
                Bitmap myBMP = mySource[i];
                int width = myBMP.Width - 1;
                int height = myBMP.Height - 1;

                int sectionW = width / 3;
                int sectionH = height / 3;


                Rectangle topL = new Rectangle(0, 0, sectionW, sectionH);
                Rectangle topR = new Rectangle(sectionW * 2, 0, sectionW, sectionH);
                Rectangle bottomL = new Rectangle(0, sectionH * 2, sectionW, sectionH);
                Rectangle bottomR = new Rectangle(sectionW * 2, sectionH * 2, sectionW, sectionH);
                Rectangle Middle = new Rectangle(sectionW, sectionH, sectionW, sectionH);

                Bitmap topLeft = CropImage(myBMP, topL);
                Bitmap topRight = CropImage(myBMP, topR);
                Bitmap bottomLeft = CropImage(myBMP, bottomL);
                Bitmap bottomRight = CropImage(myBMP, bottomR);
                Bitmap Center = CropImage(myBMP, Middle);

                AVGvalues(values, topLeft, i, 0);
                AVGvalues(values, topRight, i, 1);
                AVGvalues(values, Center, i, 2);
                AVGvalues(values, bottomLeft, i, 3);
                AVGvalues(values, bottomRight, i, 4);

            }
            // MessageBox.Show("img vals end");
            return;
        }

        // source of CropImage: https://stackoverflow.com/questions/9484935/how-to-cut-a-part-of-image-in-c-sharp
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image

            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        /********************************************
            AVGvalues
            input - images, image, imgNum, spot
            output - images gets average color value for that spot
            Uses the section of source
        ********************************************/
        public void AVGvalues(uint[,,] images, Bitmap image, int imgNum, int spot)
        {
            //MessageBox.Show("start avgvals");

            int imgW = image.Width; // grabs the width of the image
            int imgH = image.Height; // grabs the height of the image
            int area = imgH * imgW;  // what will be divided to get avg for 

            uint red = 0, green = 0, blue = 0; // initializes the color


            for (int i = 0; i < imgH; i++)
            {
                for (int j = 0; j < imgW; j++)
                {
                    red += image.GetPixel(j, i).R;
                    green += image.GetPixel(j, i).G;
                    blue += image.GetPixel(j, i).B;
                }
            }
            red = red / (uint)area;
            green = green / (uint)area;
            blue = blue / (uint)area;

            images[imgNum, spot, 0] = red;
            images[imgNum, spot, 1] = green;
            images[imgNum, spot, 2] = blue;

            //  MessageBox.Show("end avgvals");

            return;
        }

        /********************************************
            generate
            inputs - source, stock, sourceVal, stockVal
            outputs - new image to showmosaic picture box
            This function uses the genetic algorithm try and piece
            together the source image with all the stock photos.
            The best generation is brought to the top with 
            a simple bubble sort.
        ********************************************/
        public void generate(Bitmap[] source, Bitmap[] stock, uint[,,] sourceVal, uint[,,] stockVal)
        {
            // MessageBox.Show("In Generate");
            int mySize = Globals.SIZE; // amount of source images per row
            int totalimages = (int)Pow(mySize, 2); // amount of source images 
            int popSize = Globals.population; // population size
            int TotalGens = Globals.Generations; // total amount of generations
            int stockSize = stock.Length; // stock images size

            progressBar1.Minimum = 0;
            progressBar1.Maximum = TotalGens;


            Bitmap[] BestInGen = new Bitmap[totalimages]; // gets the best person in population
            int[,] populationLoc = new int[popSize, totalimages]; // holds the values of stock

            Random rnd = new Random();
            int currentGen = 0;

            int[] sourcegnome = new int[totalimages];
            for (int i = 0; i < totalimages; i++)
            {
                sourcegnome[i] = i;
            }
            individual mySource = new individual(sourceVal);

            // initializes the population array with random stock images
            for (int i = 0; i < popSize; i++)
            {
                for (int j = 0; j < totalimages; j++)
                {
                    int myNum = rnd.Next(0, stockSize);
                    populationLoc[i, j] = myNum;
                }
            }

            // SourceDom(domCol); // gets the dominant color on source images
            // List<individual> myPop = new List<individual>();
            individual[] myPop = new individual[popSize];
            bool found = false;
            int[] gnome = new int[totalimages];
            for (int i = 0; i < popSize; i++)
            {

                for (int k = 0; k < totalimages; k++)
                {
                    gnome[k] = populationLoc[i, k];
                }

                myPop[i] = (new individual(gnome, sourceVal, stockVal));
            }
            int elite = Globals.elitism;
            int weak = 100 - elite;
            while (!found && currentGen < TotalGens)
            {
                // sort
                sort();
                if (Globals.Generations <= 500)
                {
                    if (currentGen % 10 == 0)
                    {
                        for (int i = 0; i < totalimages; i++)
                        {
                            BestInGen[i] = stock[myPop[0].chromosome[i]];
                        }
                        DisplayIMG(BestInGen);
                    }
                }
                else
                {
                    if (currentGen % 100 == 0)
                    {
                        for (int i = 0; i < totalimages; i++)
                        {
                            BestInGen[i] = stock[myPop[0].chromosome[i]];
                        }
                        DisplayIMG(BestInGen);
                    }
                }
                progressBar1.Value = currentGen;
                progressBar1.Refresh();
                if (myPop[0].chromscore <= 100)
                {
                    progressBar1.Value = TotalGens;
                    found = true;
                    break;
                }
                List<individual> nextGen = new List<individual>();
                // elitism, variable of user choice of population goes to the next generation
                int s = (elite * popSize) / 100;
                for (int i = 0; i < s; i++)
                {
                    nextGen.Add(new individual(myPop[i]));
                }
                // rest of the population will mate
                // produces offspring from the best 20
                // For those 20 are the fittest, the rest "die"
                s = (weak * popSize) / 100;
                for (int i = 0; i < s; i++)
                {
                    int r = rnd.Next(0, 20);
                    individual parent1 = new individual(myPop[r]);
                    r = rnd.Next(0, 20);
                    individual parent2 = new individual(myPop[r]);
                    individual child = parent1.mate(parent2, sourceVal, stockVal, stockSize);
                    nextGen.Add(new individual(child));
                }
                //nextGen.CopyTo(myPop,totalimages);
                nextGen.CopyTo(myPop);

                currentGen++;

            }
            progressBar1.Value = TotalGens;
            progressBar1.Refresh();

            // Bubble sort
            void sort()
            {
                for (int i = 0; i < popSize - 1; i++)
                {
                    for (int j = 0; j < popSize - i - 1; j++)
                    {
                        if (myPop[j].chromscore > myPop[j + 1].chromscore)
                            swap(j, j + 1);
                    }

                }
                return;
            }

            // swaps two individuals
            void swap(int who, int what)
            {
                individual temp = new individual(myPop[who]);
                myPop[who] = myPop[what];
                myPop[what] = temp;
            }

        }

        public class individual
        {
            public int[] chromosome; // stock image locations

            public uint[,] fitness;  // holds absolute difference between source and stock images

            public double[] wholeIMGscore; // holds the whole sub-image score

            public double chromscore; // holds the whole chromosomes score
            /********************************************
            individual - source constructor
            initializes the source image individual and sets most values to 0
            ********************************************/
            public individual(uint[,,] sourceVal)
            {
                int total = (int)Pow(Globals.SIZE, 2);
                chromosome = new int[total];
                uint[,,] temp = new uint[total, 5, 3];
                fitness = new uint[total, 5];
                wholeIMGscore = new double[total];

                for (int i = 0; i < total; i++)
                {
                    chromosome[i] = i;

                }
                for (int j = 0; j < total; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        temp[j, k, 0] = sourceVal[j, k, 0];
                        temp[j, k, 1] = sourceVal[j, k, 1];
                        temp[j, k, 2] = sourceVal[j, k, 2];
                    }
                }

                for (int i = 0; i < total; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        fitness[i, j] = 0;
                    }
                    wholeIMGscore[i] = 0;
                }

            }
            /********************************************
            individual - chromosome constructor
            initializes the given chromosome comparing with source image
            ********************************************/
            public individual(int[] newchromosome, uint[,,] sourceVal, uint[,,] stockVal)
            {
                int total = (int)Pow(Globals.SIZE, 2);
                chromosome = new int[total];
                uint[,,] temp = new uint[total, 5, 3];
                fitness = new uint[total, 5];
                wholeIMGscore = new double[total];

                for (int i = 0; i < total; i++)
                {
                    chromosome[i] = newchromosome[i];
                    wholeIMGscore[i] = 0;
                }
                for (int j = 0; j < total; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        int spot = newchromosome[j];
                        temp[j, k, 0] = stockVal[spot, k, 0];
                        temp[j, k, 1] = stockVal[spot, k, 1];
                        temp[j, k, 2] = stockVal[spot, k, 2];
                    }
                }

                Calc_fitness(sourceVal, temp);

            }
            /********************************************
            individual - copy constructor
            copys a given individual
            ********************************************/
            public individual(individual copy)
            {
                int total = (int)Pow(Globals.SIZE, 2);
                chromosome = new int[total];
                uint[,,] temp = new uint[total, 5, 3];
                fitness = new uint[total, 5];
                wholeIMGscore = new double[total];

                for (int i = 0; i < total; i++)
                {
                    chromosome[i] = copy.chromosome[i];

                    wholeIMGscore[i] = copy.wholeIMGscore[i];
                }
                chromscore = copy.chromscore;
                for (int i = 0; i < total; i++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        fitness[i, k] = copy.fitness[i, k];
                    }
                }

            }
            /********************************************
            individual mate
            given the parents, creates a new child chromosome
            by taking parts from the parents at random
            then returns the child
            ********************************************/
            public individual mate(individual parent2, uint[,,] source, uint[,,] stock, int stockSize)
            {
                int total = (int)Pow(Globals.SIZE, 2);
                Random rnd = new Random();
                Random mutated = new Random();

                int[] child_chrom = new int[total];

                for (int i = 0; i < total; i++)
                {

                    double p = rnd.Next(0, 100) / 100.0;

                    // if probability is less than 0.45
                    // insert gene from parent 1
                    if (p < 0.45)
                        child_chrom[i] = chromosome[i];
                    // if probability is less than 0.90
                    // insert gene from parent 2
                    else if (p < 0.90)
                        child_chrom[i] = parent2.chromosome[i];
                    // otherwise add a new random gene
                    else
                        child_chrom[i] = mutated.Next(0, stockSize);
                }

                individual child = new individual(child_chrom, source, stock);

                return child;
            }
            /********************************************
            Calc_fitness
            calculates the fitness of each individual image in the chromosome,
            and calculates the whole chromosomes fitness by adding everything together
            ********************************************/
            public void Calc_fitness(uint[,,] source, uint[,,] temp)
            {
                int total = (int)Pow(Globals.SIZE, 2);
                //int sender = 0;
                uint red = 0, green = 0, blue = 0;
                uint totRed = 0, totGreen = 0, totBlue = 0;

                for (int i = 0; i < total; i++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        totRed += (uint)Abs(source[i, k, 0] - temp[i, k, 0]);
                        totGreen += (uint)Abs(source[i, k, 1] - temp[i, k, 1]);
                        totBlue += (uint)Abs(source[i, k, 2] - temp[i, k, 2]);

                        red = (uint)Abs(source[i, k, 0] - temp[i, k, 0]);
                        green = (uint)Abs(source[i, k, 1] - temp[i, k, 1]);
                        blue = (uint)Abs(source[i, k, 2] - temp[i, k, 2]);

                        fitness[i, k] = red + blue + green;
                    }
                    totRed /= 5;
                    totGreen /= 5;
                    totBlue /= 5;

                    wholeIMGscore[i] = (totRed + totGreen + totBlue) * 1.0;

                    totRed = 0;
                    totGreen = 0;
                    totBlue = 0;

                    chromscore += wholeIMGscore[i];
                }
                chromscore /= total;
                return;
            }

        }

        public void DisplayIMG(Bitmap[] images)
        {
            // Image thesource = sourceimg.Image;
            Bitmap source = new Bitmap(sourceimg.Image); // out of memory exception
            int sWidth = source.Width;
            int sHeight = source.Height;

            int size = Globals.SIZE;
            int width = sWidth / size;
            int height = sHeight / size;
            Bitmap empty = new Bitmap((int)width * size, (int)height * size);

            Graphics g = Graphics.FromImage(empty);
            int here = 0;


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    g.DrawImage(images[here], width * j, height * i);
                    here++;
                }

            }
            showmosaic.SizeMode = PictureBoxSizeMode.StretchImage;
            showmosaic.Image = empty;
            showmosaic.Refresh();
            return;
        }

        // source: https://stackoverflow.com/questions/991587/how-can-i-crop-scale-user-images-so-i-can-display-fixed-sized-thumbnails-without
        // Slightly modified to fix images
        private Bitmap ScaleImage(Image oldImage)
        {
            int size = Globals.SIZE;
            string path = Globals.FILE_PATH + "\\" + Globals.FILE_NAME;
            Image source = Image.FromFile(path);
            int width2 = source.Width / size;
            int height2 = source.Height / size;


            Bitmap newImage = new Bitmap(width2, height2);
            Graphics g = Graphics.FromImage(newImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(oldImage, 0, 0, newImage.Width, newImage.Height);
            return newImage;
        }

        /********************************************
         getStockIMG
         inputs: files - the string of files/images in the images folder
                 newimages - empty array for the images
         outups - newimages to have the images from files
        ********************************************/
        public void getStockIMG(Bitmap[] newimages, string[] files)
        {

            int length = files.Length; // gets how many stock images there are
            int next = 0;
            for (int i = 0; i < length; i++)
            {
                if (Path.GetExtension(files[i]) == ".jpg" || Path.GetExtension(files[i]) == ".jpeg" || Path.GetExtension(files[i]) == ".png"
                    || Path.GetExtension(files[i]) == ".gif" || Path.GetExtension(files[i]) == ".bmp")
                {
                    Image myIMG = Image.FromFile(files[i]);
                    Bitmap myBMP = ScaleImage(myIMG);
                    newimages[next] = myBMP;
                    next++;
                }

            }
        }

        // sets size to 4
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Globals.SIZE = 4;
        }

        // sets size to 16
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Globals.SIZE = 16;
        }

        // sets size to 64
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Globals.SIZE = 64;
        }

        // sets Generations to whatever inputted value is
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Globals.Generations = (int)numericUpDown2.Value;
        }

        // sets Elitism value to whatever inputted value is
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Globals.elitism = (int)numericUpDown1.Value;
        }
    }
}

/********************************************
    Global variables to hold values from other handlers
********************************************/
public static class Globals
{
    public static int SIZE = 4;
    public static string FILE_PATH = "Output.txt";
    public static string FILE_NAME = "Output.txt";
    public static int population = 40;
    public static int Generations = 1000;
    public static int elitism = 10;
}

