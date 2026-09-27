using apifestivos.dominio;

namespace apifestivos.core.servicios
{
    public interface ITipoFestivoServicio
    {
        Task<IEnumerable<TipoFestivo>> ObtenerTodos();

        Task<TipoFestivo> Obtener(int Id);

        Task<TipoFestivo> Agregar(TipoFestivo TipoFestivo);

        Task<TipoFestivo> Modificar(TipoFestivo TipoFestivo);

        Task<bool> Eliminar(int Id);
    }
}