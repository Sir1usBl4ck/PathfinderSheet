using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using UserInterface.ViewModels;


namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseViewModel _viewModel;
        
        public MainWindow()
        {         
            InitializeComponent();
            //_viewModel = new ViewModel();


            //DataContext = _viewModel.NewCharacter;
           

            //SaveButton.Click += (sender, e) =>
            //{
            //    _viewModel.Save(_viewModel);
                
            //};
               

            //LoadButton.Click += (sender, e) =>
            //{
            //    ViewModel.Load(CurrentCharacter);
            //};

        }



        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonCombat_OnClick(object sender, RoutedEventArgs e)
        {
            CombatWindow combatWindow = new CombatWindow();
            combatWindow.Owner = this;
            combatWindow.Show();
        }
    }
}
