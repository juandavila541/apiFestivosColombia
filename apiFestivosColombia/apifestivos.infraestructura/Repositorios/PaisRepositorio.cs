using apifestivos.core.repositorios;
using apifestivos.dominio;
using apifestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace apifestivos.infraestructura.Repositorios
{
    public class PaisRepositorio : IPaisRepositorio
    {
        private readonly FestivosContext contexto;

        // Inyeccion de dependencias
        public PaisRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Pais> Agregar(Pais Pais)
        {
            // agregar elemento al DbSet
            contexto.Paises.Add(Pais);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();
            // retornar registro agregado
            return contexto.Paises.FirstOrDefault(pais => pais.Id == Pais.Id);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var PaisExistente = await contexto.Paises.FindAsync(Id);
            if (PaisExistente == null)
            {
                return false;
            }
            try
            {
                // quitar elemento del dbset
                contexto.Paises.Remove(PaisExistente);
                // llevar los cambios a la base de datos
                await contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Pais> Modificar(Pais Pais)
        {
            // buscar el elemento en el dbset
            var PaisExistente = await contexto.Paises.FindAsync(Pais.Id);
            if (PaisExistente == null)
            {
                return null;
            }
            // cambiar los datos en el elemento del dbset
            contexto.Entry(PaisExistente).CurrentValues.SetValues(Pais);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();

            // retornar registro modificado
            return contexto.Paises.FirstOrDefault(pais => pais.Id == Pais.Id);
        }

        public async Task<Pais> Obtener(int Id)
        {
            return await contexto.Paises.FindAsync(Id);
        }

        public async Task<IEnumerable<Pais>> ObtenerTodos()
        {
            return await contexto.Paises
                .OrderBy(pais => pais.Nombre)
                .ToArrayAsync();
        }
    }
}
