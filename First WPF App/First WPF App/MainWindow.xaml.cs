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

namespace First_WPF_App
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

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The description is: " + DescriptionText.Text);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            DescriptionText.Text = "";

            WeldCheckbox.IsChecked =
            AssemblyCheckbox.IsChecked =
            PlasmaCheckbox.IsChecked =
            LaserCheckbox.IsChecked =
            PurchaseCheckbox.IsChecked =
            LatheCheckbox.IsChecked =
            DrillCheckbox.IsChecked =
            FoldCheckbox.IsChecked =
            RollCheckbox.IsChecked =
            SawCheckbox.IsChecked = false;

            FinishDropdown.SelectedIndex = 0;
            PurchaseDropdown.SelectedIndex = 0;

            SupplierNameText.Text = "";
            SupplierCodeText.Text = "";
            LengthText.Text = "";
            NoteText.Text = "";
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            LengthText.Text += ((CheckBox)sender).Content;
        }

        private void FinishDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NoteText == null)
                return;

            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;
            NoteText.Text = (string)value.Content;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FinishDropdown_SelectionChanged(FinishDropdown, null);
        }

        private void SupplierNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            MassText.Text = ((TextBox)sender).Text;
        }
    }
}
