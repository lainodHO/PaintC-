using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;

namespace Menu
{
    public partial class Form1 : Form
    {
        //Definir arreglos 
        public Point[] puntos;
        //Definir variables
        string nombre;
        public int edad;
        //Objetos tipo puntos
        public Point p1 = new Point(2, 2);
        public Point p2 = new Point(100, 100);
        //public Pen lapiz = new Pen(Color.Red);
        public Graphics lona;
        public SolidBrush brocha = new SolidBrush(Color.Cyan);
        public Pen lapiz = new Pen(Color.Blue);
        public LinearGradientBrush gradiente;
        public int diametro = 5; // son las variables locales de diametro y altura 
        public byte estilo_brocha;//0 si la broacha es llena y 1 si es vacia
        //Variables para el boton de lineas ,2 = Lineas
        public bool primer_punto = true;
        public Point punto1 = new Point();
        public Point punto2 = new Point();
        public StreamReader lector ;
      

        public Form1()
        {
            InitializeComponent();

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void Leer_puntos()
        {
            listBox1.Items.Clear();
            while (!lector.EndOfStream)
            {
                listBox1.Items.Add(lector.ReadLine().ToString());
            }
        }

        private void Dibujar_figura()
        {
            puntos = new Point[listBox1.Items.Count];
            string cadena;
            string subX;
            string subY;
            //ciclo de llenado del arreglo para dibujar
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                cadena = listBox1.Items[i].ToString();
                //MessageBox.Show(cadena);
                int pos_coma = cadena.IndexOf(',');
                //MessageBox.Show(pos_coma.ToString());
                subX = cadena.Substring(0, pos_coma);
                //MessageBox.Show(subX);
                subY = cadena.Substring(pos_coma + 1);
                //MessageBox.Show(subY);
                puntos[i].X = int.Parse(subX);
                puntos[i].Y = int.Parse(subY);
            }//For

            //Dibujar la figura
            lona.DrawLines(lapiz, puntos);
            lona.FillPolygon(brocha, puntos);
        }//Funcion

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Carpetita amarilla 
            listBox1.Items.Clear();
            listBox1.Visible = true;
            //MessageBox.Show("Quieres ver el archivo?");
            //openFileDialog1.ShowDialog();
             
            //El constructor es una funcion y tiene parentesis es para leer archivos 
            StreamReader lector = new StreamReader(openFileDialog1.FileName);

            while (!lector.EndOfStream)//! una negacion negada es una afiramcion 
            {
                listBox1.Items.Add(lector.ReadLine());  //ReadLine lee y salto de linea
            }
            dibuja_figura();

           
           
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            //Crear un objeto que escribe en el archivo de texto
            StreamWriter escritor = new StreamWriter(saveFileDialog1.FileName);
            //for para escribir en el archivo
            for (int i = 0; i<listBox1.Items.Count; i++) 
            {
                escritor.WriteLine(listBox1.Items[i].ToString());   
            }
            escritor.Close();

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           
            //lona.DrawEllipse(lapiz, e.X, e.Y,50, 50);
            if (estilo_brocha == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //lona.FillEllipse(gradiente, e.X, e.Y, 20, 20);
                    lona.FillEllipse(brocha, e.X, e.Y, diametro, diametro);
                }

            }

            if (estilo_brocha == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //lona.FillEllipse(gradiente, e.X, e.Y, 20, 20);
                    lona.DrawEllipse(lapiz, e.X, e.Y, diametro, diametro);
                }

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lapiz.Width = 5;//ancho
            lona = CreateGraphics();
            gradiente = new LinearGradientBrush(p1, p2, Color.Cyan, Color.Purple);
            listBox1.Visible = false;

