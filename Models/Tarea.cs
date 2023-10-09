namespace EspacioTarea;

using EspacioAccesoADatos;
using System.Text.Json.Serialization;

public enum ESTADO_TAREA {
  PENDIENTE,
  EN_PROGRESO,
  COMPLETADA,
}

public class Tarea {
  private int id;
  private string? titulo;
  private string? descripcion;
  private ESTADO_TAREA estado;

  [JsonPropertyName("id")]
  public int Id { get => id; set => id = value; }
  [JsonPropertyName("titulo")]
  public string? Titulo { get => titulo; set => titulo = value; }
  [JsonPropertyName("descripcion")]
  public string? Descripcion { get=> descripcion; set => descripcion = value; }
  [JsonPropertyName("estado")]
  public ESTADO_TAREA Estado { get => estado; set => estado = (
    Enum.TryParse<ESTADO_TAREA>(value.ToString(), out ESTADO_TAREA nuevoEstado) ? value : ESTADO_TAREA.PENDIENTE
  ); }
}