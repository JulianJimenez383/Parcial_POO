using Parcial_POO.ViewModels;





namespace Parcial_POO
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


    }

}
