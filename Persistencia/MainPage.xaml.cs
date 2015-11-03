using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Persistencia
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localData;
        const string KEY_TEXT = "text";

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;


            localData = ApplicationData.Current.LocalSettings;

            App app = (App) App.Current;
            
            PlanetaDao planetaDao = new PlanetaDao(app.Con);


        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(localData.Values.ContainsKey(KEY_TEXT)){
                txtSaved.Text = localData.Values[KEY_TEXT] as string;
            }
        }

        private void saveTxt(object sender, RoutedEventArgs e)
        {
            localData.Values[KEY_TEXT] = txt.Text;
            txtSaved.Text = txt.Text;
        }
    }
}
