using System.Data.SQLite;
using Models;

namespace Repository;

public class TareaRepository : ITareaRepository
{
    private readonly string connectionString = "Data Source=DB/kanban.db;Cache=Shared";
    public void AssignUserToTask(int userId, int taskId)
    {
        var queryString = @"UPDATE Tarea SET id_usuario_propietario = @userId WHERE id = @taskId;";

        using var connection = new SQLiteConnection(connectionString);
        using var command = new SQLiteCommand(queryString, connection);
        connection.Open();

        command.Parameters.Add(new SQLiteParameter("@userId", userId));
        command.Parameters.Add(new SQLiteParameter("@taskId", taskId));

        command.ExecuteNonQuery();
        connection.Close();
    }

    public Tarea CreateTask(int boardId)
    {
        var query = $"INSERT INTO tarea (nombre_de_usuario) VALUES (id_tablero) VALUES(@boardId);";
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        var command = new SQLiteCommand(query, connection);
        command.Parameters.Add(new SQLiteParameter("@boardId", boardId));

        //  se crea un default ?

        // command.Parameters.Add(new SQLiteParameter("@nombre", task.Nombre));
        // command.Parameters.Add(new SQLiteParameter("@estado", task.Estado));
        // command.Parameters.Add(new SQLiteParameter("@descripcion", task.Descripcion));
        // command.Parameters.Add(new SQLiteParameter("@color", task.Color));

        command.ExecuteNonQuery();

        connection.Close();
        // que deberia devolver ? 
        return new Tarea();

    }

    public void DeleteBoard(int taskId)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM tarea WHERE Id = @taskId";
        command.Parameters.Add(new SQLiteParameter("@taskId", taskId));
        command.ExecuteNonQuery();
    }

    public Tarea GetTaskById(int taskId)
    {
        var task = new Tarea();

        using var connection = new SQLiteConnection(connectionString);
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM tarea WHERE id = @taskId";
        command.Parameters.Add(new SQLiteParameter("@taskId", taskId));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                task.Id = Convert.ToInt32(reader["id"]);
                task.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                task.Nombre = reader["nombre"].ToString();
                task.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                task.Descripcion = reader["descripcion"].ToString();
                task.Color = reader["color"].ToString();
                task.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_propietario"]);
            }
        }
        connection.Close();
        return task;
    }

    public List<Tarea> ListTasksByBoard(int boardId)
    {
        var queryString = @"SELECT * FROM Tarea WHERE id_tablero = @boardId;";
        var tasks = new List<Tarea>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("boardId", boardId));
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var task = new Tarea
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]),
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_propietario"])
                    };
                    tasks.Add(task);
                }
            }
            connection.Close();
        }
        return tasks;
    }

    public List<Tarea> ListTasksByUser(int userId)
    {
        var queryString = @"SELECT * FROM tarea WHERE id_usuario_propietario = @userId;";
        var tasks = new List<Tarea>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@userId", userId));
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var task = new Tarea()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        IdTablero = Convert.ToInt32(reader["id_tablero"]),
                        Nombre = reader["nombre"].ToString(),
                        Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]),
                        Descripcion = reader["descripcion"].ToString(),
                        Color = reader["color"].ToString(),
                        IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_propietario"])
                    };
                    tasks.Add(task);
                }
            }
            connection.Close();
        }
        return tasks;
    }

    public void UpdateTask(int taskId, Tarea task)
    {
        var queryString = @"UPDATE tarea SET nombre = @name, estado = @status, descripcion = @description, color = @color
                        WHERE id = @taskId;";

        using var connection = new SQLiteConnection(connectionString);
        using var command = new SQLiteCommand(queryString, connection);

        connection.Open();

        command.Parameters.Add(new SQLiteParameter("@name", task.Nombre));
        command.Parameters.Add(new SQLiteParameter("@status", task.Estado));
        command.Parameters.Add(new SQLiteParameter("@description", task.Descripcion));
        command.Parameters.Add(new SQLiteParameter("@color", task.Color));
        command.Parameters.Add(new SQLiteParameter("@taskId", taskId));

        command.ExecuteNonQuery();

        connection.Close();
    }

}