using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ParcialPOO.Utilidades
{
    public class EmpleadoMensajeria : ValueChangedMessage<EmpleadoMensaje>
    {
        public EmpleadoMensajeria(EmpleadoMensaje value):base(value)
        {

        }



    }
}
