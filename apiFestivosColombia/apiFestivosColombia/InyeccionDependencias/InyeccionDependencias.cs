using apifestivos.aplicacion;
using apifestivos.core.repositorios;
using apifestivos.core.servicios;
using apifestivos.infraestructura.Persistencia;
using apifestivos.infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace apiFestivosColombia.InyeccionDependencias
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios,
                                                IConfiguration configuracion
                                                )
        {
            // agregar el DBContext
            servicios.AddDbContext<FestivosContext>(opciones =>
            {
                opciones.UseSqlServer(configuracion.GetConnectionString("Festivos"));
            });

            // agregar los repositorios
            servicios.AddTransient<IPaisRepositorio, PaisRepositorio>();
            servicios.AddTransient<ITipoFestivoRepositorio, TipoFestivoRepositorio>();
            servicios.AddTransient<IFestivoRepositorio, FestivoRepositorio>();

            // agregar los servicios
            servicios.AddTransient<IPaisServicio, PaisServicio>();
            servicios.AddTransient<ITipoFestivoServicio, TipoFestivoServicio>();
            servicios.AddTransient<IFestivoServicio, FestivoServicio>();

            return servicios;
        }
    }
}
