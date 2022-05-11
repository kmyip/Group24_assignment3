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

namespace Group24_assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private void ShowAllGroups_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have clicked Show all groups");
        }

        private void ShowAllStudents_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have clicked Show all students");
        }

        private void ShowAllClasses_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have clicked Show all classes");
        }

        private void ShowAllMeetings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have clicked Show all meetings");
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Searching by: {this.TextBox.Text}");
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
