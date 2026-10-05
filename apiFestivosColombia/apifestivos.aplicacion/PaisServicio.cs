using apifestivos.core.repositorios;
using apifestivos.core.servicios;
using apifestivos.dominio;

namespace apifestivos.aplicacion
{
    public class PaisServicio : IPaisServicio
    {
        private readonly IPaisRepositorio repositorio;

        public PaisServicio(IPaisRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<Pais> Agregar(Pais Pais)
        {
            try
            {
                return await repositorio.Agregar(Pais);
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

        public async Task<Pais> Modificar(Pais Pais)
        {
            try
            {
                return await repositorio.Modificar(Pais);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Pais> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Pais>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }
    }
}
