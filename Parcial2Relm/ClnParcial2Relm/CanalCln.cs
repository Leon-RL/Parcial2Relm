using CadParcial2Relm;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClnParcial2Relm
{
    public static class CanalCln
    {
        // CRUD: Insertar un nuevo canal
        public static int insertar(Canal canal)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                canal.estado = 1; // 1 = Activo
                contexto.Canal.Add(canal);
                contexto.SaveChanges();
                return canal.id;
            }
        }

        // CRUD: Actualizar un canal existente
        public static int actualizar(Canal canal)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                Canal actual = contexto.Canal.Find(canal.id);
                actual.nombre = canal.nombre;
                actual.frecuencia = canal.frecuencia;
                contexto.SaveChanges();
                return actual.id;
            }
        }

        // CRUD: Obtener un canal por ID
        public static Canal obtenerUno(int idCanal)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                return contexto.Canal.Find(idCanal);
            }
        }

        // CRUD: Listar todos los canales activos
        public static List<Canal> listar()
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                return contexto.Canal.Where(c => c.estado != -1).ToList();
            }
        }

        // CRUD: Eliminación lógica
        public static void eliminar(int idCanal)
        {
            using (var contexto = new Parcial2RelmEntities1())
            {
                Canal canal = contexto.Canal.Find(idCanal);
                canal.estado = -1; // -1 = Eliminado
                contexto.SaveChanges();
            }
        }
    }
}