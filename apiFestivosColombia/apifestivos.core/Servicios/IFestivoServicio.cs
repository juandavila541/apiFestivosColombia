using apifestivos.dominio;
using apifestivos.dominio.Dtos;

namespace apifestivos.core.servicios
{
    public interface IFestivoServicio
    {
        Task<IEnumerable<Festivo>> ObtenerTodos();

        Task<Festivo> Obtener(int Id);

        Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto);

        Task<Festivo> Agregar(Festivo Festivo);

        Task<Festivo> Modificar(Festivo Festivo);

        Task<bool> Eliminar(int Id);

        // Festivos por país
        Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais);

        // Verificar si una fecha es festivo
        Task<bool> EsFestivo(int IdPais, DateTime Fecha);

        // Fechas de los festivos de un país en un año
        Task<IEnumerable<FechaFestivoDto>> ObtenerFestivos(int IdPais, int Año);
    }
}
