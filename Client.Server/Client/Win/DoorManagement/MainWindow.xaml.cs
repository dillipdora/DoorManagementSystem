using DoorManagement.Common;
using DoorManagement.Common.Interfaces;
using System.ComponentModel.Composition;
using System.Windows;

namespace DoorManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Import(typeof(IDoorManagementViewModel))]
        public IDoorManagementViewModel DoorManagementViewModel;
        public MainWindow()
        {
            InitializeComponent();
            CompositionHelper.Container.SatisfyImportsOnce(this);
            DataContext = DoorManagementViewModel;
        }
    }
}
