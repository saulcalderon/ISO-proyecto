using System;

namespace Desafio1App.Modelos
{
    public class Paciente
    {
        private static int contadorId = 1;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string TipoSangre { get; set; }
        public string PresionArterial { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Paciente(string nombre, int edad, string genero, string tipoSangre, string presionArterial)
        {
            Id = contadorId++;
            Nombre = nombre;
            Edad = edad;
            Genero = genero;
            TipoSangre = tipoSangre;
            PresionArterial = presionArterial;
            FechaRegistro = DateTime.Now;
        }

        public Paciente() { }

        public Paciente Clonar()
        {
            return new Paciente
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Edad = this.Edad,
                Genero = this.Genero,
                TipoSangre = this.TipoSangre,
                PresionArterial = this.PresionArterial,
                FechaRegistro = this.FechaRegistro
            };
        }

        public override string ToString()
        {
            return $"{Nombre} - {Genero}, {Edad} años - Sangre: {TipoSangre}, Presión: {PresionArterial}";
        }

        public string DescripcionCorta => $"{Nombre} ({Edad} años)";
    }
}
