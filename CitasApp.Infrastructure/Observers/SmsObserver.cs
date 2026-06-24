using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class SmsObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[SMS] Recordatorio enviado al paciente {cita.PacienteId} - cita el {cita.Fecha} a las {cita.Hora}");
        }
    }
}