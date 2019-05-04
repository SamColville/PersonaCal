using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace PersonaCal
{
    /// <summary>
    /// Interaction logic for TeamView.xaml
    /// </summary>
    public partial class TeamView : Window
    {
        public static Persona selectedPersona = new Persona();
        public TeamView()
        {
            InitializeComponent();
            if(MainWindow.teamList.Count > 0)
                lbxTeamMembers.ItemsSource = MainWindow.teamList;
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow main = Owner as MainWindow;
            this.Close();
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
                    MainWindow.teamList.Clear();
                    MainWindow.teamList = JsonConvert.DeserializeObject<List<Persona>>(json);
                }
            }
            //reset team listbox 
            lbxTeamMembers.ItemsSource = null;
            lbxTeamMembers.ItemsSource = MainWindow.teamList;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(lbxTeamMembers.SelectedItem != null)
            {
                selectedPersona = lbxTeamMembers.SelectedItem as Persona;
                Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please slecet a persona.\npress OK to retuen.\nPress Cancel to go back to previous window.", "TeamView", 
                    MessageBoxButton.OKCancel);

                switch (result)
                {
                    case MessageBoxResult.OK:
                        
                        break;
                    case MessageBoxResult.Cancel:
                        Close();
                        break;
                }
            }
        }
    }
}
