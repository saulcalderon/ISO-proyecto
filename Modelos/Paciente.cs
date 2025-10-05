using System;
using System.Collections.Generic;

namespace Desafio1App.Modelos
{
    // Esta clase representa a un paciente con sus características principales
    public class Paciente
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string TipoSangre { get; set; }
        public string PresionArterial { get; set; }
        public string Key { get; internal set; }
        public IEnumerable<object> Value { get; internal set; }

        // Constructor que se usa al momento de registrar un nuevo paciente
        public Paciente(string nombre, int edad, string genero, string tipoSangre, string presionArterial)
        {
            Nombre = nombre;
            Edad = edad;
            Genero = genero;
            TipoSangre = tipoSangre;
            PresionArterial = presionArterial;
        }

        // Este método sobrescribe el ToString para mostrar una descripción del paciente
        public override string ToString()
        {
            return $"{Nombre} - {Genero}, {Edad} años - Sangre: {TipoSangre}, Presión: {PresionArterial}";
        }
    }
}
