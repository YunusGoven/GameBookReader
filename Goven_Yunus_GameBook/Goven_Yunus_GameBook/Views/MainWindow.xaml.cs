using System.Windows;
using GameBookViewModel.ViewModels;


namespace GameBookView.Views
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((StoryViewModel)DataContext).OpenFileDialog = new FileRessourceChooser();
        }
    }
}
