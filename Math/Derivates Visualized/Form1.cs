using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using MathNet.Numerics;
using System.Collections;

namespace Fractile_Summative
{
    public partial class fractalForm : Form
    {
        private Bitmap boxBitmap = new Bitmap(900, 900);
        private Bitmap box2Bitmap = new Bitmap(900, 900);
        private Bitmap box3Bitmap = new Bitmap(900, 900);
        public fractalForm()
        {
            //Sets Window's Size, Location, and Starting Text
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Size = new Size(1530, 1000);
            boxPictureBox.BorderStyle = BorderStyle.FixedSingle;
            boxPictureBox.BackColor = Color.AliceBlue;

            netPictureBox.BorderStyle = BorderStyle.FixedSingle;
            netPictureBox.BackColor = Color.AliceBlue;

            graphPictureBox.BorderStyle = BorderStyle.FixedSingle;
            graphPictureBox.BackColor = Color.AliceBlue;

            Graphics g = Graphics.FromImage(boxBitmap);

            float x = (trackBar1.Value / 1000.00f);

            informationLabel.Text = "Height (x): " + x.ToString()+"\nWidth (60-2x): " + (60 - (2 * x)).ToString()+"\nLength (30-2x): " + (30 - (2 * x)).ToString() + "\nVolume: "+(x * (60 - 2 * x) * (30 - 2 * x)).ToString();
            drawBox(g, new SolidBrush(Color.Blue), x, 60 - (2 * x), 30 - (2 * x));

            Graphics g2 = Graphics.FromImage(box2Bitmap);
            drawNet(g2, new SolidBrush(Color.Blue), x, 60 - (2 * x), 30 - (2 * x));

            Graphics g3 = Graphics.FromImage(box3Bitmap);
            drawGraph(g3, new SolidBrush(Color.Blue));
            double y = function(x);
            g3.FillRectangle(new SolidBrush(Color.Red), (float)(x * graphPictureBox.Width / 17 - 1), (float)(-y * graphPictureBox.Height / 6001 + 374), 5, 5);
            

            graphPictureBox.Refresh();

            label1.Visible = true;
            label2.Visible = true;
            label2.Visible = true;
            label2.Visible = true;
            label2.Visible = true;

            textBox1.Text = ((double)trackBar1.Value/1000).ToString();

            label3.Font = new Font("Ariel", 24, FontStyle.Bold);


            label4.Text = "This Visual is meant to represent the following question: \n" + "A piece of sheet metal 60 cm by 30 cm is to be used to make a rectangular box with an open top. \nFind the dimensions that will give the box with the largest volume. \n\n" + "The x-axis on the graph represent the length of one of the squares that has been cut. \nThe y-axis represents the volume of the prism. \nThe net represents the cut piece of metal.\n" + "The x-value can range from 0 to 15.\n" + "An x-value of 6.34 corresponds to the largest volume (volume of 5196).";

            label5.Text = "X (Height)";
            label6.Text = "Volume";

            label5.Font = new Font(label5.Font.Name, label5.Font.SizeInPoints, FontStyle.Underline);
            label6.Font = new Font(label6.Font.Name, label6.Font.SizeInPoints, FontStyle.Underline);

            //DrawGraph
        }

        private void fractalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            //Refreshes Picturebox
            boxPictureBox.BackColor = Color.AliceBlue;
            e.Graphics.DrawImage(boxBitmap, 0, 0);
        }

        private void netPictureBox_Paint(object sender, PaintEventArgs e)
        {
            boxPictureBox.BackColor = Color.AliceBlue;
            e.Graphics.DrawImage(box2Bitmap, 0, 0);
        }

