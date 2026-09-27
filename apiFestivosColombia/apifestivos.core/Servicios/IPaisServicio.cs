using apifestivos.dominio;

namespace apifestivos.core.servicios
{
    public interface IPaisServicio
    {
        Task<IEnumerable<Pais>> ObtenerTodos();

        Task<Pais> Obtener(int Id);

        Task<Pais> Agregar(Pais Pais);

        Task<Pais> Modificar(Pais Pais);

        Task<bool> Eliminar(int Id);
    }
}