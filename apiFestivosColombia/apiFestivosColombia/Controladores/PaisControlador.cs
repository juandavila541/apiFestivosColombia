using apifestivos.core.servicios;
using apifestivos.dominio;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivosColombia.Controladores
{
    [Route("api/paises")]
    [ApiController]
    public class PaisControlador : ControllerBase
    {
        private readonly IPaisServicio servicio;

        public PaisControlador(IPaisServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pais>))]
        public async Task<ActionResult<IEnumerable<Pais>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pais>> Obtener(int Id)
        {
            var pais = await servicio.Obtener(Id);
            if (pais == null)
            {
                return NotFound(new { mensaje = $"No se encontró el País con ID= {Id}" });
            }
            return Ok(pais);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Pais>> Agregar([FromBody] Pais Pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nuevoPais = await servicio.Agregar(Pais);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoPais.Id }, nuevoPais);
            }
            catch (Exception ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Pais>> Modificar([FromBody] Pais Pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var paisModificado = await servicio.Modificar(Pais);
                if (paisModificado == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el País con ID= {Pais.Id}" });
                }
                return Ok(paisModificado);
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
                return NotFound(new { mensaje = $"No se pudo eliminar el País con ID= {Id} (no existe o tiene festivos asociados)" });
            }
            return Ok(new { mensaje = $"País con ID= {Id} eliminado" });
        }
    }
}
