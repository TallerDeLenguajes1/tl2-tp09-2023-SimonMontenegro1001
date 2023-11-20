using kanban.Models;
using kanban.Repository;
using Microsoft.AspNetCore.Mvc;

namespace kanban.Controllers;

[ApiController]
[Route("api/tableros")]
public class TableroController : ControllerBase
{
    private readonly ITableroRepository tableroRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    [HttpPost]
    public ActionResult<Tablero> CreateBoard(Tablero board)
    {
        tableroRepository.CreateBoard(board);
        return Created("ubicacion del recurso", board);
    }

    [HttpGet]
    public ActionResult<List<Tablero>> GetAllBoards()
    {
        List<Tablero> boards = tableroRepository.ListBoards();

        if (boards != null)
        {
            return Ok(boards);
        }
        else
        {
            return NotFound();
        }

    }
}
