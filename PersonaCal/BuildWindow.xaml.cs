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
    /// <summary>
    /// Interaction logic for BuildWindow.xaml
    /// </summary>
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(lbxTeamSelect.SelectedItem != null)
            {
                if(teamList.Count < 8)
                {
                    teamList.Add(lbxTeamSelect.SelectedItem as Persona);
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
            if(lbxTeam.SelectedItem != null)
            {
                teamList.Remove(lbxTeam.SelectedItem as Persona);
                lbxTeam.ItemsSource = null;
                lbxTeam.ItemsSource = teamList;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(teamList, Formatting.Indented, 
                                                        new JsonSerializerSettings()
                                                        {
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        });
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = ".json"
            };
            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                string filename = sfd.FileName;
                //json = JsonConvert.SerializeObject(movieList, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(json);
                }
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string json = sr.ReadToEnd();
                    teamList = JsonConvert.DeserializeObject<List<Persona>>(json);
                }
            }
            lbxTeam.ItemsSource = null;
            lbxTeam.ItemsSource = teamList;
        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbxTeamSelect.ItemsSource = null;
            string search = tbxSearch.Text.ToLower();            
            var filterList = masterList.Where(p => p.Name.ToLower().Contains(search));
            lbxTeamSelect.ItemsSource = filterList.ToList();
        }
    }
}
