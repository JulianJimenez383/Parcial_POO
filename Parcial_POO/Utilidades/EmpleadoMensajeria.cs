using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Parcial_POO.Utilidades
{
    public class EmpleadoMensajeria : ValueChangedMessage<EmpleadoMensaje>
    {
        public EmpleadoMensajeria(EmpleadoMensaje value):base(value)
        {

        }



    }
}
