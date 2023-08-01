using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Visiott_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Stopwatch stopWatch = new Stopwatch();
        public void btn1_Click(object sender, EventArgs e)
        {
            // OpenFileDialog nesnesi oluşturun
            OpenFileDialog ofd = new OpenFileDialog();

            // Filtre olarak sadece resim dosyalarını seçebilmeyi sağlayın
            ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            // Kullanıcının fotoğraf seçmesini sağlayın
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Seçilen fotoğrafı PictureBox'a yükleyin
                Image nosizedImage = Image.FromFile(ofd.FileName);
                pictureBox1.Image = Image.FromFile(ofd.FileName);

            }
        }


        private Bitmap BinaryYap(Bitmap image)
        {
            Bitmap gri = GriYap(image);
            //int temp = 0;
            //int esik = 100;
            //Color renk;

            //for(int i = 0;i< gri.Height-1;i++)
            //{
            //    for(int j = 0;j<gri.Width-1;j++) 
            //    {
            //        temp = gri.GetPixel(j,i).R;

            //        if(temp < esik)
            //        {
            //            renk = Color.FromArgb(0,0,0);
            //            gri.SetPixel(j, i, renk);
            //        }
            //        else
            //        {
            //            renk = Color.FromArgb(255, 255, 255);
            //            gri.SetPixel(j, i, renk);
            //        }
            //    }
            //}
            return gri;
        }
        private Bitmap GriYap(Bitmap bitmap)
        {
            ///* // Fotoğrafı gri yapmak için Bitmap nesnesi oluşturun
            Bitmap grayScale = (Bitmap)pictureBox1.Image;

            // // Her piksel için R, G ve B değerlerini okuyun
            // for (int i = 0; i < grayScale.Width; i++)
            // {
            //     for (int j = 0; j < grayScale.Height; j++)
            //     {
            //         Color c = grayScale.GetPixel(i, j);
            //         int gray = (int)((c.R + c.G + c.B) / 3);

            //         // Her piksel için yeni gri değerleri atayın
            //         grayScale.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
            //     }
            // }        
            return grayScale;
        }

        private Bitmap ApplySobelFilter(Bitmap image)
        {
            //// Sobel filtresi matrisleri
            //int[,] Gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            //int[,] Gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int width = image.Width;
            int height = image.Height;

            Bitmap edges = new Bitmap(width, height);

            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        double new_x = 0, new_y = 0;

            //        for (int i = -1; i <= 1; i++)
            //        {
            //            for (int j = -1; j <= 1; j++)
            //            {
            //                if ((x + i >= 0) && (x + i < width) && (y + j >= 0) && (y + j < height))
            //                {
            //                    Color c = image.GetPixel(x + i, y + j);
            //                    int gray = (int)((c.R + c.G + c.B) / 3);

            //                    new_x += gray * Gx[i + 1, j + 1];
            //                    new_y += gray * Gy[i + 1, j + 1];
            //                }
            //            }
            //        }

            //        double gradient = Math.Sqrt((new_x * new_x) + (new_y * new_y));
            //        int intensity = Clamp((int)gradient);

            //        edges.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
            //    }
            //}

            return edges;
        }
   
        private void Uygulabtn_Click(object sender, EventArgs e)
        {
            //if (comboBox1.Text == "Make a grey")
            //{
            //    Bitmap image = new Bitmap(pictureBox1.Image);
            //    Bitmap gri = GriYap(image);
            //    pictureBox5.Image = gri;
            //}
            //if (comboBox1.Text == "Make a binary photo")
            //{
            //    /*Bitmap image = new Bitmap(pictureBox1.Image);
            //    Bitmap gri = BinaryYap(image);
            //    pictureBox5.Image = gri;

            //    pictureBox5.Image = pictureBox1.Image;
            //}
            //if (comboBox1.Text == "Find edge")
            //{
            //    Bitmap image = new Bitmap(pictureBox1.Image);
            //    Bitmap gri = ApplySobelFilter(image);
            //    pictureBox5.Image = gri;
            //}
        }
        private void Form1_Load(object sender, EventArgs e)
        {                 // Seçilen fotoğrafı PictureBox'a yükleyin
            //pictureBox1.Image = Image.FromFile("\\Users\\Lenovo\\Pictures\\temp.jpg");
        }
        private Bitmap CropImage(Bitmap image, int topCrop, int bottomCrop, int sideCrop)
        {
            int width = image.Width - (sideCrop * 2);
            int height = image.Height - (topCrop + bottomCrop);

            Rectangle rect = new Rectangle(sideCrop, topCrop, width, height);

            Bitmap croppedImage = image.Clone(rect, image.PixelFormat);
            pictureBox1.Image = croppedImage;
            return croppedImage;
        }
        public int CountWhiteSquares(Bitmap cropimage, Bitmap originalimage, int averagered, int averagegreen, int averageblue, int secilialanpxls)
        {
            //tüm resim ve kirpilmis resim gelir ve kutu rgb averagına göre toplam kutupxlrgb sayisi bulunur.
            int oneboxrgbpixelCount = 0;
            int originalImageRgbCount = 0;
            int squareCount = 0;
            int disaripixelCount = 0;
            int kutupixellerisayisi = 0;
            int kutusayisi = 0;
            int redpixelcount = 0;
            int greenpixelcount = 0;
            int bluepixelcount = 0;
            int redpixelcount0 = 0;
            int greenpixelcount0 = 0;
            int bluepixelcount0 = 0;
            int iceripixelsayisirgbli = 0;
            int kutularintoplamrgblisayisi = 0;
            int toplampixelcount = 0;

            int width = cropimage.Width;
            int height = cropimage.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    Color c1 = cropimage.GetPixel(x, y);
                    //otomatik olarak picture box'ın pixellerini saydıracak.
                    //cropimage rgbcount
                    if (c1.R > averagered - 15 && c1.R < averagered + 15)
                    {
                        redpixelcount++;
                    }
                    else
                    {
                        redpixelcount = redpixelcount;
                    }
                    if (c1.G > averagegreen - 15 && c1.G < averagegreen + 15)
                    {
                        greenpixelcount++;
                    }
                    else
                    {
                        greenpixelcount = greenpixelcount;
                    }
                    if (c1.B > averageblue - 15 && c1.B < averageblue + 15)
                    {
                        bluepixelcount++;
                    }
                    else
                    {
                        bluepixelcount = bluepixelcount;
                    }
                }
            }
            int width0 = originalimage.Width;
            int height0 = originalimage.Height;
            toplampixelcount = width0 * height0;

            for (int y = 0; y < height0; y++)
            {
                for (int x = 0; x < width0; x++)
                {
                    Color c10 = originalimage.GetPixel(x, y);//croplu

                    //original rgbcount
                    if (c10.R > averagered - 15 && c10.R < averagered + 15)
                    {
                        redpixelcount0++;
                    }
                    else
                    {
                        redpixelcount0 = redpixelcount0;
                    }
                    if (c10.G > averagegreen - 15 && c10.G < averagegreen + 15)
                    {
                        greenpixelcount0++;
                    }
                    else
                    {
                        greenpixelcount0 = greenpixelcount0;
                    }
                    if (c10.B > averageblue - 15 && c10.B < averageblue + 15)
                    {
                        bluepixelcount0++;
                    }
                    else
                    {
                        bluepixelcount0 = bluepixelcount0;
                    }
                }
            }
            kutularintoplamrgblisayisi = redpixelcount0 + greenpixelcount0 + bluepixelcount0;
            disaripixelCount = toplampixelcount - kutularintoplamrgblisayisi;
            oneboxrgbpixelCount = redpixelcount + greenpixelcount + bluepixelcount;

            //int squarecount = (pixelCount / pxlcount);

            kutusayisi = (kutularintoplamrgblisayisi / oneboxrgbpixelCount);

            return kutusayisi;

        }

        private Bitmap Kroplaveata(Image image, int startX, int startY, int width, int height)
        {
            Rectangle cropArea = new Rectangle(startX, startY, width, height);
            Bitmap croppedImage = new Bitmap(cropArea.Width, cropArea.Height);

            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                            cropArea, GraphicsUnit.Pixel);
            }

            return croppedImage;
        }

        public void button5_Click(object sender, EventArgs e)
        {
            Bitmap sizedImage = (Bitmap)pictureBox1.Image;
            Bitmap sizedImage02 = ResizeImage(sizedImage, 200, 167);
            pictureBox6.Image = (Image)sizedImage02;
            pictureBox6.Refresh();
            pictureBox6.MouseDown += new MouseEventHandler(pictureBox6_MouseDown);
            pictureBox6.MouseMove += new MouseEventHandler(pictureBox6_MouseMove);
            pictureBox6.MouseEnter += new EventHandler(pictureBox6_MouseEnter);
            pictureBox6.MouseWheel+= new MouseEventHandler(pictureBox6_MouseWheel); 
            Controls.Add(pictureBox6);
        }

        int crpX, crpY, rectW, rectH;

        public Pen crpPen = new Pen(Color.Cyan);
        private void pictureBox6_MouseDown(Object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Cursor = Cursors.Cross;
                    crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    crpX = e.X;
                    crpY = e.Y;
                }
            }
        }
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }
        private void pictureBox6_MouseWheel(object sender, MouseEventArgs e)
        {
            float zoom = 1.1f;
            if (e.Delta < 0)
            {
                zoom = 1 / zoom;
            }
            pictureBox6.Size = new Size((int)(pictureBox6.Size.Width * zoom), (int)(pictureBox6.Size.Height * zoom));
            pictureBox6.Refresh();
        }

        public int tekresimpixelbulucu(Bitmap kirpilmisresim, Bitmap orijinalresim, int redp, int greenb, int blueb, int secilialanpxls)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            int kutusayisi = CountWhiteSquares(kirpilmisresim, orijinalresim, redp, greenb, blueb, secilialanpxls);
            stopwatch1.Stop();
            stopwatch1.Restart();
            return kutusayisi;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

 

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void blueCntBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void GreenCntBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void redCntBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pixelCountBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void denemebox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)   // Crop Area
        {
            Cursor = Cursors.Default;
            Bitmap bmp2 = new Bitmap(pictureBox6.Width, pictureBox6.Height);
            Bitmap orijinalResim = (Bitmap)pictureBox6.Image;// Dosya içinden seçilen ilk resim
            pictureBox6.DrawToBitmap(bmp2, pictureBox6.ClientRectangle);//Dosya içinden seçilen ilk resim
            int width = pictureBox6.Width;
            int height = pictureBox6.Height;
            int originalPictotalpixelcount = width * height;// Dosya içinden seçilen ilk resmin toplam pixel sayisi
            Bitmap crpImg = new Bitmap(rectW, rectH);

            int secilialanpxlcount = 0;
            int red = 0, green = 0, blue = 0;
            progressBar1.Value = 30;
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < rectW; i++)
                {
                    for (int y = 0; y < rectH; y++)
                    {
                        progressBar1.Value = 100;

                        Color pxlclr = bmp2.GetPixel(crpX + i, crpY + y);
                        Color pixel = crpImg.GetPixel(i, y);
                        if (checkBox1.Checked == true)
                        {
                            if (pxlclr.R > 90)
                            {
                                int nextRowPixelR = -1;
                                if (y + 1 < rectH)
                                    nextRowPixelR = bmp2.GetPixel(crpX + i, crpY + y + 1).R;

                                int prevPixelR = -1;
                                if (i - 1 >= 0)
                                    prevPixelR = bmp2.GetPixel(crpX + i - 1, crpY + y).R;

                                int nextColumnPixelR = -1;
                                if (i + 1 < rectW)
                                    nextColumnPixelR = bmp2.GetPixel(crpX + i + 1, crpY + y).R;

                                int prevRowPixelR = -1;
                                if (y - 1 >= 0)
                                    prevRowPixelR = bmp2.GetPixel(crpX + i, crpY + y - 1).R;

                                red += pxlclr.R;
                                green += pxlclr.G;
                                blue += pxlclr.B;
                                secilialanpxlcount++;
                                /*int nextRowPixelR = -1;
                                if (y + 1 < rectH)
                                    nextRowPixelR = bmp2.GetPixel(crpX + i, crpY + y + 1).R;

                                int prevPixelR = -1;
                                if (i - 1 >= 0)
                                    prevPixelR = bmp2.GetPixel(crpX + i - 1, crpY + y).R;

                                int nextColumnPixelR = -1;
                                if (i + 1 < rectW)
                                    nextColumnPixelR = bmp2.GetPixel(crpX + i + 1, crpY + y).R;
                                */
                                if (Math.Abs(pxlclr.R - prevPixelR) < 30 && Math.Abs(pxlclr.R - nextRowPixelR) < 30 && Math.Abs(pxlclr.R - nextColumnPixelR) < 30)
                                {
                                    crpImg.SetPixel(i, y, pxlclr);

                                    red += pxlclr.R;
                                    green += pxlclr.G;
                                    blue += pxlclr.B;
                                    secilialanpxlcount++;
                                }
                                else
                                    break;

                            }
                            else if (checkBox1.Checked == false)
                            {

                                if (pxlclr.R > 100)
                                {
                                    crpImg.SetPixel(i, y, pxlclr);

                                    red += pxlclr.R;
                                    green += pxlclr.G;
                                    blue += pxlclr.B;
                                    secilialanpxlcount++;//seçi
                                }

                            }

                            /*int diff = 0;
                            if (y < rectH - 1 && i < rectW - 1)
                            {
                                Color nextPixel = bmp2.GetPixel(crpX + i, crpY + y + 1);
                                diff = Math.Abs(pxlclr.R - nextPixel.R);
                            }
                            if (diff >= 20)
                            {
                                break;
                            }
                            */
                        }
                    }
                }
                denemebox.Text = secilialanpxlcount.ToString();
                pixelCountBox.Text = Convert.ToString(secilialanpxlcount);
                // secilialanpixeli çarpı kutu sayisi eşittir içeri alan
                //rgb degerlerin averagı alınır
                int averageRed = red / secilialanpxlcount;
                redCntBox.Text = Convert.ToString(averageRed);
                int averageGreen = green / secilialanpxlcount;
                GreenCntBox.Text = Convert.ToString(averageGreen);
                int averageBlue = blue / secilialanpxlcount;

                blueCntBox.Text = Convert.ToString(averageBlue);
                pictureBox7.Image = (Image)crpImg;
                pictureBox7.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox7.Refresh();
                //belirtilen pikselde otomatik ss alaacak.

                //Image originalImage = Image.FromFile("your_image.jpg");
                //Bitmap croppedImage = CropImage(originalImage, 0, 0, 200, 300);
                //pictureBox1.Image = croppedImage;


                Bitmap kirpilmisresim = (Bitmap)pictureBox7.Image;
                int kutuSayisi = tekresimpixelbulucu(kirpilmisresim, orijinalResim, averageRed, averageGreen, averageBlue, secilialanpxlcount);//tekresim fonksiyonuna gönderir.
                string sqres = Convert.ToString(kutuSayisi);//kutu sayisini verir.
                textBox1.Text = sqres;
                if (kutuSayisi <= 6) 
                {
                    int src = kutuSayisi - Convert.ToInt32(textBox1.Text);
                    textBox2.Text = Convert.ToString(src);
                    
                }
               
                MessageBox.Show(sqres);
            }
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox6.Refresh();
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox6.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }
    }
}
           
