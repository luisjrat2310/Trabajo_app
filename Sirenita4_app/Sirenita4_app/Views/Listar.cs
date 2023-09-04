using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Xamarin.Forms;
using SQLite;
using Sirenita4_app.Models;

namespace Sirenita4_app.Views
{
    public class Listar : ContentPage
    {
        private ListView _listView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "sirena.db3");
        public Listar()
        {
            this.Title = "Registos";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<registro>().OrderBy(x => x.nombre).ToList();
            stackLayout.Children.Add(_listView);

            Content = stackLayout;

        }
    }
}