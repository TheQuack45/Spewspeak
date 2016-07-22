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

namespace SpewspeakActualReal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PLACEHOLDER_TEXT = "Enter your text here.";
        private const string BLANK_TEXT = "";

        public MainWindow()
        {
            InitializeComponent();
            this.inOutTextBox.Text = PLACEHOLDER_TEXT;
        }

        private void inOutTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.inOutTextBox.Text == BLANK_TEXT)
            {
                // If the text within the entry textbox is blank, set the text to the placeholder text.
                this.inOutTextBox.Text = PLACEHOLDER_TEXT;
            }
        }

        private void inOutTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.inOutTextBox.Text == PLACEHOLDER_TEXT)
            {
                // If the text within the entry textbox is the placeholder text, clear the textbox for entry.
                this.inOutTextBox.Text = BLANK_TEXT;
            }
        }
    }
}
