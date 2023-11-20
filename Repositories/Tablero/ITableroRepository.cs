using kanban.Models;

namespace kanban.Repository;
public interface ITableroRepository
{
    public Tablero CreateBoard(Tablero board);
    public void UpdateBoard(int boardId, Tablero board);
    public List<Tablero> ListBoards();
    public Tablero GetBoardById(int boardId);
    public void DeleteBoard(int boardId);
    public List<Tablero> ListUserBoards(int userId);
}