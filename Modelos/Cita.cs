using System;

namespace Desafio1App.Modelos
{
    public enum EstadoCita
    {
        Pendiente = 0,
        Confirmada = 1,
        Completada = 2,
        Cancelada = 3
    }

    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
        public EstadoCita Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedad de navegación
        public string NombrePaciente { get; set; }

        public Cita()
        {
            Estado = EstadoCita.Pendiente;
            Observaciones = "";
            FechaCreacion = DateTime.Now;
        }

        public Cita(int pacienteId, DateTime fechaHora, string motivo, string observaciones = "")
        {
            PacienteId = pacienteId;
            FechaHora = fechaHora;
            Motivo = motivo;
            Observaciones = observaciones ?? "";
            Estado = EstadoCita.Pendiente;
            FechaCreacion = DateTime.Now;
        }

        public string DescripcionEstado
        {
            get
            {
                switch (Estado)
                {
                    case EstadoCita.Pendiente: return "Pendiente";
                    case EstadoCita.Confirmada: return "Confirmada";
                    case EstadoCita.Completada: return "Completada";
                    case EstadoCita.Cancelada: return "Cancelada";
                    default: return "Desconocido";
                }
            }
        }

        public string FechaFormateada => FechaHora.ToString("dd/MM/yyyy HH:mm");

        public override string ToString()
        {
            return $"{FechaFormateada} - {Motivo} ({DescripcionEstado})";
        }

        public bool EsFutura => FechaHora > DateTime.Now;
        
        public bool EsPasada => FechaHora < DateTime.Now;
    }
}

