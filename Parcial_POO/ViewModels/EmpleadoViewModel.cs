using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Parcial_POO.DataAccess;
using Parcial_POO.DTOs;
using Parcial_POO.Utilidades;
using Parcial_POO.Modelos;
using Microsoft.Maui.Controls.Compatibility.Platform;

namespace Parcial_POO.ViewModels
{
    public partial class EmpleadoViewModel : ObservableObject, IQueryAttributable
    {

        private readonly EmpleadoDbContext _dbContex;

        [ObservableProperty]
        private EmpleadoDTO empleadoDTo = new EmpleadoDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdEmpleado;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public EmpleadoViewModel(EmpleadoDbContext context)
        {
            _dbContex = context;

        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdEmpleado = id;


            if (IdEmpleado == 0)
            {
                TituloPagina = "Nuevo Empleado";

            }
            else
            {
                TituloPagina = "Editar Empleado";
                loadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContex.Empleados.FirstAsync(e => e.IdEmpleado == IdEmpleado);
                    EmpleadoDTo.IdEmpleado = encontrado.IdEmpleado;
                    EmpleadoDTo.nombreEmpleado = encontrado.NombreEmpleado;
                    EmpleadoDTo.cargo = encontrado.Cargo;
                    EmpleadoDTo.correo = encontrado.Correo;
                    EmpleadoDTo.fechaCertificacion = encontrado.FechaCertificacion;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });

                });
            }
        }
        [RelayCommand]
        private async Task Guardar()
        {
            loadingEsVisible = true;
            EmpleadoMensaje mensaje = new EmpleadoMensaje();

            await Task.Run(async () =>
            {
                if (IdEmpleado == 0)
                {
                    var tbEmpleado = new Empleado
                    {
                        NombreEmpleado = EmpleadoDTo.NombreEmpleado,
                        Cargo = EmpleadoDTo.Cargo,
                        FechaCertificacion = EmpleadoDTo.FechaCertificacion,
                    };
                    _dbContex.Empleados.Add(tbEmpleado);
                    await _dbContex.SaveChangesAsync();

                    EmpleadoDTo.IdEmpleado = tbEmpleado.IdEmpleado;
                    mensaje = new EmpleadoMensaje()
                    {
                        EsCrear = true,
                        EmpleadoDTo = EmpleadoDTo,

                    };
                }

                else
                {
                    var encontrado = await _dbContex.Empleados.FirstAsync(e => e.IdEmpleado == IdEmpleado);
                    encontrado.NombreEmpleado = EmpleadoDTo.NombreEmpleado;
                    encontrado.Cargo = EmpleadoDTo.Cargo;
                    encontrado.Correo = EmpleadoDTo.Correo;
                    encontrado.FechaCertificacion = EmpleadoDTo.FechaCertificacion;

                    await _dbContex.SaveChangesAsync();

                    mensaje = new EmpleadoMensaje()
                    {
                        EsCrear = false,
                        EmpleadoDTo = EmpleadoDTo,

                    };
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    loadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EmpleadoMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });


            });

        }
    }

}
