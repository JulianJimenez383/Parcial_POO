using ParcialPOO.ViewModels;
using ParcialPOO.DataAccess;





namespace ParcialPOO
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
