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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonaCal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PersonasContainers db = new PersonasContainers();

        public MainWindow()
        {
            InitializeComponent();
            var personaOneList = db.Personas.ToList();
            lbxPersonaOne.ItemsSource = personaOneList;
            lbxPersonaTwo.ItemsSource = personaOneList;

        }
    }
}