            //llamada a metodos al iniciar la apps
            llenar_combobox_diametro();
            llenar_combobox_ancho_pluma();

        }

        private void llenar_combobox_ancho_pluma()
        {
            for (int i = 0; i < 20; i += 4)
            {//Llenamos la combo del toolbox
                toolStripComboBox2.Items.Add(i.ToString());
            }
        }

        private void llenar_combobox_diametro()
        {
            for (int i = 0; i < 30; i += 4)
            {//Llenamos la combo del toolbox
                toolStripComboBox1.Items.Add(i.ToString());
            }
        }

        private void limpiarPantallaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lona.Clear(Color.White);
            primer_punto = true;
            
        }

        private void programasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void limpiarPantallaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lona.Clear(Color.White);
            primer_punto = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cambiarColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Llamar a la paleta de colores par acambiar el color de la brocha
            colorDialog1.ShowDialog();
            brocha.Color = colorDialog1.Color;
            lapiz.Color = colorDialog1.Color;
        }

        private void cambiarColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            brocha.Color = colorDialog1.Color;
            lapiz.Color = colorDialog1.Color;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Carpetita amarilla 
            listBox1.Items.Clear();
            listBox1.Visible = true;
            //MessageBox.Show("Quieres ver el archivo?");
            openFileDialog1.ShowDialog();

            //El constructor es una funcion y tiene parentesis es para leer archivos 
            StreamReader lector = new StreamReader(openFileDialog1.FileName);

            while (!lector.EndOfStream)//! una negacion negada es una afiramcion 
            {
                listBox1.Items.Add(lector.ReadLine());  //ReadLine lee y salto de linea
            }
            dibuja_figura();





        }

        private void dibuja_figura()
        {
            puntos = new Point[listBox1.Items.Count];
            string cadena;
            string subX;
            string subY;
            //ciclo de llenado del arreglo para dibujar
            for (int i = 0; i < listBox1.Items.Count; i++)
            { 
            cadena = listBox1.Items[i].ToString();
            //MessageBox.Show(cadena);
            int pos_coma = cadena.IndexOf(',');
            //MessageBox.Show(pos_coma.ToString());
            subX=cadena.Substring(0, pos_coma);
            //MessageBox.Show(subX);
            subY = cadena.Substring(pos_coma + 1 );
            //MessageBox.Show(subY);
            puntos[i].X=int.Parse(subX);
            puntos[i].Y = int.Parse(subY);
            }//For

            //Dibujar la figura
            lona.DrawLines(lapiz,puntos);
           lona.FillPolygon(brocha,puntos);
        }//Funcion

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            brocha.Color = colorDialog1.Color;
            lapiz.Color = colorDialog1.Color;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            //Crear un objeto que escribe en el archivo de texto
            StreamWriter escritor = new StreamWriter(saveFileDialog1.FileName);
            //for para escribir en el archivo
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                escritor.WriteLine(listBox1.Items[i].ToString());
            }
            escritor.Close();

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            //cambiara el valor del diametro de la brocha 
            //diametro = int.Parse((toolStripComboBox1.SelectedItem.ToString()));

          
            //if (toolStripComboBox1.SelectedItem != null)
            //{
            //    //Convertir el valor seleccionado a un entero
            //    if (int.TryParse(toolStripComboBox1.SelectedItem.ToString(), out diametro))
            //    {
            //    }
            //}
           


        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            primer_punto = true;
            brocha.Color = Form1.DefaultBackColor;
            lapiz.Color = Form1.DefaultBackColor;
            brocha.Color = Color.White;
            lapiz.Color = Color.White;
            listBox1.Visible = false;
            listBox1.Items.Clear();

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            estilo_brocha = 0;
            listBox1.Visible = false;
            listBox1.Items.Clear();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            estilo_brocha = 1;
            listBox1.Visible = false;
            listBox1.Items.Clear();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //Boton que activa Dibujo de lineas y el List Box
            primer_punto = true;
            estilo_brocha = 2;
            listBox1.Items.Clear();
            listBox1.Visible = true;


        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Si la bandera vale 2 entrara a este codigo 
            if (estilo_brocha==2)
            {
                //Se dibujaran las lineas 
                if (primer_punto == true)
                {
                    //Guardar el punto 
                    punto1.X = e.X;
                    punto1.Y = e.Y;
                    primer_punto = false;
                    listBox1.Items.Add( e.X.ToString() + "," + e.Y.ToString());
                }
                else 
                {
                    //Va entrar a partir del segundo click en adelante
                    punto2.X = e.X;
                    punto2.Y = e.Y;
                    listBox1.Items.Add(e.X.ToString() + "," +e.Y.ToString());
                    //Dibujar la linea 
                    lona.DrawLine(lapiz,punto1,punto2);
                    punto1 = punto2;//El punto 2 se convierte en el punto 1 
                }
            }
        }

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            lapiz.Width=int.Parse(toolStripComboBox2.SelectedItem.ToString());
           
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            diametro = int.Parse((toolStripComboBox1.SelectedItem.ToString()));
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void dibujarFigura2dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //lapiz.Width = 1;
            //lapiz.Color=Color.Blue;
            //int num_puntos;//Numero de puntos que lello el list box
            //num_puntos= listBox1.Items.Count;
            ////Crear un arreglo tipo puntos donde se guarde lo de listbox
            //Point[] puntos//Creamos el arreglo puntos
            //              = new Point[num_puntos];
            //for (int i = 0;i< num_puntos; i++)
            //{
            //    //puntos[i] = listBox1.Items[i];
            //}
            //Se dibujara un triangulo con 4 ejes


            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\1.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Yellow;
            Leer_puntos();
            Dibujar_figura();
           
            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\2.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Pink;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\3.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Red;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\4.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Red;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\5.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Brown;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\8.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\9.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\10.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\blanco.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.White;
            Leer_puntos();
            Dibujar_figura();


            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\oreja1.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\oreja2.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();


            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\cola.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

            lector = new StreamReader("C:\\Users\\danie\\OneDrive\\Escritorio\\Escuela\\4 Graficación\\nariz.txt");
            lapiz.Color = Color.Black;
            brocha.Color = Color.Black;
            Leer_puntos();
            Dibujar_figura();

        }

        private void trianguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se dibujara un triangulo con 4 ejes
            lona.Clear(Color.White);
            lapiz.Width = 5;
            lapiz.Color = Color.Blue;
            //Dibujamos el eje vertical
            lona.DrawLine(lapiz,this.Width/2,0, this.Width / 2,this.Height);
            //la linea Horizontal
            lona.DrawLine(lapiz,0,this.Height/2,this.Width,this.Height/2);

            // el primer cuadrante 
            p1.X=this.Width/2;  
            p1.Y=0;//Se mueve
            p2.X = this.Width / 2; //Se mueve
            p2.Y = this.Height/2;

            //Ciclo
            lapiz.Width = 1;
            lapiz.Color = Color.Green;
            for (p1.Y  = 0; p1.Y < (this.Height / 2); p1.Y+=10)
            {
                lona.DrawLine(lapiz,p1,p2);
                p2.X += 10;
            }

            // el segundo cuadrante 
            p1.X = this.Width / 2;
            p1.Y = 0;//Se mueve
            p2.X = this.Width / 2; //Se mueve
            p2.Y = this.Height / 2;

            //Ciclo 2
            lapiz.Width = 1;
            lapiz.Color = Color.Purple;
            for (p1.Y = 0; p1.Y < (this.Height / 2); p1.Y += 10)
            {
                lona.DrawLine(lapiz, p1, p2);
                p2.X -= 10;
            }


            //tercer cuadrante
            lapiz.Color = Color.Red;
            p1.X = this.Width / 2;
            p1.Y = this.Height; // Iniciar en el fondo de la pantalla
            p2.X = this.Width / 2;
            p2.Y = this.Height / 2;

            for (p1.Y = this.Height; p1.Y > (this.Height / 2); p1.Y -= 10)
            {
                lona.DrawLine(lapiz, p1, p2);
                p2.X += 10;
            }

            //cuarto cuadrante
            lapiz.Color = Color.HotPink;
            p1.X = this.Width / 2;
            p1.Y = this.Height; // Iniciar en el fondo de la pantalla
            p2.X = this.Width / 2;
            p2.Y = this.Height / 2;

            for (p1.Y = this.Height; p1.Y > (this.Height / 2); p1.Y -= 10)
            {
                lona.DrawLine(lapiz, p1, p2);
                p2.X -= 10;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            lapiz.Color = colorDialog1.Color;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            brocha.Color = colorDialog1.Color;
            
        }

        private void figurasGeometricasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se dibujara un triangulo con 4 ejes
            lona.Clear(Color.White);
            // Dibujar cilindro
            lona.DrawEllipse(lapiz, 200, 200, 100, 25);
            lona.DrawEllipse(lapiz, 200, 100, 100, 25);
        

            // Dibujar los rectángulos
            lona.DrawRectangle(lapiz, 100, 300, 100, 50); 
            lona.DrawRectangle(lapiz, 25, 375, 100, 50);

            // Dibujar los triángulos con un desplazamiento adicional de 200 en x
            lona.DrawPolygon(lapiz, new Point[] { new Point(350, 300), new Point(400, 250), new Point(450, 300) });
            lona.DrawPolygon(lapiz, new Point[] { new Point(275, 375), new Point(325, 325), new Point(375, 375) });

            // Dos lineas para conectar los cilindres
            lona.DrawLine(lapiz, 200, 110, 200, 220);
            lona.DrawLine(lapiz, 300, 110, 300, 220);

            // Conectar los vértices de ambos rectángulos con líneas
            lona.DrawLine(lapiz, 100, 300, 25, 375); 
            lona.DrawLine(lapiz, 200, 300, 125, 375);
            lona.DrawLine(lapiz, 200, 350, 125, 425);
            lona.DrawLine(lapiz, 100, 350, 25, 425);

            // Conectar vértices de los triángulos
            lona.DrawLine(lapiz, 350, 300, 275, 375);
            lona.DrawLine(lapiz, 400, 250, 325, 325);
            lona.DrawLine(lapiz, 450, 300, 375, 375);

        }

        private void circulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lona.Clear(Color.White);
            Random rnd = new Random();

            // Genera valores aleatorios para cada sección del círculo
            int g1 = rnd.Next(1, 91);  // La suma de todos estos valores no debe superar 360
            int g2 = rnd.Next(1, 91);
            int g3 = rnd.Next(1, 91);
            int g4 = rnd.Next(1, 91);

            // Calcula el valor restante para completar el círculo
            int g5 = 360 - (g1 + g2 + g3 + g4);


            // Generar valores aleatorios para los componentes rojo, verde y azul
           int red = rnd.Next(256); // Valores entre 0 y 255
           int green = rnd.Next(256);
           int blue = rnd.Next(256);

            int red1 = rnd.Next(256); // Valores entre 0 y 255
            int green1 = rnd.Next(256);
            int blue1 = rnd.Next(256);

            int red2 = rnd.Next(256); // Valores entre 0 y 255
            int green2 = rnd.Next(256);
            int blue2 = rnd.Next(256);

            int red3 = rnd.Next(256); // Valores entre 0 y 255
            int green3 = rnd.Next(256);
            int blue3 = rnd.Next(256);

            lona.DrawPie(lapiz, 100, 100, 400, 400, 0, g1);
            lona.FillPie(brocha, 100, 100, 400, 400, 0, g1);
            brocha.Color = Color.FromArgb(red, green, blue);
            lona.DrawPie(lapiz, 100, 100, 400, 400, g1, g2);
            lona.FillPie(brocha, 100, 100, 400, 400, g1, g2);
            brocha.Color = Color.FromArgb(red1, green1, blue1);
            lona.DrawPie(lapiz, 100, 100, 400, 400, (g1 + g2), g3);
            lona.FillPie(brocha, 100, 100, 400, 400, (g1 + g2), g3);
            brocha.Color = Color.FromArgb(red2, green2, blue2);
            lona.DrawPie(lapiz, 100, 100, 400, 400, (g1 + g2 + g3), g4);
            lona.FillPie(brocha, 100, 100, 400, 400, (g1 + g2 + g3), g4);
            brocha.Color = Color.FromArgb(red3, green3, blue3);
            lona.DrawPie(lapiz, 100, 100, 400, 400, (g1 + g2 + g3 + g4), g5);
            lona.FillPie(brocha, 100, 100, 400, 400, (g1 + g2 + g3 + g4), g5);
        }

        private void barrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se dibujara un triangulo con 4 ejes
            lona.Clear(Color.White);
            //lineaL
            lapiz.Width = 5;
            lapiz.Color = Color.DarkBlue;
            lona.DrawLine(lapiz, 100, 0, 100, this.Height);
            lona.DrawLine(lapiz, 0, (this.Height - 100), this.Width, (this.Height - 100));
            //Dibujo barra1
            brocha.Color = Color.Purple;
            lona.FillRectangle(brocha, 150, (this.Height - 300), 150, 200);
            //dibujo barra2
            brocha.Color = Color.Cyan;
            lona.FillRectangle(brocha, 450, (this.Height - 450), 150, 350);
            //dibujo barra3
            brocha.Color = Color.Magenta;
            lona.FillRectangle(brocha, 750, (this.Height - 150), 150, 50);
            //dibujo barra4
            brocha.Color = Color.DarkRed;
            lona.FillRectangle(brocha, 1050, (this.Height - 275), 150, 175);
        }
    }
}