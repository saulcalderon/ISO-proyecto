using System;
using System.Collections.Generic;
using System.Linq;
using Desafio1App.Modelos;

namespace Desafio1App.Arbol
{
    public class ArbolClasificador
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> raiz;
        private List<Paciente> listaPacientes;

        public ArbolClasificador()
        {
            raiz = new Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>>();
            listaPacientes = new List<Paciente>();
        }

        public void InsertarPaciente(Paciente paciente)
        {
            listaPacientes.Add(paciente);
            AgregarAlArbol(paciente);
        }

        private void AgregarAlArbol(Paciente paciente)
        {
            if (!raiz.ContainsKey(paciente.Genero))
                raiz[paciente.Genero] = new Dictionary<string, Dictionary<string, List<Paciente>>>();

            var nodoGenero = raiz[paciente.Genero];

            if (!nodoGenero.ContainsKey(paciente.TipoSangre))
                nodoGenero[paciente.TipoSangre] = new Dictionary<string, List<Paciente>>();

            var nodoSangre = nodoGenero[paciente.TipoSangre];

            if (!nodoSangre.ContainsKey(paciente.PresionArterial))
                nodoSangre[paciente.PresionArterial] = new List<Paciente>();

            nodoSangre[paciente.PresionArterial].Add(paciente);
        }

        public void EditarPaciente(Paciente pacienteActualizado)
        {
            var pacienteOriginal = listaPacientes.FirstOrDefault(p => p.Id == pacienteActualizado.Id);
            if (pacienteOriginal == null) return;

            // Remover del árbol en la posición anterior
            RemoverDelArbol(pacienteOriginal);

            // Actualizar datos
            pacienteOriginal.Nombre = pacienteActualizado.Nombre;
            pacienteOriginal.Edad = pacienteActualizado.Edad;
            pacienteOriginal.Genero = pacienteActualizado.Genero;
            pacienteOriginal.TipoSangre = pacienteActualizado.TipoSangre;
            pacienteOriginal.PresionArterial = pacienteActualizado.PresionArterial;

            // Re-insertar en el árbol
            AgregarAlArbol(pacienteOriginal);
        }

        public void EliminarPaciente(int idPaciente)
        {
            var paciente = listaPacientes.FirstOrDefault(p => p.Id == idPaciente);
            if (paciente == null) return;

            RemoverDelArbol(paciente);
            listaPacientes.Remove(paciente);
        }

        private void RemoverDelArbol(Paciente paciente)
        {
            if (!raiz.ContainsKey(paciente.Genero)) return;
            var nodoGenero = raiz[paciente.Genero];

            if (!nodoGenero.ContainsKey(paciente.TipoSangre)) return;
            var nodoSangre = nodoGenero[paciente.TipoSangre];

            if (!nodoSangre.ContainsKey(paciente.PresionArterial)) return;
            var listaPacientes = nodoSangre[paciente.PresionArterial];

            listaPacientes.Remove(paciente);

            // Limpiar nodos vacíos
            if (listaPacientes.Count == 0)
            {
                nodoSangre.Remove(paciente.PresionArterial);
                
                if (nodoSangre.Count == 0)
                {
                    nodoGenero.Remove(paciente.TipoSangre);
                    
                    if (nodoGenero.Count == 0)
                    {
                        raiz.Remove(paciente.Genero);
                    }
                }
            }
        }

        public List<Paciente> ObtenerListaPacientes()
        {
            return new List<Paciente>(listaPacientes);
        }

        public List<Paciente> BuscarPacientes(string genero = null, string tipoSangre = null, string presion = null, string nombre = null)
        {
            var resultado = listaPacientes.AsEnumerable();

            if (!string.IsNullOrEmpty(genero) && genero != "Todos")
                resultado = resultado.Where(p => p.Genero == genero);

            if (!string.IsNullOrEmpty(tipoSangre) && tipoSangre != "Todos")
                resultado = resultado.Where(p => p.TipoSangre == tipoSangre);

            if (!string.IsNullOrEmpty(presion) && presion != "Todos")
                resultado = resultado.Where(p => p.PresionArterial == presion);

            if (!string.IsNullOrEmpty(nombre))
                resultado = resultado.Where(p => p.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0);

            return resultado.ToList();
        }

        public Paciente ObtenerPacientePorId(int id)
        {
            return listaPacientes.FirstOrDefault(p => p.Id == id);
        }

        public int TotalPacientes => listaPacientes.Count;

        public Dictionary<string, int> ObtenerEstadisticasPorTipoSangre()
        {
            return listaPacientes.GroupBy(p => p.TipoSangre)
                                 .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> ObtenerEstadisticasPorGenero()
        {
            return listaPacientes.GroupBy(p => p.Genero)
                                 .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> ObtenerEstadisticasPorPresion()
        {
            return listaPacientes.GroupBy(p => p.PresionArterial)
                                 .ToDictionary(g => g.Key, g => g.Count());
        }

        public double ObtenerEdadPromedio()
        {
            return listaPacientes.Count > 0 ? listaPacientes.Average(p => p.Edad) : 0;
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> ObtenerTodos()
        {
            return raiz;
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> Raiz => raiz;
    }
}
