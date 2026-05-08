using System;
using System.Collections.Generic;
using System.Linq;
using SistemaEntregas.Auth;
using SistemaEntregas.Tareas;

namespace SistemaEntregas.Bitacora
{
    public class Bitacora
    {
        private readonly List<RegistroEntrega> entradas = new();

        public void Registrar(Entrega entrega)
        {
            var registro = new RegistroEntrega
            {
                Id = entradas.Count + 1,
                FechaHora = DateTime.Now,
                Version = entrega.Version,
                IdEntrega = entrega.Id,
                FirmaArchivo = entrega.ObtenerFirma()
            };
            entradas.Add(registro);
        }

        public List<RegistroEntrega> ObtenerHistorial(Tarea tarea, Estudiante estudiante)
        {
            // Filtrar registros por tarea y estudiante
            var entregasTarea = tarea.ObtenerEntregas()
                .Where(e => e.Estudiante == estudiante)
                .Select(e => e.Id)
                .ToHashSet();

            return entradas
                .Where(r => entregasTarea.Contains(r.IdEntrega))
                .ToList();
        }

        public RegistroEntrega ObtenerUltimaVersion(Tarea tarea, Estudiante estudiante)
        {
            return ObtenerHistorial(tarea, estudiante)
                .OrderByDescending(r => r.Version)
                .FirstOrDefault()!;
        }
    }
}
