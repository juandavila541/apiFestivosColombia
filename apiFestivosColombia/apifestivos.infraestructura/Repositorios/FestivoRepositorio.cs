using apifestivos.core.repositorios;
using apifestivos.dominio;
using apifestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace apifestivos.infraestructura.Repositorios
{
    public class FestivoRepositorio : IFestivoRepositorio
    {
        private readonly FestivosContext contexto;

        // Inyeccion de dependencias
        public FestivoRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            // agregar elemento al DbSet
            contexto.Festivos.Add(Festivo);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();
            // retornar registro agregado
            return contexto.Festivos.FirstOrDefault(festivo => festivo.Id == Festivo.Id);
        }

        public async Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto)
        {
            if (IndiceDato == 1)
                return await contexto.Festivos
                    .Where(festivo => festivo.Nombre.Contains(Texto))
                    .Include(festivo => festivo.Pais)
                    .Include(festivo => festivo.TipoFestivo)
                    .OrderBy(festivo => festivo.Nombre)
                    .ToArrayAsync();
            else
                return await contexto.Festivos
                    .Where(festivo => festivo.Pais.Nombre.Contains(Texto))
                    .Include(festivo => festivo.Pais)
                    .Include(festivo => festivo.TipoFestivo)
                    .OrderBy(festivo => festivo.Nombre)
                    .ToArrayAsync();
        }

        public async Task<bool> Eliminar(int Id)
        {
            var FestivoExistente = await contexto.Festivos.FindAsync(Id);
            if (FestivoExistente == null)
            {
                return false;
            }
            try
            {
                // quitar elemento del dbset
                contexto.Festivos.Remove(FestivoExistente);
                // llevar los cambios a la base de datos
                await contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            // buscar el elemento en el dbset
            var FestivoExistente = await contexto.Festivos.FindAsync(Festivo.Id);
            if (FestivoExistente == null)
            {
                return null;
            }
            // cambiar los datos en el elemento del dbset
            contexto.Entry(FestivoExistente).CurrentValues.SetValues(Festivo);
            // llevar los cambios a la base de datos
            await contexto.SaveChangesAsync();

            // retornar registro modificado
            return contexto.Festivos.FirstOrDefault(festivo => festivo.Id == Festivo.Id);
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await contexto.Festivos
                .Include(festivo => festivo.Pais)
                .Include(festivo => festivo.TipoFestivo)
                .FirstOrDefaultAsync(festivo => festivo.Id == Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais)
        {
            return await contexto.Festivos
                .Where(festivo => festivo.IdPais == IdPais)
                .Include(festivo => festivo.Pais)
                .Include(festivo => festivo.TipoFestivo)
                .OrderBy(festivo => festivo.Mes)
                .ThenBy(festivo => festivo.Dia)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await contexto.Festivos
                .Include(festivo => festivo.Pais)
                .Include(festivo => festivo.TipoFestivo)
                .OrderBy(festivo => festivo.Mes)
                .ThenBy(festivo => festivo.Dia)
                .ToArrayAsync();
        }
    }
}
