using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace PersonaCal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public PersonasContainers db = new PersonasContainers();
        //string placeHoldText;
        //string errorText;
        public static int[,] ResultKeys = new int[22,22];
        List<List<int>> JsonResults = new List<List<int>>();
        List<int> TwoDResults = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            //var personaOneList = db.Personas.ToList();
            //lbxPersonaOne.ItemsSource = personaOneList;
            //lbxPersonaTwo.ItemsSource = personaOneList;
            //placeHoldText = "Press CALCULATE after selecting\n1. 1 Persona from column 1\n2. Persona from Column 2";
            //errorText = "Please select two Personas: \n1. 1 Persona from column 1\n2. Persona from Column 2";
            //tbkResult.Text = placeHoldText;
            StreamReader sr = new StreamReader("FusionResult.json");
            string json = sr.ReadToEnd();
            JsonResults = JsonConvert.DeserializeObject<List<List<int>>>(json);

            //ResultKeys = JsonResults.ToArray();
            for (int i = 0; i < 22; i++)
            {
                

            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            CalculateWindow calculateWindow = new CalculateWindow();
            calculateWindow.Owner = this;
            calculateWindow.ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        //{
        //    Persona p1 = lbxPersonaOne.SelectedItem as Persona;
        //    Persona p2 = lbxPersonaTwo.SelectedItem as Persona;
        //    FusionResult Fusion;
        //    Persona Result;
        //    if (lbxPersonaOne.SelectedItem != null && lbxPersonaTwo.SelectedItem != null)
        //    {
        //        Fusion = new FusionResult(p1, p2);
        //        Result = Fusion.CheckPersonaResult(p1, p2);
        //        tbkResult.Text = Result.ToString();
        //    }
        //    else
        //        tbkResult.Text = errorText;

        //}
    }
}
