public class Tarea
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Color { get; set; }
    public EstadoTarea Estado { get; set; }
    public int? IdUsuarioAsignado { get; set; }
}