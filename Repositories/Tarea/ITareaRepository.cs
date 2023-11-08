using Models;

namespace Repository;
public interface ITareaRepository
{
    public Tarea CreateTask(int boardId);
    public void UpdateTask(int taskId, Tarea task);
    public Tarea GetTaskById(int taskId);
    public List<Tarea> ListTasksByUser(int userId);
    public List<Tarea> ListTasksByBoard(int boardId);
    public void DeleteBoard(int taskId);
    public void AssignUserToTask(int userId, int taskId);
}