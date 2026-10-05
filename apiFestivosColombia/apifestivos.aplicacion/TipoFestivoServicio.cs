using apifestivos.core.repositorios;
using apifestivos.core.servicios;
using apifestivos.dominio;

namespace apifestivos.aplicacion
{
    public class TipoFestivoServicio : ITipoFestivoServicio
    {
        private readonly ITipoFestivoRepositorio repositorio;

        public TipoFestivoServicio(ITipoFestivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<TipoFestivo> Agregar(TipoFestivo TipoFestivo)
        {
            try
            {
                return await repositorio.Agregar(TipoFestivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await repositorio.Eliminar(Id);
        }

        public async Task<TipoFestivo> Modificar(TipoFestivo TipoFestivo)
        {
            try
            {
                return await repositorio.Modificar(TipoFestivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<TipoFestivo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<TipoFestivo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }
    }
}
