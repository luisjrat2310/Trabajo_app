using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sirenita4_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lateral : MasterDetailPage
    {
        public lateral()
        {
            InitializeComponent();
            this.Master = new Principal();
            this.Detail = new NavigationPage(new cara());
        }
    }
}