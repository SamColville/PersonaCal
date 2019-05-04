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
        public Persona Result;
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
            
            //Populate combo boxes with search type
            cbxSearchOne.ItemsSource = MainWindow.sortBy;
            cbxSearchTwo.ItemsSource = MainWindow.sortBy;
            cbxSearchOne.SelectedIndex = 0;
            cbxSearchTwo.SelectedIndex = 0;

        }

        #region CALCULATE LOGIC
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Persona p1 = lbxPersonaOne.SelectedItem as Persona;
            Persona p2 = lbxPersonaTwo.SelectedItem as Persona;
            FusionResult Fusion;
            //check valid selections from both listboxes
            if (lbxPersonaOne.SelectedItem != null && lbxPersonaTwo.SelectedItem != null)
            {
                //check fusion results
                Fusion = new FusionResult(p1, p2);
                Result = Fusion.CheckPersonaResult();
                tbkResult.Text = Result.ToString();

                //Possible single line alternative
                //tbkResult.Text = new FusionResult(p1, p2).CheckPersonaResult().ToString();
            }
            else 
                //error handling: Displays messagebox with instructions
                MessageBox.Show(errorText, "Fusion Calculator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

        #region NAV BUTTONS
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
        #endregion

        #region SEARCHBOX LOGIC
        private void TbxSearchOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Search
            //Checks for search property: Name or Arcana
            if (cbxSearchOne.SelectedIndex == 0)
            {
                //If Name, sorts masterList by names that contain text from tbxSearchOne
                lbxPersonaOne.ItemsSource = null;
                string search = tbxSearchOne.Text.ToLower();
                var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
                lbxPersonaOne.ItemsSource = filterList.ToList();
            }
            else if (cbxSearchOne.SelectedIndex == 1)
            {
                //Same as above, but search by Arcana
                lbxPersonaOne.ItemsSource = null;
                string search = tbxSearchOne.Text.ToLower();
                var filterList = masterList.Where(p => p.Arcana.ArcanaName.ToLower().Contains(search));
                lbxPersonaOne.ItemsSource = filterList.ToList();
            }
        }

        private void TbxSearchTwo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Same as TbxSearchOne_TextChanged
            if (cbxSearchTwo.SelectedIndex == 0)
            {
                lbxPersonaTwo.ItemsSource = null;
                string search = tbxSearchTwo.Text.ToLower();
                var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
                lbxPersonaTwo.ItemsSource = filterList.ToList();
            }
            else if (cbxSearchTwo.SelectedIndex == 1)
            {
                lbxPersonaTwo.ItemsSource = null;
                string search = tbxSearchTwo.Text.ToLower();
                var filterList = masterList.Where(p => p.Arcana.ArcanaName.ToLower().Contains(search));
                lbxPersonaTwo.ItemsSource = filterList.ToList();

            }
        }

        //Clears text boxes if combo box selection changed
        //and hence resets listbox itemssource
        private void CbxSearchOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxSearchOne.Clear();
        }

        private void CbxSearchTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxSearchTwo.Clear();
        }
        #endregion

        private void BtnAddToTeam_Click(object sender, RoutedEventArgs e)
        {
            if (tbkResult.Text.Contains(placeHoldText) || tbkResult.Text.Contains(errorText))
                MessageBox.Show("Please make a fusion to add to team.", "Calculator", MessageBoxButton.OK);
            else if(tbkResult.Text.Contains("Fusion not possible"))
            {
                MessageBox.Show("This fusion is not possible.\nPlease try a new fusion.", "Calculator", MessageBoxButton.OK);
            }
            else
            {
                if(Result != null)
                    MainWindow.teamList.Add(Result);
                else
                    MessageBox.Show("Please make a fusion to add to team.", "Calculator", MessageBoxButton.OK);
            }
        }
    }
}
