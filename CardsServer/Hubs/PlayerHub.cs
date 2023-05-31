using CardsServer.Entity.Game;
using CardsServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace CardsServer.Hubs;

public class PlayerHub : Hub
{
    private readonly ILogger<PlayerHub> _logger;
    private readonly PlayerService _playerService;

    public PlayerHub(ILogger<PlayerHub> logger, PlayerService playerService)
    {
        _logger = logger;
        _playerService = playerService;
    }

    public Player? LoginOrRegister(string username, string? userId = null)
    {
        if(userId is null)
        {
            return _playerService.CreatePlayer(username);
        }

        Player? player = _playerService.GetPlayer(Guid.Parse(userId));

        if(player == null)
        {
            return _playerService.CreatePlayer(username);
        }

        return player;
    }


}
