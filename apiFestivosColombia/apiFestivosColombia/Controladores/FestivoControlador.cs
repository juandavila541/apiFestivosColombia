using apifestivos.core.servicios;
using apifestivos.dominio;
using apifestivos.dominio.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivosColombia.Controladores
{
    [Route("api/festivos")]
    [ApiController]
    public class FestivoControlador : ControllerBase
    {
        private readonly IFestivoServicio servicio;

        public FestivoControlador(IFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Festivo>> Obtener(int Id)
        {
            var festivo = await servicio.Obtener(Id);
            if (festivo == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Festivo con ID= {Id}" });
            }
            return Ok(festivo);
        }

        // IndiceDato: 1 = nombre del festivo, 2 = nombre del país
        [HttpGet("{IndiceDato:int}/{Texto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> Buscar(int IndiceDato, string Texto)
        {
            var lista = await servicio.Buscar(IndiceDato, Texto);
            return Ok(lista);
        }

        [HttpGet("pais/{IdPais:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> ObtenerPorPais(int IdPais)
        {
            var lista = await servicio.ObtenerPorPais(IdPais);
            return Ok(lista);
        }

        [HttpGet("verificar/{IdPais:int}/{Anio:int}/{Mes:int}/{Dia:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Verificar(int IdPais, int Anio, int Mes, int Dia)
        {
            if (Anio < 1 || Anio > 9999 || Mes < 1 || Mes > 12 ||
                Dia < 1 || Dia > DateTime.DaysInMonth(Anio, Mes))
            {
                return BadRequest(new { mensaje = "Fecha no válida" });
            }
            var esFestivo = await servicio.EsFestivo(IdPais, new DateTime(Anio, Mes, Dia));
            return Ok(esFestivo ? "Es Festivo" : "No es festivo");
        }

        [HttpGet("obtener/{IdPais:int}/{Anio:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FechaFestivoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FechaFestivoDto>>> ObtenerFestivos(int IdPais, int Anio)
        {
            if (Anio < 1 || Anio > 9999)
            {
                return BadRequest(new { mensaje = "Año no válido" });
            }
            var lista = await servicio.ObtenerFestivos(IdPais, Anio);
            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Festivo>> Agregar([FromBody] Festivo Festivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nuevoFestivo = await servicio.Agregar(Festivo);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoFestivo.Id }, nuevoFestivo);
            }
            catch (Exception ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Festivo>> Modificar([FromBody] Festivo Festivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var festivoModificado = await servicio.Modificar(Festivo);
                if (festivoModificado == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el Festivo con ID= {Festivo.Id}" });
                }
                return Ok(festivoModificado);
            }
            catch (Exception ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Eliminar(int Id)
        {
            var eliminado = await servicio.Eliminar(Id);
            if (!eliminado)
            {
                return NotFound(new { mensaje = $"No se pudo eliminar el Festivo con ID= {Id}" });
            }
            return Ok(new { mensaje = $"Festivo con ID= {Id} eliminado" });
        }
    }
}
