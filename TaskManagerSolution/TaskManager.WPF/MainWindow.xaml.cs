using System.Windows;
using System.Windows.Controls;

namespace TaskManager.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
        }
    }
}