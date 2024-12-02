﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Parcial_POO.DTOs
{
    public partial class EmpleadoDTO : ObservableObject
    {
        [ObservableProperty]
        public int idEmpleado;
        [ObservableProperty]
        public string nombreEmpleado;
        [ObservableProperty]
        public string cargo;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public DateTime fechaCertificacion; 
    }
}