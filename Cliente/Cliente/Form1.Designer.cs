namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.UsuarioBox = new System.Windows.Forms.TextBox();
            this.ContraseñaBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chatM = new System.Windows.Forms.TextBox();
            this.chat = new System.Windows.Forms.ListBox();
            this.invitartxt = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.abandonar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.userr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.invitar = new System.Windows.Forms.Button();
            this.EnviarM = new System.Windows.Forms.Button();
            this.Registrar = new System.Windows.Forms.Button();
            this.Iniciar = new System.Windows.Forms.Button();
            this.desconectar = new System.Windows.Forms.Button();
            this.conectar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // UsuarioBox
            // 
            this.UsuarioBox.Location = new System.Drawing.Point(16, 134);
            this.UsuarioBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.UsuarioBox.Name = "UsuarioBox";
            this.UsuarioBox.Size = new System.Drawing.Size(132, 22);
            this.UsuarioBox.TabIndex = 9;
            // 
            // ContraseñaBox
            // 
            this.ContraseñaBox.Location = new System.Drawing.Point(219, 134);
            this.ContraseñaBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ContraseñaBox.Name = "ContraseñaBox";
            this.ContraseñaBox.Size = new System.Drawing.Size(132, 22);
            this.ContraseñaBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlText;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OldLace;
            this.label1.Location = new System.Drawing.Point(16, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "USUARIO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlText;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(216, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "CONTRASEÑA:";
            // 
            // edad
            // 
            this.edad.Location = new System.Drawing.Point(16, 403);
            this.edad.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.edad.Name = "edad";
            this.edad.Size = new System.Drawing.Size(105, 22);
            this.edad.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Bisque;
            this.label3.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(16, 376);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 22);
            this.label3.TabIndex = 34;
            this.label3.Text = "Edad";
            // 
            // chatM
            // 
            this.chatM.Location = new System.Drawing.Point(859, 346);
            this.chatM.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.chatM.Name = "chatM";
            this.chatM.Size = new System.Drawing.Size(319, 22);
            this.chatM.TabIndex = 37;
            // 
            // chat
            // 
            this.chat.BackColor = System.Drawing.Color.Turquoise;
            this.chat.Font = new System.Drawing.Font("High Tower Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chat.FormattingEnabled = true;
            this.chat.ItemHeight = 19;
            this.chat.Location = new System.Drawing.Point(859, 19);
            this.chat.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(319, 270);
            this.chat.TabIndex = 38;
            // 
            // invitartxt
            // 
            this.invitartxt.Location = new System.Drawing.Point(497, 231);
            this.invitartxt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.invitartxt.Name = "invitartxt";
            this.invitartxt.Size = new System.Drawing.Size(187, 22);
            this.invitartxt.TabIndex = 40;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(586, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(245, 137);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Font = new System.Drawing.Font("Castellar", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkKhaki;
            this.label10.Location = new System.Drawing.Point(458, 403);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(309, 25);
            this.label10.TabIndex = 44;
            this.label10.Text = "Aceptar la Invitacion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.BlueViolet;
            this.label4.Font = new System.Drawing.Font("Poor Richard", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(395, 199);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(456, 24);
            this.label4.TabIndex = 45;
            this.label4.Text = "Escribe aqui el nombre del usuario conectado para invitar.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 335);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 46;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(187, 335);
            this.textBox2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.label5.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(16, 298);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 22);
            this.label5.TabIndex = 48;
            this.label5.Text = "Usuario";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Bisque;
            this.label6.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(187, 298);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 22);
            this.label6.TabIndex = 49;
            this.label6.Text = "Contraseña";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(637, 86);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(141, 70);
            this.textBox3.TabIndex = 51;
            // 
            // abandonar
            // 
            this.abandonar.AutoSize = true;
            this.abandonar.BackColor = System.Drawing.Color.Aquamarine;
            this.abandonar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abandonar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.abandonar.Location = new System.Drawing.Point(941, 486);
            this.abandonar.Name = "abandonar";
            this.abandonar.Size = new System.Drawing.Size(199, 28);
            this.abandonar.TabIndex = 52;
            this.abandonar.Text = "Abandonar Chat";
            this.abandonar.UseVisualStyleBackColor = false;
            this.abandonar.Click += new System.EventHandler(this.abandonar_Click);
            this.abandonar.MouseEnter += new System.EventHandler(this.abandonar_MouseEnter);
            this.abandonar.MouseLeave += new System.EventHandler(this.abandonar_MouseLeave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Brown;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button1.Location = new System.Drawing.Point(291, 486);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 33);
            this.button1.TabIndex = 53;
            this.button1.Text = "Darse De Baja";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter_1);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave_1);
            // 
            // userr
            // 
            this.userr.Location = new System.Drawing.Point(291, 458);
            this.userr.Name = "userr";
            this.userr.Size = new System.Drawing.Size(136, 22);
            this.userr.TabIndex = 54;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.label7.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label7.Location = new System.Drawing.Point(291, 405);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 50);
            this.label7.TabIndex = 55;
            this.label7.Text = "Escribe su nombre para la baja";
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button3.Location = new System.Drawing.Point(637, 444);
            this.button3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 75);
            this.button3.TabIndex = 43;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.button3_MouseEnter_1);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave_1);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button2.Location = new System.Drawing.Point(462, 444);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 75);
            this.button2.TabIndex = 42;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter_1);
            this.button2.MouseLeave += new System.EventHandler(this.button2_MouseLeave_1);
            // 
            // invitar
            // 
            this.invitar.AutoSize = true;
            this.invitar.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.inviteee;
            this.invitar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.invitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invitar.Font = new System.Drawing.Font("Poor Richard", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitar.ForeColor = System.Drawing.Color.Cyan;
            this.invitar.Location = new System.Drawing.Point(487, 261);
            this.invitar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(211, 102);
            this.invitar.TabIndex = 39;
            this.invitar.Text = "Invitar";
            this.invitar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.invitar.UseVisualStyleBackColor = true;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            this.invitar.MouseEnter += new System.EventHandler(this.invitar_MouseEnter);
            this.invitar.MouseLeave += new System.EventHandler(this.invitar_MouseLeave_1);
            // 
            // EnviarM
            // 
            this.EnviarM.AutoSize = true;
            this.EnviarM.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.email;
            this.EnviarM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EnviarM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnviarM.Font = new System.Drawing.Font("Poor Richard", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarM.ForeColor = System.Drawing.Color.Yellow;
            this.EnviarM.Location = new System.Drawing.Point(939, 376);
            this.EnviarM.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.EnviarM.Name = "EnviarM";
            this.EnviarM.Size = new System.Drawing.Size(190, 87);
            this.EnviarM.TabIndex = 36;
            this.EnviarM.Text = "Enviar Mensaje";
            this.EnviarM.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.EnviarM.UseVisualStyleBackColor = true;
            this.EnviarM.Click += new System.EventHandler(this.EnviarM_Click);
            this.EnviarM.MouseEnter += new System.EventHandler(this.EnviarM_MouseEnter);
            this.EnviarM.MouseLeave += new System.EventHandler(this.EnviarM_MouseLeave);
            // 
            // Registrar
            // 
            this.Registrar.AutoSize = true;
            this.Registrar.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.re;
            this.Registrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Registrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Registrar.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Registrar.ForeColor = System.Drawing.Color.FloralWhite;
            this.Registrar.Location = new System.Drawing.Point(1, 431);
            this.Registrar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Registrar.Name = "Registrar";
            this.Registrar.Size = new System.Drawing.Size(262, 100);
            this.Registrar.TabIndex = 15;
            this.Registrar.UseVisualStyleBackColor = true;
            this.Registrar.Click += new System.EventHandler(this.Registrar_Click);
            this.Registrar.MouseEnter += new System.EventHandler(this.Registrar_MouseEnter);
            this.Registrar.MouseLeave += new System.EventHandler(this.Registrar_MouseLeave);
            // 
            // Iniciar
            // 
            this.Iniciar.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Iniciar.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.iniciar_sesion;
            this.Iniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Iniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Iniciar.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Iniciar.ForeColor = System.Drawing.Color.OldLace;
            this.Iniciar.Location = new System.Drawing.Point(74, 172);
            this.Iniciar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Iniciar.Name = "Iniciar";
            this.Iniciar.Size = new System.Drawing.Size(246, 81);
            this.Iniciar.TabIndex = 13;
            this.Iniciar.UseVisualStyleBackColor = false;
            this.Iniciar.Click += new System.EventHandler(this.Iniciar_Click);
            this.Iniciar.MouseEnter += new System.EventHandler(this.Iniciar_MouseEnter);
            this.Iniciar.MouseLeave += new System.EventHandler(this.Iniciar_MouseLeave);
            // 
            // desconectar
            // 
            this.desconectar.AutoSize = true;
            this.desconectar.BackColor = System.Drawing.Color.Gold;
            this.desconectar.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.disconnect;
            this.desconectar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.desconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desconectar.Font = new System.Drawing.Font("Poor Richard", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desconectar.ForeColor = System.Drawing.Color.LightBlue;
            this.desconectar.Location = new System.Drawing.Point(265, 1);
            this.desconectar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(254, 89);
            this.desconectar.TabIndex = 5;
            this.desconectar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.desconectar.UseVisualStyleBackColor = false;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click_1);
            this.desconectar.MouseEnter += new System.EventHandler(this.desconectar_MouseEnter);
            this.desconectar.MouseLeave += new System.EventHandler(this.desconectar_MouseLeave);
            // 
            // conectar
            // 
            this.conectar.AutoSize = true;
            this.conectar.BackColor = System.Drawing.Color.Gold;
            this.conectar.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.connect;
            this.conectar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.conectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.conectar.Font = new System.Drawing.Font("Poor Richard", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conectar.ForeColor = System.Drawing.Color.DodgerBlue;
            this.conectar.Location = new System.Drawing.Point(16, 1);
            this.conectar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(222, 89);
            this.conectar.TabIndex = 5;
            this.conectar.Text = "Conectar";
            this.conectar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.conectar.UseVisualStyleBackColor = false;
            this.conectar.Click += new System.EventHandler(this.conectar_Click_1);
            this.conectar.MouseEnter += new System.EventHandler(this.conectar_MouseEnter);
            this.conectar.MouseLeave += new System.EventHandler(this.conectar_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1197, 532);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.userr);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.abandonar);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.invitartxt);
            this.Controls.Add(this.invitar);
            this.Controls.Add(this.chat);
            this.Controls.Add(this.chatM);
            this.Controls.Add(this.EnviarM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edad);
            this.Controls.Add(this.Registrar);
            this.Controls.Add(this.Iniciar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContraseñaBox);
            this.Controls.Add(this.UsuarioBox);
            this.Controls.Add(this.desconectar);
            this.Controls.Add(this.conectar);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.desconectar_Click_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.TextBox UsuarioBox;
        private System.Windows.Forms.TextBox ContraseñaBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Iniciar;
        private System.Windows.Forms.Button Registrar;
        private System.Windows.Forms.TextBox edad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button EnviarM;
        private System.Windows.Forms.TextBox chatM;
        private System.Windows.Forms.ListBox chat;
        private System.Windows.Forms.Button invitar;
        private System.Windows.Forms.TextBox invitartxt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button abandonar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox userr;
        private System.Windows.Forms.Label label7;
    }
}

