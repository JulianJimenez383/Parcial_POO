using Parcial_POO.ViewModels;
using Parcial_POO.Views;

namespace Parcial_POO.Views;

public partial class EmpleadoPage : ContentPage
{
	public EmpleadoPage(EmpleadoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}