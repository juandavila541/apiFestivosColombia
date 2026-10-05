using apifestivos.core.repositorios;
using apifestivos.core.servicios;
using apifestivos.dominio;
using apifestivos.dominio.Dtos;

namespace apifestivos.aplicacion
{
    public class FestivoServicio : IFestivoServicio
    {
        private readonly IFestivoRepositorio repositorio;

        public FestivoServicio(IFestivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            ValidarFecha(Festivo);
            try
            {
                return await repositorio.Agregar(Festivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto)
        {
            return await repositorio.Buscar(IndiceDato, Texto);
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await repositorio.Eliminar(Id);
        }

        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            ValidarFecha(Festivo);
            try
            {
                return await repositorio.Modificar(Festivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais)
        {
            return await repositorio.ObtenerPorPais(IdPais);
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }

        public async Task<bool> EsFestivo(int IdPais, DateTime Fecha)
        {
            var festivos = await ObtenerFestivos(IdPais, Fecha.Year);
            return festivos.Any(festivo => festivo.Fecha.Date == Fecha.Date);
        }

        public async Task<IEnumerable<FechaFestivoDto>> ObtenerFestivos(int IdPais, int Año)
        {
            var festivos = await repositorio.ObtenerPorPais(IdPais);

            var fechas = new List<FechaFestivoDto>();
            foreach (var festivo in festivos)
            {
                var fecha = CalcularFecha(festivo, Año);
                if (fecha != null)
                {
                    fechas.Add(new FechaFestivoDto { Festivo = festivo.Nombre, Fecha = fecha.Value });
                }
            }
            return fechas.OrderBy(festivo => festivo.Fecha);
        }

        private static DateTime? CalcularFecha(Festivo festivo, int año)
        {
            return festivo.IdTipo switch
            {
                // Fijo
                1 => new DateTime(año, festivo.Mes, festivo.Dia),
                // Ley Puente Festivo
                2 => ServicioFechas.SiguienteLunes(new DateTime(año, festivo.Mes, festivo.Dia)),
                // Basado en Pascua
                3 => ServicioFechas.AgregarDias(ServicioFechas.ObtenerPascua(año), festivo.DiasPascua ?? 0),
                // Basado en Pascua y Ley Puente Festivo
                4 => ServicioFechas.SiguienteLunes(
                        ServicioFechas.AgregarDias(ServicioFechas.ObtenerPascua(año), festivo.DiasPascua ?? 0)),
                // Ley Puente Festivo Viernes
                5 => ServicioFechas.TrasladarFestivoViernes(new DateTime(año, festivo.Mes, festivo.Dia)),
                _ => null
            };
        }

        private static void ValidarFecha(Festivo festivo)
        {
            bool usaDiaMes = festivo.IdTipo == 1 || festivo.IdTipo == 2 || festivo.IdTipo == 5;
            if (usaDiaMes && (festivo.Mes < 1 || festivo.Mes > 12 ||
                              festivo.Dia < 1 || festivo.Dia > DateTime.DaysInMonth(2023, festivo.Mes)))
            {
                throw new InvalidOperationException($"El día {festivo.Dia} y mes {festivo.Mes} no forman una fecha válida");
            }
        }
    }
}
