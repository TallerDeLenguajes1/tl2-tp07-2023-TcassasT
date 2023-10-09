namespace EspacioAccesoADatos;

using EspacioTarea;
using System.Text.Json;

public class AccesoADatos {
  public List<Tarea> GetTareas() {
    string archivoPath = "tareas.json";
    List<Tarea> listaDeTareas = new List<Tarea>();

    if (Existe(archivoPath)) {
      string? contenidoDeArchivoTareas = File.ReadAllText(archivoPath);

      listaDeTareas = JsonSerializer.Deserialize<List<Tarea>>(contenidoDeArchivoTareas);
    }

    return listaDeTareas;
  }

  public List<Tarea> GetTareas(ESTADO_TAREA estado) {
    List<Tarea> listaDeTareas = GetTareas();
    List<Tarea> listaDeTareasFiltradas = new List<Tarea>();
    listaDeTareas.ForEach(tareaItem => {
      if (tareaItem.Estado.ToString().Equals(estado.ToString())) {
        listaDeTareasFiltradas.Add(tareaItem);
      }
    });
    return listaDeTareasFiltradas;
  }

  public Boolean AddTarea(Tarea tareaNueva) {
    string archivoPath = "tareas.json";
    List<Tarea> listaDeTareas = GetTareas();
    int cantidadDeTareasAnterior = listaDeTareas.Count();

    listaDeTareas.Add(tareaNueva);

    File.WriteAllText(archivoPath, JsonSerializer.Serialize<List<Tarea>>(listaDeTareas));

    return GetTareas().Count() > cantidadDeTareasAnterior;
  }

  public Boolean SetTareas(List<Tarea> listaDeTareas) {
    string archivoPath = "tareas.json";
    File.WriteAllText(archivoPath, JsonSerializer.Serialize<List<Tarea>>(listaDeTareas));
    return GetTareas().Count() == listaDeTareas.Count();
  }

  public Tarea GetTarea(int tareaId) {
    List<Tarea> listaDeTareas = GetTareas();
    return listaDeTareas.FirstOrDefault(tareaItem => tareaItem.Id == tareaId, null);
  }

  public Tarea UpdateTarea(Tarea tareaOriginal, Tarea modificaciones) {
    Tarea tareaModificada = tareaOriginal;

    if (modificaciones.Titulo != null) {
      tareaModificada.Titulo = modificaciones.Titulo;
    }

    if (modificaciones.Descripcion != null) {
      tareaModificada.Descripcion = modificaciones.Descripcion;
    }

    if (modificaciones.Estado != null) {
      tareaModificada.Estado = modificaciones.Estado;
    }

    List<Tarea> listaDeTareas = GetTareas();
    Tarea tareaARemover = listaDeTareas.Find(tareaItem => tareaItem.Id == tareaOriginal.Id);
    listaDeTareas.Remove(tareaARemover);
    listaDeTareas.Add(tareaModificada);
    SetTareas(listaDeTareas);

    return tareaModificada;
  }

  public Boolean BorrarTarea(int tareaId) {
    List<Tarea> listaDeTareas = GetTareas();
    int cantidadDeTareasAnterior = listaDeTareas.Count();
    Tarea tareaARemover = listaDeTareas.Find(tareaItem => tareaItem.Id == tareaId);
    listaDeTareas.Remove(tareaARemover);
    SetTareas(listaDeTareas);
    return GetTareas().Count() < cantidadDeTareasAnterior;
  }

  private Boolean Existe(string archivoPath) {
    Boolean existeYTieneCotenido = false;

    if (File.Exists(archivoPath)) {
      string? contenidoDeArchivo = File.ReadAllText(archivoPath);

      if (!string.IsNullOrEmpty(contenidoDeArchivo)) {
        existeYTieneCotenido = true;
      }
    }

    return existeYTieneCotenido;
  }
}