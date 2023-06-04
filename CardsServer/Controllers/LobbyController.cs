using CardsServer.Entity.Game;
using CardsServer.Hubs;
using CardsServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CardsServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController : ControllerBase
{
    private readonly ILogger<LobbyController> _logger;
    private readonly GameLobbyService _gameLobbyService;

    public LobbyController(ILogger<LobbyController> logger, GameLobbyService gameLobbyService)
    {
        _logger = logger;
        _gameLobbyService = gameLobbyService;
    }

    [HttpGet("GetLobbies")]
    public async Task<List<GameLobby>> GetLobbies()
    {
        return _gameLobbyService.GetAllLobbies();
    }

    [HttpGet("GetPlayersInLobby")]
    public async Task<IActionResult> GetPlayersInLobby(Guid lobbyId)
    {
        List<Player>? playersInLobby = _gameLobbyService.GetPlayersInLobby(lobbyId);

        if (playersInLobby is null)
        {
            return NotFound("Could not find a lobbi with that id");
        }

        return Ok(playersInLobby);
    }

    [HttpPost("CreateGameLobby")]
    public async Task<Guid> CreateGameLobby()
    {
        return _gameLobbyService.CreateGameLobby().Guid;
    }

    [HttpPost("JoinGameLobby")]
    public async Task<IActionResult> JoinGameLobby(Guid lobbyGuid, Guid playerGuid)
    {
        var result = _gameLobbyService.JoinGameLobby(lobbyGuid, playerGuid);

        return Ok(result);
    }
}