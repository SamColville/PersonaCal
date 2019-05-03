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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        List<Persona[]> Resutls = new List<Persona[]>();
        public PersonasContainers db = new PersonasContainers();
        List<Persona> masterList;

        public SearchWindow()
        {
            InitializeComponent();
            masterList = db.Personas.ToList();
            lbxChild.ItemsSource = masterList;
            cbxSearchType.ItemsSource = MainWindow.sortBy;
            cbxSearchType.SelectedIndex = 0;
        }

        #region NAV BUTTONS
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            SearchWindow search = new SearchWindow();
            this.Close();
            search.ShowDialog();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            CalculateWindow calc = new CalculateWindow();
            this.Close();
            calc.ShowDialog();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            this.Close();
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            BuildWindow build = new BuildWindow();
            this.Close();
            build.ShowDialog();
        }
        #endregion NAV BUTTONS

        #region FIND LOGIC
        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            lbxResults.ItemsSource = null;

            if (lbxChild.SelectedItem != null)
            {
                Persona child = lbxChild.SelectedItem as Persona;
                SearchResult result = new SearchResult(child);
                result.CheckPossible();
                result.VerifyResult();
                lbxResults.ItemsSource = result.ResultList;

            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            //Clears results for new selection
            lbxResults.ItemsSource = null;
        }
        #endregion

        #region SEARCH LOGIC
        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Search by name or arcana, name by default
            if(cbxSearchType.SelectedIndex == 0)
            {
                lbxChild.ItemsSource = null;
                string search = tbxSearch.Text.ToLower();
                var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
                lbxChild.ItemsSource = filterList.ToList();
            }
            else if(cbxSearchType.SelectedIndex == 1)
            {
                lbxChild.ItemsSource = null;
                string search = tbxSearch.Text.ToLower();
                var filterList = masterList.Where(p => p.Arcana.ArcanaName.ToLower().Contains(search));
                lbxChild.ItemsSource = filterList.ToList();

            }
        }

        private void CbxSearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxChild.ItemsSource = null;
            lbxChild.ItemsSource = masterList;
            tbxSearch.Clear();
        }
        #endregion
    }
}
