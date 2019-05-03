using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PersonaCal
{
    public partial class BuildWindow : Window
    {
        public PersonasContainers db = new PersonasContainers();
        public List<Persona> masterList;
        public List<Persona> personaOneList;
        public List<Persona> teamList;

        public BuildWindow()
        {
            InitializeComponent();
            masterList = db.Personas.ToList();
            lbxTeamSelect.ItemsSource = masterList;
            teamList = new List<Persona>();
            cbxSearchType.ItemsSource = MainWindow.sortBy;
            cbxSearchType.SelectedIndex = 0;
        }

        #region NAV BUTTONS
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
        #endregion NAV BUTTONS

        #region ADD/REMOVE BUTTONS
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Check selection is not null
            if(lbxTeamSelect.SelectedItem != null)
            {
                //Max of 8 personas in a team
                if(teamList.Count < 8)
                {
                    teamList.Add(lbxTeamSelect.SelectedItem as Persona);
                    //displays selected personas in listbox
                    lbxTeam.ItemsSource = null;
                    lbxTeam.ItemsSource = teamList;
                }
                else
                {
                    MessageBox.Show("Max 8 Personas per team.\nPlease remove 1 or more to add this selection.",
                                    "Team Builder", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Remove a persona from team
            if(lbxTeam.SelectedItem != null)
            {
                teamList.Remove(lbxTeam.SelectedItem as Persona);
                lbxTeam.ItemsSource = null;
                lbxTeam.ItemsSource = teamList;
            }
        }
        #endregion NAV BUTTONS

        #region SAVE/LOAD BUTTONS
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(
                                                    teamList, Formatting.Indented, 
                                                    new JsonSerializerSettings()
                                                    {
                                                        //Since arcana is an object that Persona has
                                                        //ignore looping when writing. Does not affect loading
                                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                    });

            //Choose where to save team file, save as .json by default
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = ".json"
            };
            bool? result = sfd.ShowDialog();


            if (result == true)
            {
                //Save persona info to json file
                string filename = sfd.FileName;
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(json);
                }
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Choose file to load
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string json = sr.ReadToEnd();
                    teamList.Clear();
                    teamList = JsonConvert.DeserializeObject<List<Persona>>(json);
                }
            }
            //reset team listbox 
            lbxTeam.ItemsSource = null;
            lbxTeam.ItemsSource = teamList;
        }
        #endregion

        #region SEARCH LOGIC
        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Search etiher by name or ny arcana. Name by default
            if (cbxSearchType.SelectedIndex == 0)
            {
                lbxTeamSelect.ItemsSource = null;
                string search = tbxSearch.Text.ToLower();
                var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
                lbxTeamSelect.ItemsSource = filterList.ToList();
            }
            else if (cbxSearchType.SelectedIndex == 1)
            {
                lbxTeamSelect.ItemsSource = null;
                string search = tbxSearch.Text.ToLower();
                var filterList = masterList.Where(p => p.Arcana.ArcanaName.ToLower().Contains(search));
                lbxTeamSelect.ItemsSource = filterList.ToList();
            }
        }

        private void CbxSearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxSearch.Clear();

        }
        #endregion
    }
}
