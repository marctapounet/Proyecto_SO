using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void conectar_Click_1(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9800);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

        }



        private void desconectar_Click_1(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            string mensaje = "2/" + UsuarioBox.Text + "/" + ContraseñaBox.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg3 = new byte[80];
            server.Receive(msg3);
            string mensajevuelta;
            mensajevuelta = Encoding.ASCII.GetString(msg3).Split('\0')[0];
            if (mensajevuelta == "Correctol")
            {
                MessageBox.Show("Se ha logeado");

            }
            else
                MessageBox.Show("Incorrecto");
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + UsuarioBox.Text + "/" + ContraseñaBox.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg3 = new byte[80];
            server.Receive(msg3);
            string mensajevuelta;
            mensajevuelta = Encoding.ASCII.GetString(msg3).Split('\0')[0];
            if (mensajevuelta == "Correctol")
            {
                MessageBox.Show("Sesion cerrada conrrectamente");

            }
            else
                MessageBox.Show("Incorrecto");
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            string mensaje = "3/" + UsuarioBox.Text + "/" + ContraseñaBox.Text + "/" + Confirmar_Contra.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg3 = new byte[80];
            server.Receive(msg3);
            string mensajevuelta;
            mensajevuelta = Encoding.ASCII.GetString(msg3).Split('\0')[0];
            if (mensajevuelta == "Correctol")
            {
                MessageBox.Show("Se ha registrado");

            }
            else
                MessageBox.Show("Incorrecto");
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserName.Checked)
            {
                string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text + "/" + textBox3.Text + "/" + textBox4.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("User Name del Jugador es: " + mensaje);
            }


            if (id_partidas.Checked)
            {
                string mensaje = "2/" + textBox5.Text + "/" + textBox4.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("id de las partidas es: " + mensaje);
            }



        }

        
    }
}

