using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using ParcialPOO.DataAccess;
using ParcialPOO.DTOs;
using ParcialPOO.Utilidades;
using ParcialPOO.Modelos;
using Microsoft.Maui.Controls.Compatibility.Platform;
using System.Collections.ObjectModel;
using ParcialPOO.Views;

namespace ParcialPOO.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly EmpleadoDbContext _dbContex;
        [ObservableProperty]
        private ObservableCollection<EmpleadoDTO> listaEmpleado = new ObservableCollection<EmpleadoDTO>();

        public MainViewModel(EmpleadoDbContext context)
        {
            _dbContex = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<EmpleadoMensajeria>(this, (r, m) =>
            {
                EmpleadoMensajeRecibido(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContex.Empleados.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaEmpleado.Add(new EmpleadoDTO
                    {
                        IdEmpleado=item.IdEmpleado,
                        NombreEmpleado=item.NombreEmpleado,
                        Cargo=item.Cargo,
                        Correo=item.Correo,
                        FechaCertificacion=item.FechaCertificacion,
                    });
                }

            }
        }

        private void EmpleadoMensajeRecibido(EmpleadoMensaje empleadoMensaje)
        {
            var empleadoDTO = empleadoMensaje.EmpleadoDTo;

            if (empleadoMensaje.EsCrear)
            {
                ListaEmpleado.Add(empleadoDTO);
            }
            else
            {
                var encontrado = ListaEmpleado
                    .First(e => e.IdEmpleado == empleadoDTO.IdEmpleado);

                encontrado.NombreEmpleado = empleadoDTO.NombreEmpleado;
                encontrado.Cargo = empleadoDTO.Cargo;
                encontrado.Correo = empleadoDTO.Correo;
                encontrado.FechaCertificacion = empleadoDTO.FechaCertificacion;
            }
        }


        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(EmpleadoPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }


        [RelayCommand]
        private async Task Editar(EmpleadoDTO empleadoDTo)
        {
            var uri = $"{nameof(EmpleadoPage)}?id={empleadoDTo.IdEmpleado}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(EmpleadoDTO empleadoDTo)
        {
            bool aswer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el empleado?", "SI", "NO");

            if(aswer)
            {
                var encontrado = await _dbContex.Empleados
                    .FirstAsync(e => e.IdEmpleado == empleadoDTo.IdEmpleado);
                _dbContex.Empleados.Remove(encontrado);
                await _dbContex.SaveChangesAsync();
                ListaEmpleado.Remove(empleadoDTo);
            }
        }



    }
}

