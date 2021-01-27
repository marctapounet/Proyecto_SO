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
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        delegate void DelegadoParaEscribir(string mensaje);
        List<string> list = new List<string>();
        Socket server;
        Thread atender;
        string user;
        string emisor;        
        string jugador1;
        string jugador2;
        //string user2;
       // string user3;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            button2.Enabled = false; //aceptar la invitacion
            button3.Enabled = false;  //rechazar la invitacion
            EnviarM.Enabled = false;
            invitar.Enabled = false;
            Registrar.Enabled = false;
            desconectar.Enabled = false;
            Iniciar.Enabled = false;
            abandonar.Enabled = false;
            button1.Enabled = false;  //boton para darse de baja

        }


       // int iniciar = 0;
        
       int inviAceptado = 0;
       int conectadoss = 0;
        private void conectar_Click_1(object sender, EventArgs e)
        {
           
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos

                //localhost, entorno de desarollo.
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, 9640);

                   //Shiva, entorno de produccion.
                 //IPAddress direc = IPAddress.Parse("147.83.117.22");
                 //IPEndPoint ipep = new IPEndPoint(direc, 50091);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    //this.BackColor = Color.Green;
                    this.BackgroundImage = Properties.Resources.welcome_1;
                    MessageBox.Show("Conectado");

                }
                catch (SocketException)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                ThreadStart ts = delegate { atender_servidor(); };
                atender = new Thread(ts);
                atender.Start();
                Registrar.Enabled = true;
                desconectar.Enabled = true;
                Iniciar.Enabled = true;
                conectar.Enabled = false;
                abandonar.Enabled = false;

        }
        private void desconectar_Click_1(object sender, EventArgs e)
        {
            //if (desconectarse == 0)
            //{

                dataGridView1.ColumnCount = 1;
                dataGridView1.RowCount = 1;
                dataGridView1.Rows[0].Cells[0].Value = " ";
                //desconectarse = 1;
                //Mensaje de desconexión
                string mensaje = "0/" + user;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                this.BackgroundImage = Properties.Resources.adios;
                // Nos desconectamos
                atender.Abort();
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                //iniciar = 0;

                textBox3.Clear();
                desconectar.Enabled = false;
                invitar.Enabled = false;
                Registrar.Enabled = false;
                Iniciar.Enabled = false;
                conectar.Enabled = true;
                user = "";
                jugador1 = "";
                jugador2 = "";
                abandonar.Enabled = false;
                button1.Enabled = false;
                
        }
        private void Iniciar_Click(object sender, EventArgs e)
        {
            
                    if ((UsuarioBox.Text == "") || (ContraseñaBox.Text == ""))
                    {
                        MessageBox.Show(" Escribe usuario y contraseña");
                    }
                    else
                    {
                        user = UsuarioBox.Text;
                        string mensaje = "1/" + UsuarioBox.Text + "/" + ContraseñaBox.Text;
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        
                    }

                     }

        
        
        private void Registrar_Click(object sender, EventArgs e)
        {

            
                if ((textBox1.Text == "") || (textBox2.Text == "") || (edad.Text == ""))
                {
                    MessageBox.Show("Tienes que rellenar los 3 campos para registrarte.");
                }
                else
                {
                        
                    string mensaje = "2/" + textBox1.Text + "/" + textBox2.Text + "/" + edad.Text;
                    
                    edad.Clear();
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
           
        }




        private void atender_servidor()
        {
            try {
            while (true)
            {
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string mm = Encoding.ASCII.GetString(msg2);
                
                string[] trozos = mm.Split('/');

                int codigo = Convert.ToInt32(trozos[0]);

                string mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {
                    case 1:
                         // si nos llega un 1, usuario o contraseña sea incorrecto
                        MessageBox.Show("usuario o contraseña incorrecto");
                        Iniciar.Enabled = true;
                        Registrar.Enabled = true;
                        desconectar.Enabled = true;
                        conectar.Enabled = false;

                        break;

                    case 2:
                        // se ha podido hacer login el usuario 

                        MessageBox.Show("bienvenido: " + mensaje);
                        user = UsuarioBox.Text;
                        //iniciar = 1;
                        Iniciar.Enabled = false;
                        Registrar.Enabled = false;
                        invitar.Enabled = true;
                        desconectar.Enabled = true;
                        button1.Enabled = true;

                        break;

                    case 3:
                        // si nos llega un 3/ok es, signific que se ha registrado correctamente

                        if (mensaje == "ok")
                        {
                            MessageBox.Show(" se ha registrado correctamente");
                            Registrar.Enabled = false;

                        }
                        else
                            MessageBox.Show("No ha sido posible registrarse");

                        break;

                    case 4:
                        //usuario se da de baja.
                        if (mensaje == "ELIMINADO")

                        {
                            MessageBox.Show("El usuario ha sido eliminado de la base de datos. POR FAVOR, DESCONÉCTESE!!");
                            //MessageBox.Show("Si desea registrarse, otra vez, debe desconectar primero y volver a conectarse");
                            dataGridView1.ColumnCount = 1;
                            dataGridView1.RowCount = 1;
                            dataGridView1.Rows[0].Cells[0].Value = " ";
                            textBox3.Clear();

                            
                        }

                        else
                            MessageBox.Show("El usuario no se ha podido eliminar.");

                        break;

                    case 5: //lista conectados 





                        textBox3.Clear();
                        conectadoss = 0;
                        string[] separadas = new string[5];
                        separadas = mensaje.Split('/');

                        for (int i = 0; i < Convert.ToInt32(separadas[0]); i++)
                        {
                            if (trozos[i + 2] != user)  // el jugador conectado no ve su propio nombre dentro de la lista de conectados
                            {
                                textBox3.AppendText(trozos[i + 2]);
                                textBox3.AppendText(Environment.NewLine);
                                conectadoss++;
                            }
                        }




                        try
                        {
                            string[] vector = new string[5];
                            vector = mensaje.Split('/');

                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.ColumnHeadersVisible = false;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                            dataGridView1.RowCount = vector.Length;
                            dataGridView1.ColumnCount = 1;

                            int i = 0;
                            while (i < vector.Length)
                            {

                                if (i == 0)
                                {
                                    dataGridView1.Rows[i].Cells[0].Value = "En Linea: " + vector[i];
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[0].Value = vector[i];
                                }
                                i++;
                            }
                        }
                        catch (FormatException) { }
                        //catch (ArgumentException) { }*/

                        
                        break;

                    case 6:
                        //cuando llega una invitacion del otro usuario.

                        if (mensaje == "ok")
                        {
                            emisor = trozos[2].Split('\0')[0];
                            button2.Enabled = true;
                            button3.Enabled = true;
                            invitar.Enabled = false;

                            
                            this.BackgroundImage = Properties.Resources.notification;
                            MessageBox.Show("El usuario " + emisor + " te ha invitado");
                            //inviRecibido = 1;
                            jugador1 = user;
                            jugador2 = emisor;
                        }
                        break;

                    case 7:
                        // el usuario invitado ha aceptado la invitacion
                        if (mensaje == "aceptado")
                        {
                            
                            this.BackgroundImage = Properties.Resources.livechat;
                            MessageBox.Show("Ha aceptado tu invitación");
                            abandonar.Enabled = true;
                            invitar.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            EnviarM.Enabled = true;
                        }

                        break;

                    case 8:
                        // usuario ha rechazado la invitacion

                        if (mensaje == "rechazado")
                        {
                            MessageBox.Show("Tu invitación ha sido rechazada");

                            invitar.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = false;
                        }

                        break;

                    case 9:
                        // intercambio de mensajes, pone los mensajes en el chat
                        

                        chat.Items.Add(mensaje);
                        chat.TopIndex = chat.Items.Count - 1;

                        break;                    

                    case 10:
                        // usuario ha abandonado el chat
                        invitar.Enabled = true;
                        abandonar.Enabled = false;
                        desconectar.Enabled = true;
                        EnviarM.Enabled = false;
                        this.BackgroundImage = Properties.Resources.welcome_1;
                        string message3 = "Ha abandonado el chat";
                        string message4 = message3;

                            if (inviAceptado == 1)
                    {
                        message3 = jugador1 + ": " + message3;
                        message4 = jugador2 + ": " + message4;
                    }
                    else
                    {
                        message3 = jugador2 + ": " + message3;
                        message4 = jugador1 + ": " + message4;
                    }


                    chat.Items.Add(message3);
                    chat.Items.Add(message4);
                    chat.TopIndex = chat.Items.Count - 1;

                jugador1 = "";
                jugador2 = "";
                        break;
                }
            }
        }
        
                 catch (FormatException)
                {
                    //MessageBox.Show("Error de formato");
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error SOCKET EXCEPTION");
                }
           }
            
        

        private void invitar_Click(object sender, EventArgs e)
        {
            // se encarrega de enviar la invitacion al usuario.
              
                    if (invitartxt.Text == "")
                    {
                        MessageBox.Show("Escribe el nombre de un usuario CONECTADO");
                    }
                    else if (invitartxt.Text == user)
                    {
                        MessageBox.Show("No puedes chatear contra ti mismo, lo sentimos :)");
                    }
                    else
                    {

                        string mensaje = "4/" + invitartxt.Text;
                        
                        if (invitartxt.Text == null)
                        {
                            MessageBox.Show("Introduce el nombre de la persona que quieres invitar. Recuerda que la persona que quieres invitar estará en los conectados.");
                        }
                        else
                        {
                            jugador1 = invitartxt.Text;
                            jugador2 = user;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                            server.Send(msg);
                        }
                    }
                        
        }

        private void EnviarM_Click(object sender, EventArgs e)
        {
            //boton para enviar mensaje al usuario
                       
                            string Mensaj = chatM.Text;

                            Mensaj = Mensaj.Replace("/", "");


                            if (inviAceptado == 1)
                            {

                                Mensaj = jugador1 + ": " + Mensaj;

                            }
                            else
                            {
                                Mensaj = jugador2 + ": " + Mensaj;
                            }


                            chat.Items.Add(Mensaj);
                            chat.TopIndex = chat.Items.Count - 1;

                            if (inviAceptado == 1)
                            {


                                string mensaje = "7/" + Mensaj + "/" + jugador2;
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                server.Send(msg);
                            }
                            else
                            {
                                string mensaje = "7/" + Mensaj + "/" + jugador1;
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                server.Send(msg);
                            }



                            chatM.Text = "";
                        
                        
                    }
                   
                
        
        private void button2_Click(object sender, EventArgs e)
        {
            //boton de aceptar la invitacion
            
                         inviAceptado = 1;
                         string mensaje = "5/" + emisor;
                         byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                         server.Send(msg);
                         
                         this.BackgroundImage = Properties.Resources.livechat;
                         invitar.Enabled = false;
                         button2.Enabled = false;
                         button3.Enabled = false;
                         EnviarM.Enabled = true;
                         abandonar.Enabled = true;
                    }
                  

        private void button3_Click(object sender, EventArgs e)
        {
            // boton de rechazar la invitacion
            string mensaje = "6/" + emisor;
            invitar.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            this.BackColor = Color.Green;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.form_load;
            conectadoss = 0;
        }
        

        private void abandonar_Click(object sender, EventArgs e)
        {

            // boton de abandonar el chat
            invitar.Enabled = true;

            string message3 = "Ha abandonado el chat";
            string message4 = message3;

            if (inviAceptado == 1)
            {
                message3 = jugador1 + ": " + message3;
                message4 = jugador2 + ": " + message4;
            }
            else
            {
                message3 = jugador2 + ": " + message3;
                message4 = jugador1 + ": " + message4;
            }


            chat.Items.Add(message3);
            chat.Items.Add(message4);
            chat.TopIndex = chat.Items.Count - 1;

            if (inviAceptado == 1)
            {
                string mensaje2 = "8/" + jugador2;
                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg2);
                jugador1 = "";
                jugador2 = "";
                this.BackgroundImage = Properties.Resources.welcome_1;
                this.BackColor = Color.Green;
                desconectar.Enabled = true;
                EnviarM.Enabled = false;
            }
            else
            {
                string mensaje2 = "8/" + jugador1;
                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                server.Send(msg2);
                jugador1 = "";
                jugador2 = "";
                this.BackgroundImage = Properties.Resources.welcome_1;
                this.BackColor = Color.Green;
                desconectar.Enabled = true;
                EnviarM.Enabled = false;
            }
            abandonar.Enabled = false;
        }


        // apartir de aqui los codigos son para el diseño de los botones.


        private void desconectar_MouseEnter(object sender, EventArgs e)
        {
            desconectar.FlatAppearance.BorderSize = 5;
            desconectar.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void desconectar_MouseLeave(object sender, EventArgs e)
        {
            desconectar.FlatAppearance.BorderSize = 0;
            desconectar.FlatAppearance.BorderColor = desconectar.BackColor;
        }

        private void conectar_MouseEnter(object sender, EventArgs e)
        {
            conectar.FlatAppearance.BorderSize = 5;
            conectar.FlatAppearance.BorderColor = Color.Yellow ;
        }

        private void conectar_MouseLeave(object sender, EventArgs e)
        {
            conectar.FlatAppearance.BorderSize = 0;
            conectar.FlatAppearance.BorderColor = conectar.BackColor;
        }

        private void Iniciar_MouseEnter(object sender, EventArgs e)
        {
            Iniciar.FlatAppearance.BorderSize = 5;
            Iniciar.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void Iniciar_MouseLeave(object sender, EventArgs e)
        {
            Iniciar.FlatAppearance.BorderSize = 0;
            Iniciar.FlatAppearance.BorderColor = Iniciar.BackColor ;
        }

        private void Registrar_MouseEnter(object sender, EventArgs e)
        {
            Registrar.FlatAppearance.BorderSize = 5;
            Registrar.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void Registrar_MouseLeave(object sender, EventArgs e)
        {
            Registrar.FlatAppearance.BorderSize = 0;
            Registrar.FlatAppearance.BorderColor = Registrar.BackColor;
        }

        private void invitar_MouseEnter(object sender, EventArgs e)
        {
            invitar.FlatAppearance.BorderSize = 5;
            invitar.FlatAppearance.BorderColor = Color.Yellow;
        }
       

        private void EnviarM_MouseLeave(object sender, EventArgs e)
        {
            EnviarM.FlatAppearance.BorderSize = 0;
            EnviarM.FlatAppearance.BorderColor = EnviarM.BackColor;
        }

        private void EnviarM_MouseEnter(object sender, EventArgs e)
        {
            EnviarM.FlatAppearance.BorderSize = 5;
            EnviarM.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void abandonar_MouseEnter(object sender, EventArgs e)
        {
            abandonar.FlatAppearance.BorderSize = 5;
            abandonar.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void abandonar_MouseLeave(object sender, EventArgs e)
        {
            abandonar.FlatAppearance.BorderSize = 0;
            abandonar.FlatAppearance.BorderColor = abandonar.BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            // boton para darse de baja.

            if (userr.Text == user)
            {
                string mensaje = "3/" + userr.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                this.BackgroundImage = Properties.Resources.sad;
                button1.Enabled = false;
            }
            else
                MessageBox.Show("NO puedes dar de baja a otro usuario");
        }      

       
        private void button1_MouseEnter_1(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 5;
            button1.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void button1_MouseLeave_1(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderColor = Color.Green;
        }

        private void invitar_MouseLeave_1(object sender, EventArgs e)
        {
            invitar.FlatAppearance.BorderSize = 0;
            invitar.FlatAppearance.BorderColor = invitar.BackColor;
        }

        private void button2_MouseEnter_1(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 5;
            button2.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void button2_MouseLeave_1(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderColor = button2.BackColor;
        }

        private void button3_MouseEnter_1(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 5;
            button3.FlatAppearance.BorderColor = Color.Yellow;
        }

        private void button3_MouseLeave_1(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderColor = button3.BackColor;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {  //para que no salga el cuadro de error que sale cuando se muestra la lista de conectados automaticos.
            e.Cancel = true;
        }

    }
}

