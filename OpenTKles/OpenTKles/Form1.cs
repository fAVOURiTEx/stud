using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace OpenTKles
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        double ScreenW, ScreenH;
        private float devX;
        private float devY;
        private float[,] GrapValuesArray;
        private int elements_count = 0;
        
        private bool not_calculate = true;
        private int pointPosition = 0;
        
        float lineX, lineY;
        float Mcoord_X = 0, Mcoord_Y = 0;

        private void functionCalculation()
        {
            
            float x = 0, y = 0;
            GrapValuesArray = new float[300, 2];
            elements_count = 0;
            for (x = -15; x < 15; x += 0.1f)
            {
                y = (float)Math.Sin(x) * 3 + 1;
                GrapValuesArray[elements_count, 0] = x;
                GrapValuesArray[elements_count, 1] = y;
                elements_count++;
            }
            not_calculate = false;
        }

        private void DrawDiagram()
        {
            if (not_calculate)
            {
                functionCalculation();
            }
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);
            for (int ax = 1; ax < elements_count; ax += 2)
            {
                GL.Vertex2(GrapValuesArray[ax, 0], GrapValuesArray[ax, 1]);
            }
            GL.End();
            GL.PointSize(5);
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            GL.End();
            GL.PointSize(1);
        }

        private void Draw()
        {
            GL.ClearColor(Color.Bisque);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            GL.Color3(0, 0, 0);
            GL.PushMatrix();
            GL.Translate(20, 15, 0);
            GL.Begin(PrimitiveType.Points);
            for (int ax = -15; ax < 15; ax++)
            {
                for (int bx = -15; bx < 15; bx++)
                {
                    GL.Vertex2(ax, bx);
                }
            }
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, -15);
            GL.Vertex2(0, 15);
            GL.Vertex2(-15, 0);
            GL.Vertex2(15, 0);
            GL.Vertex2(0, 15);
            GL.Vertex2(0.1, 14.5);
            GL.Vertex2(0, 15);
            GL.Vertex2(-0.1, 14.5);
            GL.Vertex2(15, 0);
            GL.Vertex2(14.5, 0.1);
            GL.Vertex2(15, 0);
            GL.Vertex2(14.5, -0.1);
            GL.End();
       
            DrawDiagram();
            GL.PopMatrix();
           
            GL.Color3(255, 0, 0);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(lineX, 15);
            GL.Vertex2(lineX, lineY);
            GL.Vertex2(20, lineY);
            GL.Vertex2(lineX, lineY);
            GL.End();
            
            AnT.SwapBuffers();
        }











        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
           
                
                Mcoord_X = e.X;
                Mcoord_Y = e.Y;
                
                lineX = devX * e.X;
                lineY = (float)(ScreenH - devY * e.Y);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if ((float)AnT.Width <= (float)AnT.Height)
            {
                ScreenW = 30.0;
                ScreenH = 30.0 * (float)AnT.Height / (float)AnT.Width;
                GL.Ortho(0.0, ScreenW, 0.0, ScreenH, -1, 1);
            }
            else
            {
                ScreenH = 30.0;
                ScreenW = 30.0 * (float)AnT.Width / (float)AnT.Height;
                GL.Ortho(0.0, 30.0 * (float)AnT.Width / (float)AnT.Height, 0.0, 30.0, -1, 1);
            }

            devX = (float)ScreenH / (float)AnT.Width;
            devY = (float)ScreenH / (float)AnT.Height;

            GL.MatrixMode(MatrixMode.Modelview);
            
            PointInGrap.Start();
            
        }

        private void PointInGrap_Tick(object sender, EventArgs e)
        {
            if (pointPosition == elements_count - 1)
                pointPosition = 0;

            Draw();

            pointPosition++;
        }
    }
}
