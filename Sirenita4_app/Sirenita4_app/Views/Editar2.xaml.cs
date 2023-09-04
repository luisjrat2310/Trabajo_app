using Sirenita4_app.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sirenita4_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editar2 : ContentPage
    {
        private Entry _identry;
        private registro _registro = new registro();
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");

        public Editar2()
        {
            InitializeComponent();
            this.Title = "Editar";

            var db = new SQLiteConnection(_dbPath);
            _listView.ItemsSource = db.Table<registro>().OrderBy(x => x.nombre).ToList();
        }

        private async void _actualizarButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nombreEntry.Text) ||
                string.IsNullOrWhiteSpace(_apellidoEntry.Text) ||
                string.IsNullOrWhiteSpace(_correoEntry.Text) ||
                string.IsNullOrWhiteSpace(_telefonoEntry.Text) ||
                string.IsNullOrWhiteSpace(_edadEntry.Text))
            {
                await DisplayAlert("Campos Vacíos", "Por favor, complete todos los campos.", "OK");
                return;
            }

            if (!IsValidPhoneNumber(_telefonoEntry.Text))
            {
                await DisplayAlert("Número de Teléfono Inválido", "El número de teléfono debe tener 9 dígitos.", "OK");
                return;
            }

            if (!IsValidEmail(_correoEntry.Text))
            {
                await DisplayAlert("Correo Inválido", "El correo debe ser válido (contener '@' y '.com').", "OK");
                return;
            }

            if (!IsValidAge(_edadEntry.Text))
            {
                await DisplayAlert("Edad Inválida", "La edad debe tener al menos dos dígitos y ser mayor de 18 años.", "OK");
                return;
            }

            var db = new SQLiteConnection(_dbPath);
            registro registro = new registro()
            {
                Id = Convert.ToInt32(_identry.Text),
                nombre = _nombreEntry.Text,
                Apellidos = _apellidoEntry.Text,
                correo = _correoEntry.Text,
                telefono = Convert.ToInt32(_telefonoEntry.Text),
                edad = Convert.ToInt32(_edadEntry.Text),
            };
            db.Update(registro);
            await Navigation.PopAsync();
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

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 9 && int.TryParse(phoneNumber, out _);
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".com");
        }

        private bool IsValidAge(string age)
        {
            if (int.TryParse(age, out int ageValue))
            {
                return age.Length >= 2 && ageValue > 18;
            }
            return false;
        }

        private void NombreEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string text = e.NewTextValue;

            if (!string.IsNullOrWhiteSpace(text))
            {
                entry.Text = new string(text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            }
        }

        private void ApellidosEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string text = e.NewTextValue;

            if (!string.IsNullOrWhiteSpace(text))
            {
                entry.Text = new string(text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            }
        }

        private void TelefonoEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string text = e.NewTextValue;

            if (!string.IsNullOrWhiteSpace(text) && (!text.All(char.IsDigit) || text.Length > 9))
            {
                entry.Text = new string(text.Where(char.IsDigit).Take(9).ToArray());
            }
        }

        private void EdadEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string text = e.NewTextValue;

            if (!string.IsNullOrWhiteSpace(text) && (!text.All(char.IsDigit) || text.Length < 2 || int.Parse(text) <= 18))
            {
                entry.Text = new string(text.Where(char.IsDigit).ToArray());
            }
        }
    }
}