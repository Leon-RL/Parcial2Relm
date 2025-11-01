using CadParcial2Relm;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClnParcial2Relm
{
    public static class ProgramaCln
    {
        // CRUD: Insertar un nuevo programa
        public static int insertar(Programa programa)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                programa.estado = 1; // 1 = Activo
                contexto.Programa.Add(programa);
                contexto.SaveChanges();
                return programa.id;
            }
        }

        // CRUD: Actualizar un programa existente
        public static int actualizar(Programa programa)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                Programa actual = contexto.Programa.Find(programa.id);
                actual.idCanal = programa.idCanal;
                actual.titulo = programa.titulo;
                actual.duracion = programa.duracion;
                actual.productor = programa.productor;
                actual.fechaEstreno = programa.fechaEstreno;
                contexto.SaveChanges();
                return actual.id;
            }
        }

        // CRUD: Obtener un programa por ID
        public static Programa obtenerUno(int idPrograma)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                return contexto.Programa.Find(idPrograma);
            }
        }

        // CRUD: Listar todos los programas activos
        public static List<Programa> listar()
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                return contexto.Programa.Where(p => p.estado != -1).ToList();
            }
        }

        // CRUD: Eliminación lógica
        public static void eliminar(int idPrograma)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                Programa programa = contexto.Programa.Find(idPrograma);
                programa.estado = -1; // -1 = Eliminado
                contexto.SaveChanges();
            }
        }

        // AUXILIAR: Listar canales para el ComboBox (cboCanal)
        public static List<Canal> listarCanales()
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                // Solo lista canales activos (estado 1)
                return contexto.Canal.Where(c => c.estado == 1).ToList();
            }
        }
    }
}