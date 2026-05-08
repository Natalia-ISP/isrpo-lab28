using GamesApi.Models;
using GamesApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Contollers;

[ApiController]
[Route("api/[Controller]")]
public class GamesController : ControllerBase {
    [HttpGet]
    public ActionResult<List<Game>> GetAll() {
        return Ok(GameStore.Games);
    }

    [HttpGet("{id}")]
    public ActionResult<Game> GetById(int id) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"игра с id={id} не найдена" });
        }
        return Ok(game);
    }

    [HttpPost]
    public ActionResult<Game> Create([FromBody] Game game) {
        game.Id = GameStore.NextId();
        GameStore.Games.Add(game);
        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"игра с id={id} не найдена" });
        }
        GameStore.Games.Remove(game);
        return NoContent();
    }

    [HttpPut("{id}")]
    public ActionResult<Game> Update(int id, [FromBody] Game update) {
        var game = GameStore.Games.FirstOrDefault(g => g.Id == id);
        if (game is null) {
            return NotFound(new { message = $"игра с id={id} не найдена" });
        }
        game.Title = update.Title;
        game.Genre = update.Genre;
        game.ReleaseYear = update.ReleaseYear;
        return Ok(game);
    }
}