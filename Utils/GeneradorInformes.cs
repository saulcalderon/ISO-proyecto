using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Desafio1App.Modelos;
using Desafio1App.Data;

namespace Desafio1App.Utils
{
    public enum TipoInforme
    {
        ListaPacientes,
        EstadisticasTipoSangre,
        EstadisticasPresion,
        EstadisticasGenero,
        ListaCitas,
        ResumenGeneral
    }

    public class GeneradorInformes
    {
        private readonly PacienteRepository pacienteRepository;
        private readonly CitaRepository citaRepository;

        public GeneradorInformes()
        {
            pacienteRepository = new PacienteRepository();
            citaRepository = new CitaRepository();
        }

        #region Generación de Excel (CSV)
        
        public string GenerarExcelPacientes(List<Paciente> pacientes, string rutaArchivo)
        {
            var sb = new StringBuilder();
            
            // Encabezados
            sb.AppendLine("ID,Nombre,Edad,Género,Tipo Sangre,Presión Arterial,Teléfono,Email,Dirección,Fecha Registro");
            
            // Datos
            foreach (var p in pacientes)
            {
                sb.AppendLine($"{p.Id},\"{p.Nombre}\",{p.Edad},{p.Genero},{p.TipoSangre},{p.PresionArterial}," +
                              $"\"{p.Telefono}\",\"{p.Email}\",\"{p.Direccion}\",{p.FechaRegistro:dd/MM/yyyy}");
            }
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        public string GenerarExcelCitas(List<Cita> citas, string rutaArchivo)
        {
            var sb = new StringBuilder();
            
            // Encabezados
            sb.AppendLine("ID,Paciente,Fecha y Hora,Motivo,Estado,Observaciones,Fecha Creación");
            
            // Datos
            foreach (var c in citas)
            {
                sb.AppendLine($"{c.Id},\"{c.NombrePaciente}\",{c.FechaHora:dd/MM/yyyy HH:mm}," +
                              $"\"{c.Motivo}\",{c.DescripcionEstado},\"{c.Observaciones}\",{c.FechaCreacion:dd/MM/yyyy}");
            }
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        public string GenerarExcelEstadisticas(Dictionary<string, int> estadisticas, string titulo, string rutaArchivo)
        {
            var sb = new StringBuilder();
            
            int total = estadisticas.Values.Sum();
            
            sb.AppendLine($"{titulo}");
            sb.AppendLine("Categoría,Cantidad,Porcentaje");
            
            foreach (var item in estadisticas.OrderByDescending(x => x.Value))
            {
                double porcentaje = total > 0 ? (double)item.Value / total * 100 : 0;
                sb.AppendLine($"{item.Key},{item.Value},{porcentaje:F2}%");
            }
            
            sb.AppendLine($"Total,{total},100%");
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        #endregion

        #region Generación de HTML (para imprimir como PDF)

        public string GenerarHTMLPacientes(List<Paciente> pacientes, string rutaArchivo)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine(ObtenerEncabezadoHTML("Lista de Pacientes"));
            
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><th>ID</th><th>Nombre</th><th>Edad</th><th>Género</th><th>Tipo Sangre</th><th>Presión</th><th>Teléfono</th><th>Email</th></tr>");
            
            foreach (var p in pacientes)
            {
                sb.AppendLine($"<tr><td>{p.Id}</td><td>{p.Nombre}</td><td>{p.Edad}</td><td>{p.Genero}</td>" +
                              $"<td>{p.TipoSangre}</td><td>{p.PresionArterial}</td><td>{p.Telefono}</td><td>{p.Email}</td></tr>");
            }
            
            sb.AppendLine("</table>");
            sb.AppendLine($"<p class='footer'>Total de pacientes: {pacientes.Count}</p>");
            sb.AppendLine(ObtenerPieHTML());
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        public string GenerarHTMLCitas(List<Cita> citas, string rutaArchivo)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine(ObtenerEncabezadoHTML("Lista de Citas Médicas"));
            
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><th>ID</th><th>Paciente</th><th>Fecha y Hora</th><th>Motivo</th><th>Estado</th><th>Observaciones</th></tr>");
            
            foreach (var c in citas)
            {
                string claseEstado = ObtenerClaseEstado(c.Estado);
                sb.AppendLine($"<tr><td>{c.Id}</td><td>{c.NombrePaciente}</td><td>{c.FechaFormateada}</td>" +
                              $"<td>{c.Motivo}</td><td class='{claseEstado}'>{c.DescripcionEstado}</td><td>{c.Observaciones}</td></tr>");
            }
            
            sb.AppendLine("</table>");
            sb.AppendLine($"<p class='footer'>Total de citas: {citas.Count}</p>");
            sb.AppendLine(ObtenerPieHTML());
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        public string GenerarHTMLEstadisticas(Dictionary<string, int> estadisticas, string titulo, string rutaArchivo)
        {
            var sb = new StringBuilder();
            int total = estadisticas.Values.Sum();
            
            sb.AppendLine(ObtenerEncabezadoHTML(titulo));
            
            sb.AppendLine("<div class='stats-container'>");
            sb.AppendLine("<table class='stats-table'>");
            sb.AppendLine("<tr><th>Categoría</th><th>Cantidad</th><th>Porcentaje</th><th>Gráfico</th></tr>");
            
            foreach (var item in estadisticas.OrderByDescending(x => x.Value))
            {
                double porcentaje = total > 0 ? (double)item.Value / total * 100 : 0;
                sb.AppendLine($"<tr><td>{item.Key}</td><td>{item.Value}</td><td>{porcentaje:F1}%</td>" +
                              $"<td><div class='bar' style='width:{porcentaje}%;'></div></td></tr>");
            }
            
            sb.AppendLine("</table>");
            sb.AppendLine($"<p class='total'>Total: {total}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine(ObtenerPieHTML());
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        public string GenerarHTMLResumenGeneral(string rutaArchivo)
        {
            var sb = new StringBuilder();
            var pacientes = pacienteRepository.ObtenerTodos();
            var citas = citaRepository.ObtenerTodas();
            
            sb.AppendLine(ObtenerEncabezadoHTML("Resumen General del Sistema"));
            
            // Resumen de pacientes
            sb.AppendLine("<div class='section'>");
            sb.AppendLine("<h2>Pacientes</h2>");
            sb.AppendLine($"<p><strong>Total de pacientes registrados:</strong> {pacientes.Count}</p>");
            
            if (pacientes.Count > 0)
            {
                var edadPromedio = pacientes.Average(p => p.Edad);
                sb.AppendLine($"<p><strong>Edad promedio:</strong> {edadPromedio:F1} años</p>");
                
                // Distribución por género
                var porGenero = pacientes.GroupBy(p => p.Genero).ToDictionary(g => g.Key, g => g.Count());
                sb.AppendLine("<h3>Distribución por Género</h3>");
                sb.AppendLine("<ul>");
                foreach (var item in porGenero)
                {
                    sb.AppendLine($"<li>{item.Key}: {item.Value} ({(double)item.Value / pacientes.Count * 100:F1}%)</li>");
                }
                sb.AppendLine("</ul>");
                
                // Distribución por tipo de sangre
                var porSangre = pacientes.GroupBy(p => p.TipoSangre).ToDictionary(g => g.Key, g => g.Count());
                sb.AppendLine("<h3>Distribución por Tipo de Sangre</h3>");
                sb.AppendLine("<ul>");
                foreach (var item in porSangre.OrderByDescending(x => x.Value))
                {
                    sb.AppendLine($"<li>{item.Key}: {item.Value} ({(double)item.Value / pacientes.Count * 100:F1}%)</li>");
                }
                sb.AppendLine("</ul>");
                
                // Distribución por presión
                var porPresion = pacientes.GroupBy(p => p.PresionArterial).ToDictionary(g => g.Key, g => g.Count());
                sb.AppendLine("<h3>Distribución por Presión Arterial</h3>");
                sb.AppendLine("<ul>");
                foreach (var item in porPresion.OrderByDescending(x => x.Value))
                {
                    sb.AppendLine($"<li>{item.Key}: {item.Value} ({(double)item.Value / pacientes.Count * 100:F1}%)</li>");
                }
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("</div>");
            
            // Resumen de citas
            sb.AppendLine("<div class='section'>");
            sb.AppendLine("<h2>Citas Médicas</h2>");
            sb.AppendLine($"<p><strong>Total de citas:</strong> {citas.Count}</p>");
            
            if (citas.Count > 0)
            {
                var citasPendientes = citas.Count(c => c.Estado == EstadoCita.Pendiente);
                var citasConfirmadas = citas.Count(c => c.Estado == EstadoCita.Confirmada);
                var citasCompletadas = citas.Count(c => c.Estado == EstadoCita.Completada);
                var citasCanceladas = citas.Count(c => c.Estado == EstadoCita.Cancelada);
                
                sb.AppendLine("<h3>Estado de Citas</h3>");
                sb.AppendLine("<ul>");
                sb.AppendLine($"<li>Pendientes: {citasPendientes}</li>");
                sb.AppendLine($"<li>Confirmadas: {citasConfirmadas}</li>");
                sb.AppendLine($"<li>Completadas: {citasCompletadas}</li>");
                sb.AppendLine($"<li>Canceladas: {citasCanceladas}</li>");
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("</div>");
            
            sb.AppendLine(ObtenerPieHTML());
            
            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
            return rutaArchivo;
        }

        #endregion

        #region Helpers HTML

        private string ObtenerEncabezadoHTML(string titulo)
        {
            return $@"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <title>{titulo} - SCS V1.0</title>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; margin: 40px; color: #333; }}
        h1 {{ color: #0066cc; border-bottom: 2px solid #0066cc; padding-bottom: 10px; }}
        h2 {{ color: #0066cc; margin-top: 30px; }}
        h3 {{ color: #444; }}
        table {{ border-collapse: collapse; width: 100%; margin: 20px 0; }}
        th, td {{ border: 1px solid #ddd; padding: 10px; text-align: left; }}
        th {{ background-color: #0066cc; color: white; }}
        tr:nth-child(even) {{ background-color: #f9f9f9; }}
        tr:hover {{ background-color: #f0f0f0; }}
        .header {{ text-align: center; margin-bottom: 30px; }}
        .footer {{ margin-top: 30px; font-style: italic; color: #666; }}
        .stats-container {{ margin: 20px 0; }}
        .stats-table {{ width: 100%; }}
        .bar {{ background-color: #0066cc; height: 20px; min-width: 5px; }}
        .total {{ font-weight: bold; font-size: 1.1em; }}
        .section {{ margin-bottom: 30px; padding: 20px; background-color: #f5f5f5; border-radius: 8px; }}
        .pendiente {{ background-color: #fff3cd; }}
        .confirmada {{ background-color: #d4edda; }}
        .completada {{ background-color: #d1ecf1; }}
        .cancelada {{ background-color: #f8d7da; }}
        ul {{ line-height: 1.8; }}
        @media print {{
            body {{ margin: 20px; }}
            .no-print {{ display: none; }}
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>{titulo}</h1>
        <p>Sistema de Clasificación de Sangre (SCS V1.0)</p>
        <p>Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}</p>
    </div>";
        }

        private string ObtenerPieHTML()
        {
            return @"
    <div class='footer'>
        <p>Este informe fue generado automáticamente por el Sistema de Clasificación de Sangre (SCS V1.0)</p>
        <p class='no-print'>Para guardar como PDF, use la función de impresión del navegador (Ctrl+P) y seleccione 'Guardar como PDF'</p>
    </div>
</body>
</html>";
        }

        private string ObtenerClaseEstado(EstadoCita estado)
        {
            switch (estado)
            {
                case EstadoCita.Pendiente: return "pendiente";
                case EstadoCita.Confirmada: return "confirmada";
                case EstadoCita.Completada: return "completada";
                case EstadoCita.Cancelada: return "cancelada";
                default: return "";
            }
        }

        #endregion

        #region Utilidades

        public void AbrirArchivo(string rutaArchivo)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo abrir el archivo: {ex.Message}");
            }
        }

        public string ObtenerRutaTemporal(string nombreArchivo)
        {
            string carpetaTemp = Path.Combine(Path.GetTempPath(), "SCS_Informes");
            if (!Directory.Exists(carpetaTemp))
            {
                Directory.CreateDirectory(carpetaTemp);
            }
            return Path.Combine(carpetaTemp, nombreArchivo);
        }

        #endregion
    }
}

