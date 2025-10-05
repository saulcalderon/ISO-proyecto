using System;
using System.Collections.Generic;
using Desafio1App.Modelos;

namespace Desafio1App.Arbol
{
    public class ArbolClasificador
    {
        // Estructura: Género -> TipoSangre -> Presión -> Lista de Pacientes
        private Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> raiz;

        public ArbolClasificador()
        {
            raiz = new Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>>();
        }

        // Inserta un paciente en el árbol según su clasificación
        public void InsertarPaciente(Paciente paciente)
        {
            if (!raiz.ContainsKey(paciente.Genero))
            {
                raiz[paciente.Genero] = new Dictionary<string, Dictionary<string, List<Paciente>>>();
            }

            var nodoGenero = raiz[paciente.Genero];

            if (!nodoGenero.ContainsKey(paciente.TipoSangre))
            {
                nodoGenero[paciente.TipoSangre] = new Dictionary<string, List<Paciente>>();
            }

            var nodoSangre = nodoGenero[paciente.TipoSangre];

            if (!nodoSangre.ContainsKey(paciente.PresionArterial))
            {
                nodoSangre[paciente.PresionArterial] = new List<Paciente>();
            }

            nodoSangre[paciente.PresionArterial].Add(paciente);
        }

        // Devuelve toda la estructura del árbol para mostrarla
        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> ObtenerTodos()
        {
            return raiz;
        }

        // Propiedad pública para acceder a la raíz si es necesario (solo lectura)
        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> Raiz
        {
            get { return raiz; }
        }
    }
}
