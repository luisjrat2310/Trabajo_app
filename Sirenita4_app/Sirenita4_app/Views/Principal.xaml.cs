using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sirenita4_app.Views
{
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
        }


        private void btnregistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Agregar2());

        }

        private void btneditar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Editar());

        }

        private void btnlistar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Listar());

        }

        private void btneliminar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Eliminar());

        }
    }
}
