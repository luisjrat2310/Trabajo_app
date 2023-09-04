using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Sirenita4_app.Views
{
    public class principal2 : ContentPage
    {
        public principal2()
        {
            this.Title = "Seleccione la opción";

            StackLayout stackLayout = new StackLayout();
            Button button = new Button();
            button.Text = "Registrar cliente";
            button.Clicked += Button_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Listar";
            button.Clicked += Button_Clicked1; 
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Editar";
            button.Clicked += Button_Clicked2; 
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Eliminar";
            button.Clicked += Button_Clicked3; 
            stackLayout.Children.Add(button);

            Content = stackLayout;
        }

        private async void Button_Clicked3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Eliminar());
        }

        private async void Button_Clicked2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Editar());
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listar());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agregar());
        }

        
    }
}