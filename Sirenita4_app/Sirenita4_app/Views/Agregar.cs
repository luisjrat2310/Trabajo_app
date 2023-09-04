using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sirenita4_app.Models;

using Xamarin.Forms;

namespace Sirenita4_app.Views
{
    public class Agregar : ContentPage
    {
        private Entry _nombreEntry;
        private Entry _apellidoEntry;
        private Entry _correoEntry;
        private Entry _telefonoEntry;
        private Entry _edadEntry;
        private Entry _horaEntry;
        private Button _guardarButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");

        public Agregar()
        {
            this.Title = "Ingrese datos";

            StackLayout stackLayout = new StackLayout();
            
           
            _nombreEntry = new Entry();
            _nombreEntry.Keyboard = Keyboard.Text;
            _nombreEntry.Placeholder = "Ingrese su nombre";
            stackLayout.Children.Add( _nombreEntry );

            _apellidoEntry = new Entry();
            _apellidoEntry.Keyboard = Keyboard.Text;
            _apellidoEntry.Placeholder = "Ingrese sus apellidos";
            stackLayout.Children.Add(_apellidoEntry);

            _correoEntry = new Entry();
            _correoEntry.Keyboard = Keyboard.Text;
            _correoEntry.Placeholder = "Ingrese su correo";
            stackLayout.Children.Add(_correoEntry);

            _telefonoEntry = new Entry();
            _telefonoEntry.Keyboard = Keyboard.Text;
            _telefonoEntry.Placeholder = "Ingrese su telefono";
            stackLayout.Children.Add(_telefonoEntry);

            _edadEntry = new Entry();
            _edadEntry.Keyboard = Keyboard.Text;
            _edadEntry.Placeholder = "Ingrese su edad";
            stackLayout.Children.Add(_edadEntry);

      

            _guardarButton = new Button();
            _guardarButton.Text = "Agregar";
            _guardarButton.Clicked += _guardarBurron_Clicked;
            stackLayout.Children.Add(_guardarButton);

            Content = stackLayout;

        }

        private async void _guardarBurron_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<registro>();

            var maxPK = db.Table<registro>().OrderByDescending(a => a.Id).FirstOrDefault();

            registro agregar = new registro()
            {
                Id = (maxPK == null ? 1 : maxPK.Id + 1),
                nombre = _nombreEntry.Text,
                Apellidos = _apellidoEntry.Text,
                correo = _correoEntry.Text,
                telefono = Convert.ToInt32(_telefonoEntry.Text),
                edad = Convert.ToInt32(_edadEntry.Text),
                
            };
            db.Insert(agregar);
            await DisplayAlert(null, "Guardado", "ok");
            await Navigation.PopAsync();
        }
    }
}