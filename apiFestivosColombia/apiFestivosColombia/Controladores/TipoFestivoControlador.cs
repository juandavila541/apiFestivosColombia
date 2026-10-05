using apifestivos.core.servicios;
using apifestivos.dominio;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivosColombia.Controladores
{
    [Route("api/tipos")]
    [ApiController]
    public class TipoFestivoControlador : ControllerBase
    {
        private readonly ITipoFestivoServicio servicio;

        public TipoFestivoControlador(ITipoFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoFestivo>))]
        public async Task<ActionResult<IEnumerable<TipoFestivo>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoFestivo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoFestivo>> Obtener(int Id)
        {
            var tipo = await servicio.Obtener(Id);
            if (tipo == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Tipo de festivo con ID= {Id}" });
            }
            return Ok(tipo);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoFestivo))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TipoFestivo>> Agregar([FromBody] TipoFestivo TipoFestivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nuevoTipo = await servicio.Agregar(TipoFestivo);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoTipo.Id }, nuevoTipo);
            }
            catch (Exception ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoFestivo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TipoFestivo>> Modificar([FromBody] TipoFestivo TipoFestivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var tipoModificado = await servicio.Modificar(TipoFestivo);
                if (tipoModificado == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el Tipo de festivo con ID= {TipoFestivo.Id}" });
                }
                return Ok(tipoModificado);
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
                return NotFound(new { mensaje = $"No se pudo eliminar el Tipo de festivo con ID= {Id} (no existe o tiene festivos asociados)" });
            }
            return Ok(new { mensaje = $"Tipo de festivo con ID= {Id} eliminado" });
        }
    }
}
