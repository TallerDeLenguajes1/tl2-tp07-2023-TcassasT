using Microsoft.AspNetCore.Mvc;
using EspacioTarea;
using EspacioAccesoADatos;

[ApiController]
[Route("[controller]")]
public class TareasController: ControllerBase {
  private readonly ILogger<TareasController> _logger;
  public TareasController(ILogger<TareasController> logger) {
    _logger = logger;
  }
  private AccesoADatos accesoADatos = new AccesoADatos();

  [HttpPost("AgregaTarea")]
  public IActionResult AgregaTarea(Tarea tarea) {
    if (accesoADatos.AddTarea(tarea)) {
      return Ok("Tarea creada");
    } else {
      return BadRequest("No se pudo crear tarea");
    }
  }

  [HttpGet("ObtieneTarea/{tareaId}")]
  public IActionResult ObtieneTarea(int tareaId) {
    Tarea tareaBuscada = accesoADatos.GetTarea(tareaId);

    if (tareaBuscada == null) {
      return Ok(tareaBuscada);
    } else {
      return NotFound("No se encontró tarea");
    }
  }

  [HttpPut("UpdateTarea/{tareaId}")]
  public IActionResult UpdateTarea(int tareaId, Tarea modificaciones) {
    Tarea tareaBuscada = accesoADatos.GetTarea(tareaId);

    if (tareaBuscada == null) {
      return NotFound("No se encontró tarea para updetear");
    }

    accesoADatos.UpdateTarea(tareaBuscada, modificaciones);

    return Ok("Tarea actualizada");
  }

  [HttpDelete("BorrarTarea/{tareaId}")]
  public IActionResult BorrarTarea(int tareaId) {
    Tarea tareaBuscada = accesoADatos.GetTarea(tareaId);

    if (tareaBuscada == null) {
      return NotFound("No se encontró tarea para borrar");
    }

    accesoADatos.BorrarTarea(tareaId);

    return Ok("Tarea borrada");
  }

  [HttpGet("ListarTareas")]
  public List<Tarea> ListarTareas() {
    return accesoADatos.GetTareas();
  }

  [HttpGet("ListarTareasCompletadas")]
  public List<Tarea> ListarTareasCompletadas() {
    return accesoADatos.GetTareas(ESTADO_TAREA.COMPLETADA);
  }
}