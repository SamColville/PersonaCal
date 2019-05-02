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
        string placeHoldText;
        string errorText;

        public CalculateWindow()
        {
            InitializeComponent();
            List<Persona> personaOneList = db.Personas.ToList();
            lbxPersonaOne.ItemsSource = personaOneList;
            lbxPersonaTwo.ItemsSource = personaOneList;
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
                Result = Fusion.CheckPersonaResult(p1, p2);
                tbkResult.Text = Result.ToString();
            }
            else
                tbkResult.Text = errorText;

        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            this.Close();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
