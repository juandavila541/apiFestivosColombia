using apifestivos.core.repositorios;
using apifestivos.dominio;
using apifestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace apifestivos.infraestructura.Repositorios
{
    public class TipoFestivoRepositorio : ITipoFestivoRepositorio
    {
        private readonly FestivosContext contexto;

        // Inyeccion de dependencias
        public TipoFestivoRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<TipoFestivo> Agregar(TipoFestivo TipoFestivo)
        {
            // agregar elemento al DbSet
            contexto.TiposFestivo.Add(TipoFestivo);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();
            // retornar registro agregado
            return contexto.TiposFestivo.FirstOrDefault(tipo => tipo.Id == TipoFestivo.Id);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var TipoFestivoExistente = await contexto.TiposFestivo.FindAsync(Id);
            if (TipoFestivoExistente == null)
            {
                return false;
            }
            try
            {
                // quitar elemento del dbset
                contexto.TiposFestivo.Remove(TipoFestivoExistente);
                // llevar los cambios a la base de datos
                await contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TipoFestivo> Modificar(TipoFestivo TipoFestivo)
        {
            // buscar el elemento en el dbset
            var TipoFestivoExistente = await contexto.TiposFestivo.FindAsync(TipoFestivo.Id);
            if (TipoFestivoExistente == null)
            {
                return null;
            }
            // cambiar los datos en el elemento del dbset
            contexto.Entry(TipoFestivoExistente).CurrentValues.SetValues(TipoFestivo);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();

            // retornar registro modificado
            return contexto.TiposFestivo.FirstOrDefault(tipo => tipo.Id == TipoFestivo.Id);
        }

        public async Task<TipoFestivo> Obtener(int Id)
        {
            return await contexto.TiposFestivo.FindAsync(Id);
        }

        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await contexto.TiposFestivo
                .OrderBy(tipo => tipo.Tipo)
                .ToArrayAsync();
        }
    }
}
