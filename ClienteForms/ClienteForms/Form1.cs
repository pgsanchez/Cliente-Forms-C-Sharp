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

namespace ClienteForms
{
    public partial class Form1 : Form
    {
        Socket socket1;
        IPEndPoint direccion;

        public Form1()
        {
            InitializeComponent();
            textBoxMsgs.Text = "Iniciando conexión...";
            socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                socket1.Connect(direccion);
                textBoxMsgs.Text = "Cliente conectado con éxito";
            }
            catch (Exception error)
            {
                // escribir en el msgbox que ha habido un error
                textBoxMsgs.Text = "Error de conexión: " + error.ToString();
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                socket1.Close();
                textBoxMsgs.Text = "Cliente desconectado";
            }
            catch (Exception error)
            {
                // escribir en el msgbox que ha habido un error
                textBoxMsgs.Text = "Error al desconectar: " + error.ToString();
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            byte[] txtAEnviar;
	        txtAEnviar = Encoding.Default.GetBytes("Hola, soy el cliente");
            socket1.Send(txtAEnviar);
            textBoxMsgs.Text = "Texto de prueba enviado";

            // Esperamos a recibir algo
            byte[] byARecibir = new byte[255];
            int longitud = socket1.Receive(byARecibir);
            Array.Resize(ref byARecibir, longitud);
            //Console.WriteLine(Encoding.Default.GetString(byARecibir));
            textBoxMsgs.Text = Encoding.Default.GetString(byARecibir);
        }
    }
}
