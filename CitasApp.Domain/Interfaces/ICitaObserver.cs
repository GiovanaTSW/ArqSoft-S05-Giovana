using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaObserver
    {
        void Notificar(Cita cita);
    }
}