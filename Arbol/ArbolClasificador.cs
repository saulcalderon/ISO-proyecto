using System;
using System.Collections.Generic;
using System.Linq;
using Desafio1App.Modelos;
using Desafio1App.Data;

namespace Desafio1App.Arbol
{
    public class ArbolClasificador
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> raiz;
        private List<Paciente> listaPacientes;
        private PacienteRepository repository;

        public ArbolClasificador()
        {
            raiz = new Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>>();
            listaPacientes = new List<Paciente>();
            repository = new PacienteRepository();
            CargarDesdeBaseDatos();
        }

        private void CargarDesdeBaseDatos()
        {
            listaPacientes = repository.ObtenerTodos();
            ReconstruirArbol();
        }

        private void ReconstruirArbol()
        {
            raiz.Clear();
            foreach (var paciente in listaPacientes)
            {
                AgregarAlArbol(paciente);
            }
        }

        public void InsertarPaciente(Paciente paciente)
        {
            paciente.Id = repository.Insertar(paciente);
            paciente.FechaRegistro = DateTime.Now;
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

            RemoverDelArbol(pacienteOriginal);

            pacienteOriginal.Nombre = pacienteActualizado.Nombre;
            pacienteOriginal.Edad = pacienteActualizado.Edad;
            pacienteOriginal.Genero = pacienteActualizado.Genero;
            pacienteOriginal.TipoSangre = pacienteActualizado.TipoSangre;
            pacienteOriginal.PresionArterial = pacienteActualizado.PresionArterial;

            repository.Actualizar(pacienteOriginal);
            AgregarAlArbol(pacienteOriginal);
        }

        public void EliminarPaciente(int idPaciente)
        {
            var paciente = listaPacientes.FirstOrDefault(p => p.Id == idPaciente);
            if (paciente == null) return;

            RemoverDelArbol(paciente);
            listaPacientes.Remove(paciente);
            repository.Eliminar(idPaciente);
        }

        private void RemoverDelArbol(Paciente paciente)
        {
            if (!raiz.ContainsKey(paciente.Genero)) return;
            var nodoGenero = raiz[paciente.Genero];

            if (!nodoGenero.ContainsKey(paciente.TipoSangre)) return;
            var nodoSangre = nodoGenero[paciente.TipoSangre];

            if (!nodoSangre.ContainsKey(paciente.PresionArterial)) return;
            var listaPacientesNodo = nodoSangre[paciente.PresionArterial];

            listaPacientesNodo.Remove(paciente);

            if (listaPacientesNodo.Count == 0)
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

        public List<Paciente> ObtenerListaPacientes() => new List<Paciente>(listaPacientes);

        public List<Paciente> BuscarPacientes(string genero = null, string tipoSangre = null, string presion = null, string nombre = null)
        {
            return repository.Buscar(genero, tipoSangre, presion, nombre);
        }

        public Paciente ObtenerPacientePorId(int id) => listaPacientes.FirstOrDefault(p => p.Id == id);

        public int TotalPacientes => listaPacientes.Count;

        public Dictionary<string, int> ObtenerEstadisticasPorTipoSangre() => repository.ObtenerEstadisticasPor("TipoSangre");

        public Dictionary<string, int> ObtenerEstadisticasPorGenero() => repository.ObtenerEstadisticasPor("Genero");

        public Dictionary<string, int> ObtenerEstadisticasPorPresion() => repository.ObtenerEstadisticasPor("PresionArterial");

        public double ObtenerEdadPromedio() => repository.ObtenerEdadPromedio();

        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> ObtenerTodos() => raiz;
        public Dictionary<string, Dictionary<string, Dictionary<string, List<Paciente>>>> Raiz => raiz;
    }
}
