using ParcialPOO.ViewModels;
using ParcialPOO.Views;

namespace ParcialPOO.Views;

public partial class EmpleadoPage : ContentPage
{
	public EmpleadoPage(EmpleadoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}