using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using ParcialPOO.DataAccess;
using ParcialPOO.DTOs;
using ParcialPOO.Utilidades;
using ParcialPOO.Modelos;
using Microsoft.Maui.Controls.Compatibility.Platform;

namespace ParcialPOO.ViewModels
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
            EmpleadoDTo.FechaCertificacion=DateTime.Now;  //asigna fecha - pendiente por corregir

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
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContex.Empleados.FirstAsync(e => e.IdEmpleado == IdEmpleado);
                    EmpleadoDTo.IdEmpleado = encontrado.IdEmpleado;
                    EmpleadoDTo.NombreEmpleado = encontrado.NombreEmpleado;
                    EmpleadoDTo.Cargo = encontrado.Cargo;
                    EmpleadoDTo.Correo = encontrado.Correo;
                    EmpleadoDTo.FechaCertificacion = encontrado.FechaCertificacion;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });

                });
            }
        }
        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
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
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EmpleadoMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });


            });

        }
    }

}
