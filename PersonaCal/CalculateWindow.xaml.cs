using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CalculateWindow.xaml
    /// </summary>
    public partial class CalculateWindow : Window
    {
        public PersonasContainers db = new PersonasContainers();
        public List<Persona> masterList;
        string placeHoldText;
        string errorText;

        public CalculateWindow()
        {
            InitializeComponent();
            masterList = db.Personas.ToList();
            lbxPersonaOne.ItemsSource = masterList;
            lbxPersonaTwo.ItemsSource = masterList;
            placeHoldText = "Press CALCULATE after selecting\n1. 1 Persona from column 1\n2. Persona from Column 2";
            errorText = "Please select two Personas: \n1. 1 Persona from column 1\n2. Persona from Column 2";
            tbkResult.Text = placeHoldText;
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Persona p1 = lbxPersonaOne.SelectedItem as Persona;
            Persona p2 = lbxPersonaTwo.SelectedItem as Persona;
            FusionResult Fusion;
            Persona Result;
            if (lbxPersonaOne.SelectedItem != null && lbxPersonaTwo.SelectedItem != null)
            {
                Fusion = new FusionResult(p1, p2);
                Result = Fusion.CheckPersonaResult();
                tbkResult.Text = Result.ToString();
            }
            else //ADD SHOWDIALOG
                MessageBox.Show(errorText, "Fusion Calculator", MessageBoxButton.OK, MessageBoxImage.Exclamation);


        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            this.Close();
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
            MainWindow main = Owner as MainWindow;
            BuildWindow build = new BuildWindow();
            this.Close();
            build.ShowDialog();
        }

        private void TbxSearchOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbxPersonaOne.ItemsSource = null;
            string search = tbxSearchOne.Text.ToLower();
            var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
            lbxPersonaOne.ItemsSource = filterList.ToList();
        }

        private void TbxSearchTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbxPersonaTwo.ItemsSource = null;
            string search = tbxSearchTwo.Text.ToLower();
            var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
            lbxPersonaTwo.ItemsSource = filterList.ToList();
        }
    }
}
