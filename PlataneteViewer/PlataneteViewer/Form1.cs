using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace PlataneteViewer
{
    public partial class Form1 : Form
    {

        ChromiumWebBrowser browser;
        WindowsCrap keyManager;
        public Form1()
        {
            InitializeComponent();
            //Inicializamos Navegador
            tbxUrl.Text = Application.StartupPath+"/index.html";
            browser = new ChromiumWebBrowser(tbxUrl.Text);
            panel1.Controls.Add(browser);
            
            // Instanciamos clase WindowsCrap =P
            keyManager = new WindowsCrap();

            //Instanciamos nuestro controlador asincrono
            ManejadorEventosJs controladorAsincrono = new ManejadorEventosJs();
            controladorAsincrono.mensajeRecibido += alRecibirMensaje;

            //Asociamos nuesto objeto handler al navegador.
            browser.JavascriptObjectRepository.Register("llamadaAsincrona", controladorAsincrono, true);

            //Apagamos esta bandera para que otros hilos puedan modificar la ventana
            CheckForIllegalCrossThreadCalls = false;
        }

        private void alRecibirMensaje(object sender, string e) {
            label1.Text = "Ultimo mensaje: "+e;
            switch(e)
            {
                case "UP":
                    {
                        WindowsCrap.Press(WindowsCrap.ScanCodeShort.UP);
                        WindowsCrap.Release(WindowsCrap.ScanCodeShort.UP);
                        break;
                    }
                case "DOWN":
                    {
                        WindowsCrap.Press(WindowsCrap.ScanCodeShort.DOWN);
                        WindowsCrap.Release(WindowsCrap.ScanCodeShort.DOWN);
                        break;
                    }
                case "LEFT":
                    {
                        WindowsCrap.Press(WindowsCrap.ScanCodeShort.LEFT);
                        WindowsCrap.Release(WindowsCrap.ScanCodeShort.LEFT);
                        break;
                    }
                case "RIGHT":
                    {
                        WindowsCrap.Press(WindowsCrap.ScanCodeShort.RIGHT);
                        WindowsCrap.Release(WindowsCrap.ScanCodeShort.RIGHT);
                        break;
                    }
            }
   
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Abrimos la URL escrita
            browser.Load(tbxUrl.Text);
        }

        private void btnBrowse_KeyDown(object sender, KeyEventArgs e)
        {
            //Detonamos el evento click en el boton
            if (e.KeyCode == Keys.Enter)
            {
                btnBrowse_Click(this, new EventArgs());
            }
        }
    }

    public class ManejadorEventosJs {

        public EventHandler<string> mensajeRecibido;
        public void enviarMensaje(string mensaje)
        {
            if (mensajeRecibido != null)
            {
                mensajeRecibido(this,mensaje);
            }
        }
    }
}