        private void graphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            graphPictureBox.BackColor = Color.AliceBlue;
            e.Graphics.DrawImage(box3Bitmap, 0, 0);
        }

        private void drawBox(Graphics drawingSurface, Brush brush, float height, float width, float length)
        {
            int scale = 7;
            int x = 25;
            int y = 200;


            height *= scale;
            width *= scale;
            length *= scale;
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;

            PointF A = new PointF(x, y);
            PointF B = new PointF(x + width, y);
            PointF C = new PointF(x + width, y + height);
            PointF D = new PointF(x, y + height);
            PointF[] Face1 = { A, B, C, D };

            
            drawingSurface.FillPolygon(brush,Face1);
            drawingSurface.DrawLine(pen, A, B);
            drawingSurface.DrawLine(pen, C, B);
            drawingSurface.DrawLine(pen, D, C);
            drawingSurface.DrawLine(pen, A, D);

            float Ex = x + (float)(Math.Cos(Math.PI / 3) * length);
            float Ey = y - (float)(Math.Sin(Math.PI / 3) * length);
            PointF E = new PointF(Ex, Ey);

            float Fx = Ex + width;
            float Fy = y - (float)(Math.Sin(Math.PI / 3) * length);
            PointF F = new PointF(Fx, Fy);

            float Gx = Fx;
            float Gy = y + height - (float)(Math.Sin(Math.PI / 3) * length);
            PointF G = new PointF(Gx, Gy);

            PointF[] Face2 = { B, C, G, F };
            drawingSurface.FillPolygon(brush, Face2);


            drawingSurface.DrawLine(pen, B, F);
            drawingSurface.DrawLine(pen, F, G);
            drawingSurface.DrawLine(pen, C, G);
            drawingSurface.DrawLine(pen, F, E);
            drawingSurface.DrawLine(pen, E, A);


        }
        

        private void drawNet(Graphics drawingSurface, Brush brush, float height, float width, float length)
        {

            int scale = 7;
            int x = 25;
            int y = 100;

            height *= scale;
            width *= scale;
            length *= scale;
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;

            PointF A = new PointF(x, y);
            PointF B = new PointF(x + width + height + height, y);
            PointF C = new PointF(x, y + height+height+length);
            PointF D = new PointF(x + width + height + height, y + height+height+length);
            PointF[] Face1 = { B, A, C, D };


            drawingSurface.FillPolygon(brush, Face1);

            PointF E = new PointF(x + width + height,y);
            PointF F = new PointF(x, y+height+length);
            PointF G = new PointF(x + width + height, y+height+length);

            PointF A2 = new PointF(x, y + height);
            PointF A3 = new PointF(x+height, y + height);
            PointF A4 = new PointF(x+height, y);
            PointF B2 = new PointF(x+width+height, y + height);
            PointF B3 = new PointF(x+height+height+width, y + height);
            PointF C2 = new PointF(x+height+height+width, y + height+length);
            PointF C3 = new PointF(x+height+width, y + height+height+length);
            PointF D2 = new PointF(x + height, y + height + length);
            PointF D3 = new PointF(x + height, y + height + height + length);

            PointF TL = new Point(x, y);
            PointF TR = new Point(x+60*scale, y);
            PointF BL = new Point(x, y + 30 * scale);
            PointF BR = new Point(x + 60 * scale, y + 30 * scale);

            drawingSurface.DrawLine(pen, TL, TR);
            drawingSurface.DrawLine(pen, TL, BL);
            drawingSurface.DrawLine(pen, TR, BR);
            drawingSurface.DrawLine(pen, BR, BL);



            drawingSurface.FillRectangle(new SolidBrush(Color.AliceBlue), new RectangleF(A, new SizeF(height, height)));
            drawingSurface.FillRectangle(new SolidBrush(Color.AliceBlue), new RectangleF(E, new SizeF(height, height)));
            drawingSurface.FillRectangle(new SolidBrush(Color.AliceBlue), new RectangleF(F, new SizeF(height, height)));
            drawingSurface.FillRectangle(new SolidBrush(Color.AliceBlue), new RectangleF(F, new SizeF(height, height)));
            drawingSurface.FillRectangle(new SolidBrush(Color.AliceBlue), new RectangleF(G, new SizeF(height, height)));

            if (length != 0)
            {

                drawingSurface.DrawLine(pen, A2, A3);
                drawingSurface.DrawLine(pen, A2, F);
                drawingSurface.DrawLine(pen, F, D2);
                drawingSurface.DrawLine(pen, B2, B3);
                drawingSurface.DrawLine(pen, C2, B3);
                drawingSurface.DrawLine(pen, G, C2);

            }

            drawingSurface.DrawLine(pen, A3, A4);
            drawingSurface.DrawLine(pen, E, B2);
            drawingSurface.DrawLine(pen, D2, D3);
            drawingSurface.DrawLine(pen, G, C3);

            drawingSurface.DrawLine(pen, A4, E);
            drawingSurface.DrawLine(pen, D3, C3);

        }

        private void drawGraph (Graphics drawingSurface, Brush brush)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            float w = graphPictureBox.Width;
            float h = graphPictureBox.Height;

            double scaleX = w / 17;
            double scaleY = h / 6001;

            for (double i=0; i <= 16; i += 0.01)
            {
                double y = function(i);
                drawingSurface.FillRectangle(brush, (float)(i*scaleX), (float)(-y*scaleY+375), 1, 1);
            }
        }
        private void drawGraph2(Graphics drawingSurface, Brush brush)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            float w = graphPictureBox.Width;
            float h = graphPictureBox.Height;

            double scaleX = w / 13;
            double scaleY = h / 23;

            for (double i = 0; i <= 12; i += 0.01)
            {
                double y = function2(i);
                drawingSurface.FillRectangle(brush, (float)(i * scaleX), (float)(-y * scaleY + 375), 1, 1);
            }
        }
        private void drawGraph3(Graphics drawingSurface, Brush brush)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            float w = graphPictureBox.Width;
            float h = graphPictureBox.Height;

            double scaleX = w / 11;
            double scaleY = h / 39;

            for (double i = 0; i <= 10; i += 0.001)
            {
                double y = function3(i);
                drawingSurface.FillRectangle(brush, (float)(i * scaleX), (float)(-y * scaleY + 375), 1, 1);
            }
        }
        private void drawGraph4(Graphics drawingSurface, Brush brush)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            float w = graphPictureBox.Width;
            float h = graphPictureBox.Height;

            double scaleX = w / 7;
            double scaleY = h / 11;

            for (double i = 0; i <= 6; i += 0.001)
            {
                double y = function4(i);
                drawingSurface.FillRectangle(brush, (float)(i * scaleX), (float)(-y * scaleY + 375), 1, 1);
            }
        }


        private double function(double x)
        {
            return Math.Round((4 * x * x * x) - (180 * x * x) + (1800 * x),3);
        }
        private double function2(double x)
        {
            return Math.Round(((x * (12 - x) * (12 - x)) / (4 * (Math.PI))),3);
        }
        private double function3(double x)
        {
            return Math.Round(((x-5) + 5) * Math.Sqrt((5 * 5) - ((x-5) * (x-5))),3);
        }
        private double function4(double x)
        {
            return Math.Round((6*x-(x*x)),3);
        }
        private void drawCylinder (Graphics drawingSurface, Brush brush, float height, float width)
        {
            int scale = 20;
            height *= scale;
            width *= scale*2;
            float diameter = (float)(width / Math.PI);
            int x = (int)((boxPictureBox.Width / 2)  - (diameter/2));
            int y = 20;
            Pen pen = new Pen((Color.Black),6);
            Pen pen2 = new Pen((Color.Black), 2);

            
            PointF A = new PointF(x, y+diameter/4);
            PointF B = new PointF(x, y+height+diameter/4);
            PointF C = new PointF(x+diameter, y+diameter/4);
            PointF D = new PointF(x+diameter, y+height+diameter/4);
            SolidBrush brush2 = new SolidBrush(Color.Teal);
            PointF[] array = new PointF[4] {D,C,A,B};
            drawingSurface.FillPolygon(brush2, array);

            if (height != 12 * scale)

            {

                RectangleF rectBot = new RectangleF(x, y + height, diameter, diameter/2);
                drawingSurface.DrawEllipse(pen, rectBot);
                drawingSurface.FillEllipse(brush, rectBot);

                RectangleF rectTop = new RectangleF(x, y, diameter, diameter/2);
                drawingSurface.DrawEllipse(pen, rectTop);
                drawingSurface.FillEllipse(brush, rectTop);

            }

            drawingSurface.DrawLine(pen2, A, B);
            drawingSurface.DrawLine(pen2, C, D);



        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            textBox1.Text = (Convert.ToDouble(trackBar1.Value)/1000).ToString();
            if (radioButton1.Checked == true)
            {
                Graphics g = Graphics.FromImage(boxBitmap);
                g.Clear(boxPictureBox.BackColor);

                float x = ((trackBar1.Value) / 1000.00f);
                drawBox(g, new SolidBrush(Color.Blue), x, 60 - (2 * x), 30 - (2 * x));
                informationLabel.Text = "Height (x): " + x.ToString() + "\nWidth (60-2x): " + (60 - (2 * x)).ToString() + "\nLength (30-2x): " + (30 - (2 * x)).ToString() + "\nVolume (4x^3 - 180x^2 + 1800x): " + function(x).ToString();
                boxPictureBox.Refresh();


                Graphics g2 = Graphics.FromImage(box2Bitmap);
                g2.Clear(netPictureBox.BackColor);
                drawNet(g2, new SolidBrush(Color.Blue), x, 60 - (2 * x), 30 - (2 * x));
                netPictureBox.Refresh();

                Graphics g3 = Graphics.FromImage(box3Bitmap);
                g3.Clear(graphPictureBox.BackColor);

                double y = function(x);
                g3.FillRectangle(new SolidBrush(Color.Red), (float)(x * graphPictureBox.Width / 17 - 1), (float)(-y * graphPictureBox.Height / 6001 + 374), 5, 5);

                drawGraph(g3, new SolidBrush(Color.Blue));

                PointF TopPoint = new PointF((float)((x * graphPictureBox.Width / 17 - 1) + 2.5), 0);
                PointF LeftPoint = new PointF(0, (float)((-y * graphPictureBox.Height / 6001 + 374) + 2.5));
                PointF GraphPoint = new PointF((float)((x * graphPictureBox.Width / 17 - 1) + 2.5), (float)((-y * graphPictureBox.Height / 6001 + 374) + 2.5));

                Pen penLine = new Pen(Color.Black, 1);
                float[] dashValues = { 3, 3, 3, 3 };
                penLine.DashPattern = dashValues;
                g3.DrawLine(penLine, TopPoint, GraphPoint);
                g3.DrawLine(penLine, LeftPoint, GraphPoint);

                
                label1.Text = x.ToString();
                label2.Text = function(x).ToString();
                label1.Location = new Point((int)TopPoint.X+graphPictureBox.Left - label1.Width/2,graphPictureBox.Top-15);
                label2.Location = new Point(graphPictureBox.Left - 55,(int)LeftPoint.Y+graphPictureBox.Top-label2.Height/2);

                double x2 = Math.Round(x, 2);
                label7.Text = (x2-0.2).ToString();
                label8.Text = (x2 - 0.1).ToString();
                label9.Text = (x2).ToString();
                label10.Text = (x2 + 0.1).ToString();
                label11.Text = (x2 + 0.2).ToString();
                label12.Text = function(x2 - 0.2).ToString();
                label13.Text = function(x2 - 0.1).ToString();
                label14.Text = function(x2).ToString();
                label15.Text = function(x2 + 0.1).ToString();
                label16.Text = function(x2 + 0.2).ToString();


                graphPictureBox.Refresh();
            }
            else if (radioButton2.Checked == true)
            {
                Graphics g = Graphics.FromImage(boxBitmap);
                g.Clear(boxPictureBox.BackColor);

                float x = (trackBar1.Value / 1000.00f);
                drawCylinder(g, new SolidBrush(Color.Blue), x, 12 - x);
                informationLabel.Text = "Length (x): " + x.ToString() + "\nWidth (12-x): " + (12 - (x)).ToString() + "\nVolume (x*(12-x)^2 / 4*pi): " + (function2(x).ToString());
                boxPictureBox.Refresh();

                Graphics g2 = Graphics.FromImage(box2Bitmap);
                g2.Clear(netPictureBox.BackColor);
                int Scale = 20;
                int temp = 200;
                RectangleF rect = new RectangleF(temp, 100, (12 - x) * Scale, x * Scale);
                g2.FillRectangle(new SolidBrush(Color.Teal), rect);
                PointF A = new PointF(temp, 100);
                PointF B = new PointF(temp + (12 - x) * Scale, 100);
                PointF C = new PointF(temp + (12 - x) * Scale, 100 + x * Scale);
                PointF D = new PointF(temp, 100 + x * Scale);
                g2.DrawLine(new Pen(Color.Black, 4), A, B);
                g2.DrawLine(new Pen(Color.Black, 4), C, B);
                g2.DrawLine(new Pen(Color.Black, 4), C, D);
                g2.DrawLine(new Pen(Color.Black, 4), A, D);
                netPictureBox.Refresh();


                Graphics g3 = Graphics.FromImage(box3Bitmap);
                g3.Clear(graphPictureBox.BackColor);

                drawGraph2(g3, new SolidBrush(Color.Blue));


                double y = function2(x);
                g3.FillRectangle(new SolidBrush(Color.Red), (float)(x * graphPictureBox.Width / 13 - 1), (float)(-y * graphPictureBox.Height / 23 + 374), 5, 5);

              

                PointF TopPoint = new PointF((float)((x * graphPictureBox.Width / 13 - 1) +2.5), 0);
                PointF LeftPoint = new PointF(0, (float)((-y * graphPictureBox.Height / 23 + 374)+2.5));
                PointF GraphPoint = new PointF((float)((x * graphPictureBox.Width / 13 - 1)+2.5), (float)((-y * graphPictureBox.Height / 23 + 374)+2.5));

                Pen penLine = new Pen(Color.Black, 1);
                float[] dashValues = { 3, 3, 3, 3 };
                penLine.DashPattern = dashValues;

                g3.DrawLine(penLine, TopPoint, GraphPoint);
                g3.DrawLine(penLine, LeftPoint, GraphPoint);
                label1.Text = x.ToString();
                label2.Text = function2(x).ToString();
                label1.Location = new Point((int)TopPoint.X + graphPictureBox.Left - label1.Width / 2, graphPictureBox.Top - 15);
                label2.Location = new Point(graphPictureBox.Left - 55, (int)LeftPoint.Y + graphPictureBox.Top - label2.Height / 2);


                double x2 = Math.Round(x, 2);
                label7.Text = (x2 - 0.2).ToString();
                label8.Text = (x2 - 0.1).ToString();
                label9.Text = (x2).ToString();
                label10.Text = (x2 + 0.1).ToString();
                label11.Text = (x2 + 0.2).ToString();
                label12.Text = function2(x2 - 0.2).ToString();
                label13.Text = function2(x2 - 0.1).ToString();
                label14.Text = function2(x2).ToString();
                label15.Text = function2(x2 + 0.1).ToString();
                label16.Text = function2(x2 + 0.2).ToString();


                graphPictureBox.Refresh();
            }
            else if (radioButton3.Checked)
            {
                Graphics g = Graphics.FromImage(boxBitmap);
                g.Clear(boxPictureBox.BackColor);

                float x = ((trackBar1.Value-5000) / 1000.00f);
                RectangleF circleRect = new RectangleF(45, 25, 320, 320);
                g.DrawEllipse(new Pen(Color.Black), circleRect);
                informationLabel.Text = "Height (x): " + (x+5).ToString() + "\nArea (x * sqrt(5^2 - (x-5)^2)): " + function3(x+5).ToString();
                
                PointF[] arr = new PointF[3] { new PointF(205, 25), new PointF((float)(205 - Math.Sqrt((160 * 160) - (32 * x) * (32 * x))), 25 + 160 + 32 * x), new PointF((float)(205 + Math.Sqrt((160 * 160) - (32 * x) * (32 * x))), 25 + 160 + 32 * x)};
                g.FillPolygon(new SolidBrush(Color.Blue), arr);
                
                boxPictureBox.Refresh();

                Graphics g2 = Graphics.FromImage(box2Bitmap);
                g2.Clear(netPictureBox.BackColor);
                netPictureBox.Refresh();

                Graphics g3 = Graphics.FromImage(box3Bitmap);
                g3.Clear(graphPictureBox.BackColor);
                drawGraph3(g3, new SolidBrush(Color.Blue));
                double y = function3(x+5);
                g3.FillRectangle(new SolidBrush(Color.Red), (float)((x+5) * graphPictureBox.Width / 11 - 1), (float)(-y * graphPictureBox.Height / 39 + 374), 5, 5);
                
                PointF TopPoint = new PointF((float)(((x+5) * graphPictureBox.Width / 11 - 1) + 2.5), 0);
                PointF LeftPoint = new PointF(0, (float)((-y * graphPictureBox.Height / 39 + 374) + 2.5));
                PointF GraphPoint = new PointF((float)(((x+5) * graphPictureBox.Width / 11 - 1) + 2.5), (float)((-y * graphPictureBox.Height / 39 + 374) + 2.5));
                label1.Text = (x+5).ToString();
                label2.Text = function3(x+5).ToString();
                label1.Location = new Point((int)TopPoint.X + graphPictureBox.Left - label1.Width / 2, graphPictureBox.Top - 15);
                label2.Location = new Point(graphPictureBox.Left - 55, (int)LeftPoint.Y + graphPictureBox.Top - label2.Height / 2);

                Pen penLine = new Pen(Color.Black, 1);
                float[] dashValues = { 3, 3, 3, 3 };
                penLine.DashPattern = dashValues;

                double x2 = Math.Round(x+5, 2);
                label7.Text = (x2 - 0.2).ToString();
                label8.Text = (x2 - 0.1).ToString();
                label9.Text = (x2).ToString();
                label10.Text = (x2 + 0.1).ToString();
                label11.Text = (x2 + 0.2).ToString();
                label12.Text = function3(x2 - 0.2).ToString();
                label13.Text = function3(x2 - 0.1).ToString();
                label14.Text = function3(x2).ToString();
                label15.Text = function3(x2 + 0.1).ToString();
                label16.Text = function3(x2 + 0.2).ToString();

                g3.DrawLine(penLine, TopPoint, GraphPoint);
                g3.DrawLine(penLine, LeftPoint, GraphPoint);
                graphPictureBox.Refresh();
            }
            else if (radioButton4.Checked)
            {
                Graphics g = Graphics.FromImage(boxBitmap);
                g.Clear(boxPictureBox.BackColor);

                float x = ((trackBar1.Value) / 1000.00f);

                informationLabel.Text = "Length (x): " + (x).ToString() + "\nWidth (6-x): "+(6-x).ToString() + "\nArea (6x-x^2): " + function4(x).ToString();
                int scale = 35;
                PointF[] arr = new PointF[4] { new PointF(60, 70 + scale * x), new PointF(60, 70), new PointF(60 + scale * (6 - x), 70), new PointF(60 + scale * (6 - x), 70 + scale * x) };
                g.FillPolygon(new SolidBrush(Color.Blue), arr);


                Pen pen = new Pen(Color.Black);
                pen.Width = 4;
                g.DrawLine(pen, arr[0], arr[1]);
                g.DrawLine(pen, arr[2], arr[1]);
                g.DrawLine(pen, arr[2], arr[3]);
                g.DrawLine(pen, arr[3], arr[0]);

                boxPictureBox.Refresh();

                Graphics g2 = Graphics.FromImage(box2Bitmap);
                g2.Clear(netPictureBox.BackColor);
                netPictureBox.Refresh();

                Graphics g3 = Graphics.FromImage(box3Bitmap);
                g3.Clear(graphPictureBox.BackColor);
                drawGraph4(g3, new SolidBrush(Color.Blue));
                double y = function4(x);
                g3.FillRectangle(new SolidBrush(Color.Red), (float)((x) * graphPictureBox.Width / 7 - 1), (float)(-y * graphPictureBox.Height / 11 + 374), 5, 5);


                PointF TopPoint = new PointF((float)(((x) * graphPictureBox.Width / 7 - 1) + 2.5), 0);
                PointF LeftPoint = new PointF(0, (float)((-y * graphPictureBox.Height / 11 + 374) + 2.5));
                PointF GraphPoint = new PointF((float)(((x) * graphPictureBox.Width / 7 - 1) + 2.5), (float)((-y * graphPictureBox.Height / 11 + 374) + 2.5));
                label1.Text = (x).ToString();
                label2.Text = function4(x).ToString();
                label1.Location = new Point((int)TopPoint.X + graphPictureBox.Left - label1.Width / 2, graphPictureBox.Top - 15);
                label2.Location = new Point(graphPictureBox.Left - 55, (int)LeftPoint.Y + graphPictureBox.Top - label2.Height / 2);

                Pen penLine = new Pen(Color.Black, 1);
                float[] dashValues = { 3, 3, 3, 3 };
                penLine.DashPattern = dashValues;

                double x2 = Math.Round(x, 2);
                label7.Text = (x2 - 0.2).ToString();
                label8.Text = (x2 - 0.1).ToString();
                label9.Text = (x2).ToString();
                label10.Text = (x2 + 0.1).ToString();
                label11.Text = (x2 + 0.2).ToString();
                label12.Text = function4(x2 - 0.2).ToString();
                label13.Text = function4(x2 - 0.1).ToString();
                label14.Text = function4(x2).ToString();
                label15.Text = function4(x2 + 0.1).ToString();
                label16.Text = function4(x2 + 0.2).ToString();

                g3.DrawLine(penLine, TopPoint, GraphPoint);
                g3.DrawLine(penLine, LeftPoint, GraphPoint);

                graphPictureBox.Refresh();

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {

                trackBar1.Maximum = 15000;
                trackBar1.Value = 7500;
                trackBar1_Scroll(sender, e);

                label4.Text = "This Visual is meant to represent the following question: \n" + "A piece of sheet metal 60 cm by 30 cm is to be used to make a rectangular box with an open top. \nFind the dimensions that will give the box with the largest volume. \n\n" + "The x-axis on the graph represent the length of one of the squares that has been cut. \nThe y-axis represents the volume of the prism. \nThe net represents the cut piece of metal.\n"+"The x-value can range from 0 to 15.\n" + "An x-value of 6.34 corresponds to the largest volume (volume of 5196).";

                label5.Text = "X (Height)";
                label6.Text = "Volume";
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                trackBar1.Maximum = 12000;
                trackBar1.Value = 6000;
                trackBar1_Scroll(sender, e);


                label4.Text = "This Visual is meant to represent the following question: \n" + "Consider a rectangle of perimeter 24 inches. Form a cylinder by revolving this rectangle about one of its edges.\nWhat dimensions of the rectangle will result in a cylinder of maximum volume?\n\n" + "The x-axis on the graph represent the length of the rectangle. \nThe y-axis represents the volume of the cylinder. \nThe net represents the rectangle, whose perimeter is 12 inches.\n" + "The x-value can range from 0 to 12.\n"+"An x-value of 4 corresponds to the largest volume (volume of 20.372).";


                label5.Text = "X (Length)";
                label6.Text = "Volume";
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                trackBar1.Minimum = 0;
                trackBar1.Maximum = 10000;
                trackBar1.Value = 5000;
                trackBar1_Scroll(sender, e);

                label4.Text = "This Visual is meant to represent the following question: \n" + "Consider an inscribed isoceles triangle inside a circle of radius 5 (diameter of 10).\nWhat is the largest area that is possible to create inside the triangle?\n\n" + "The x-axis on the graph represent the height of the triangle. \nThe y-axis represents the area of the triangle.\n" + "The x-value can range from 0 to 10.\n"+ "An x-value of 7.5 corresponds to the largest area (area of 32.476).";

                label5.Text = "X (Height)";
                label6.Text = "Area";

            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                trackBar1.Minimum = 0;
                trackBar1.Maximum = 6000;
                trackBar1.Value = 3000;
                trackBar1_Scroll(sender, e);

                label4.Text = "This Visual is meant to represent the following question: \n" + "Consider a rectangle with the perimeter of 12 units. \nWhat is the largest area that is possible to create inside the rectangle?\n\n" + "The x-axis on the graph represent the length of the rectangle. \nThe y-axis represents the area of the rectangle.\n" + "The x-value can range from 0 to 6.\n" + "An x-value of 3 corresponds to the largest area (area of 9).";

                label5.Text = "X (Length)";
                label6.Text = "Area";

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string stri= "";
            string accept = ".0123456789";
            for (int i=0; i<textBox1.TextLength; i++)
            {
                string g = textBox1.Text[i].ToString();
                if (accept.IndexOf(g) != -1)
                {
                    stri = stri + g;
                }
            }
            if ((stri.Split('.').Length-1)>1)
            {
                stri = stri.Substring(0, stri.Length - 1);
            }
            if (stri.Length >= 1)
            {
                
                if (stri[0].ToString() == ".")
                {
                    stri = stri.Remove(0, 1);
                }
            }
            
            if (stri.Length >= 1)
            {
                bool dot = false;
                if (stri[stri.Length - 1].ToString() == ".") {
                    dot = true;
                }

                stri = (Math.Floor(Convert.ToDouble(stri)*1000)/1000).ToString();
                if (dot)
                {
                    stri = stri + ".";
                }
                if (Convert.ToDouble(stri) > trackBar1.Maximum / 1000)
                {
                    stri = ((trackBar1.Maximum) / 1000).ToString();
                    MessageBox.Show("Number cannot exceed " + stri);
                }

                textBox1.SelectionStart = textBox1.TextLength;

                if (stri[stri.Length - 1].ToString() != ".")
                {
                    trackBar1.Value = (int)(Convert.ToDouble(stri) * 1000);
                    trackBar1_Scroll(sender, e);
                }

            }
            textBox1.Text = stri;

        }


    }
}
