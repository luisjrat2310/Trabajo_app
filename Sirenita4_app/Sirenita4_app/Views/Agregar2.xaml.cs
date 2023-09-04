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
    public partial class Agregar2 : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");
        public Agregar2()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
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
            await DisplayAlert(null, "Guardado Realizado", "¡Cliente guardado con éxito!", "OK");
            await Navigation.PopAsync();
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
                return age.Length == 2 && ageValue >= 18;
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