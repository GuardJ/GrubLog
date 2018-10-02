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
using TestMVVM.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace TestMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
        /*
        ListBoxItem storedItem;
        RadioButton checkedButton;
        int currentSender;
        bool flag;

        private void RadioButtonColumn_Click(object sender, RoutedEventArgs e)
        {
            currentSender = 0;

            updatingUIStuff(currentSender, sender);
        }

        private void ls1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSender = 1;
            

            updatingUIStuff(currentSender, sender);
        }      

        public void updatingUIStuff(int currentSender, object sender)
        {
            if(currentSender == 0)
            {
                ListBoxItem selectedItem = (ListBoxItem)ls1.ItemContainerGenerator.ContainerFromItem(((RadioButton)sender).DataContext);
                
                checkedButton = (RadioButton)sender;
                //ischecked always true after click event...
                if (selectedItem.IsSelected == true && checkedButton.IsChecked == true)
                {
                    checkedButton.IsChecked = false;
                    storedItem = selectedItem;
                }
                //if (selectedItem.IsSelected == true && checkedButton.IsChecked == false)
                //{
                //    checkedButton.IsChecked = true;
                //    storedItem = selectedItem;
                //    
                //}
                flag = true;
                selectedItem.IsSelected = true;
                flag = false;
            }
            if(currentSender == 1)
            {
                if (ls1.SelectedItem == storedItem)
                    return;
                if (ls1.SelectedItem != storedItem && checkedButton != null && !flag)
                    checkedButton.IsChecked = false;
            }
        }
        */
    }
}
