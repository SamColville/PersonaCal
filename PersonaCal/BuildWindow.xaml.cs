using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PersonaCal
{
    /// <summary>
    /// Interaction logic for BuildWindow.xaml
    /// </summary>
    public partial class BuildWindow : Window
    {
        public PersonasContainers db = new PersonasContainers();


        public BuildWindow()
        {
            InitializeComponent();
            List<Persona> personaOneList = db.Personas.ToList();
            lbxTeamSelect.ItemsSource = personaOneList;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            this.Close();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            CalculateWindow calc = new CalculateWindow();
            this.Close();
            calc.ShowDialog();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            SearchWindow search = new SearchWindow();
            this.Close();
            search.ShowDialog();
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e)
        {
            //No action
        }
    }
}
