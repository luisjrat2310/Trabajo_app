using Sirenita4_app.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Sirenita4_app.Views
{
    public class Editar : ContentPage
    {
        private ListView _listView;
        private Entry _identry;
        private Entry _nombreEntry;
        private Entry _apellidoEntry;
        private Entry _correoEntry;
        private Entry _telefonoEntry;
        private Entry _edadEntry;
        private Button _actualizarButton;

        registro _registro = new registro();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");
        public Editar()
        {
            this.Title = "Editar";
            BackgroundColor = Color.LightBlue;

            var db = new SQLiteConnection(_dbPath);

            var formularioGlobo = new Frame
            {
                BackgroundColor = Color.White,
                CornerRadius = 20,
                Margin = new Thickness(20),
                Padding = new Thickness(20)
            };

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<registro>().OrderBy(x => x.nombre).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _identry = new Entry();
            _identry.Placeholder = "ID";
            _identry.IsVisible = false;
            stackLayout.Children.Add(_identry);

            _nombreEntry = new Entry();
            _nombreEntry.Keyboard = Keyboard.Text;
            _nombreEntry.Placeholder = "Nombre";
            _nombreEntry.TextChanged += _nombreEntry_TextChanged;
            stackLayout.Children.Add(_nombreEntry);

            _apellidoEntry = new Entry();
            _apellidoEntry.Keyboard = Keyboard.Text;
            _apellidoEntry.Placeholder = "Apellidos";
            _apellidoEntry.TextChanged += _apellidoEntry_TextChanged;
            stackLayout.Children.Add(_apellidoEntry);

            _correoEntry = new Entry();
            _correoEntry.Keyboard = Keyboard.Text;
            _correoEntry.Placeholder = "correo";
            stackLayout.Children.Add(_correoEntry);

            _telefonoEntry = new Entry();
            _telefonoEntry.Keyboard = Keyboard.Numeric;
            _telefonoEntry.Placeholder = "telefono";
            _telefonoEntry.TextChanged += _telefonoEntry_TextChanged;
            stackLayout.Children.Add(_telefonoEntry);

            _edadEntry = new Entry();
            _edadEntry.Keyboard = Keyboard.Text;
            _edadEntry.Placeholder = "edad";
            stackLayout.Children.Add(_edadEntry);

            _actualizarButton = new Button();
            _actualizarButton.Text = "Actualizar";
            _actualizarButton.Clicked += _actualizarButton_Clicked;
            stackLayout.Children.Add(_actualizarButton);

            formularioGlobo.Content = stackLayout;
            Content = stackLayout;
        }

        private async void _actualizarButton_Clicked(object sender, EventArgs e)
        {
            // Validar los campos antes de actualizar la base de datos
            if (string.IsNullOrWhiteSpace(_nombreEntry.Text) ||
                string.IsNullOrWhiteSpace(_apellidoEntry.Text) ||
                string.IsNullOrWhiteSpace(_correoEntry.Text) ||
                string.IsNullOrWhiteSpace(_telefonoEntry.Text) ||
                string.IsNullOrWhiteSpace(_edadEntry.Text))
            {
                await DisplayAlert("Error", "Todos los campos deben ser completados.", "OK");
                return; // Detener la ejecución si hay campos vacíos
            }

            if (!int.TryParse(_telefonoEntry.Text, out int telefono) || _telefonoEntry.Text.Length != 9)
            {
                await DisplayAlert("Error", "El número de teléfono debe ser un número de 9 dígitos.", "OK");
                return; // Detener la ejecución si el número de teléfono es inválido
            }

            if (!IsValidEmail(_correoEntry.Text))
            {
                await DisplayAlert("Error", "La dirección de correo electrónico no es válida.", "OK");
                return; // Detener la ejecución si el correo electrónico es inválido
            }

            if (!int.TryParse(_edadEntry.Text, out int edad) || _edadEntry.Text.Length != 2 || edad <= 18)
            {
                await DisplayAlert("Error", "La edad debe ser un número de 2 dígitos y mayor a 18.", "OK");
                return; // Detener la ejecución si la edad es inválida
            }

            // Todos los campos son válidos, procede a actualizar la base de datos
            var db = new SQLiteConnection(_dbPath);
            registro registro = new registro()
            {
                Id = Convert.ToInt32(_identry.Text),
                nombre = _nombreEntry.Text,
                Apellidos = _apellidoEntry.Text,
                correo = _correoEntry.Text,
                telefono = telefono,
                edad = edad,
            };
            db.Update(registro);
            await Navigation.PopAsync();
        }

        private bool IsValidEmail(string email)
        {
            // Aquí puedes implementar una validación más completa según tus necesidades
            // En este ejemplo, simplemente verifica si contiene "@" y ".com"
            return email.Contains("@") && email.Contains(".com");
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _registro = (registro)e.SelectedItem;
            _identry.Text = _registro.Id.ToString();
            _nombreEntry.Text = _registro.nombre;
            _apellidoEntry.Text = _registro.Apellidos;
            _correoEntry.Text = _registro.correo;
            _telefonoEntry.Text = _registro.telefono.ToString();
            _edadEntry.Text = _registro.edad.ToString();
        }

        private void _nombreEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !IsAlphaWithSpaces(e.NewTextValue))
            {
                _nombreEntry.Text = e.OldTextValue; // Restaurar el valor anterior si contiene caracteres no alfabéticos
            }
        }

        private void _apellidoEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !IsAlphaWithSpaces(e.NewTextValue))
            {
                _apellidoEntry.Text = e.OldTextValue; // Restaurar el valor anterior si contiene caracteres no alfabéticos
            }
        }

        private bool IsAlphaWithSpaces(string text)
        {
            return text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private void _telefonoEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !IsNumeric(e.NewTextValue))
            {
                _telefonoEntry.Text = e.OldTextValue; // Restaurar el valor anterior si no es numérico
            }

            if (_telefonoEntry.Text.Length > 9)
            {
                _telefonoEntry.Text = _telefonoEntry.Text.Substring(0, 9); // Limitar la longitud a 9 caracteres
            }
        }

        private bool IsNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}