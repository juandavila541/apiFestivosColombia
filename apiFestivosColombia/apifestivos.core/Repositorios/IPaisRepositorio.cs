using apifestivos.dominio;

namespace apifestivos.core.repositorios
{
    public interface IPaisRepositorio
    {
        Task<IEnumerable<Pais>> ObtenerTodos();

        Task<Pais> Obtener(int Id);

        Task<Pais> Agregar(Pais Pais);

        Task<Pais> Modificar(Pais Pais);

        Task<bool> Eliminar(int Id);
    }
}