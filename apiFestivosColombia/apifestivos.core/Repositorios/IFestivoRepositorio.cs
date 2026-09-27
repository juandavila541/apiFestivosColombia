using apifestivos.dominio;

namespace apifestivos.core.repositorios
{
    public interface IFestivoRepositorio
    {
        Task<IEnumerable<Festivo>> ObtenerTodos();

        Task<Festivo> Obtener(int Id);

        Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto);

        Task<Festivo> Agregar(Festivo Festivo);

        Task<Festivo> Modificar(Festivo Festivo);

        Task<bool> Eliminar(int Id);

        // Festivos por país
        Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais);
    }
}