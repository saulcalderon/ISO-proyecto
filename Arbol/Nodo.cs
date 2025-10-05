using System.Collections.Generic;
using Desafio1App.Modelos;

namespace Desafio1App.Arbol
{
    // Esta clase representa un nodo del árbol jerárquico de clasificación
    public class Nodo
    {
        public string Clave { get; set; } // Puede ser Género, Tipo de Sangre o Presión Arterial
        public List<Nodo> Hijos { get; set; } // Hijos de este nodo
        public List<Paciente> Pacientes { get; set; } // Pacientes almacenados en el nodo final

        // Constructor del nodo, recibe la clave identificadora
        public Nodo(string clave)
        {
            Clave = clave;
            Hijos = new List<Nodo>();
            Pacientes = new List<Paciente>();
        }

        // Busca si ya existe un hijo con esa clave
        public Nodo BuscarHijo(string clave)
        {
            return Hijos.Find(n => n.Clave == clave);
        }

        // Si el hijo no existe, lo crea y lo agrega
        public Nodo AgregarHijoSiNoExiste(string clave)
        {
            Nodo hijo = BuscarHijo(clave);
            if (hijo == null)
            {
                hijo = new Nodo(clave);
                Hijos.Add(hijo);
            }
            return hijo;
        }
    }
}
