using CardsServer.Entity.Game;
using CardsServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace CardsServer.Hubs;

public class LobbyHub : Hub
{
    private readonly ILogger<LobbyHub> _logger;
    private readonly GameLobbyService _gameLobbyService;
    private readonly PlayerService _playerService;

    public LobbyHub(ILogger<LobbyHub> logger, GameLobbyService gameLobbyService, PlayerService playerService)
    {
        _logger = logger;
        _gameLobbyService = gameLobbyService;
        _playerService = playerService;
    }

    public async Task SendLobbyData()
    {
        _logger.LogInformation("Sending lobby data");
        await Clients.All.SendAsync("LobbyData", _gameLobbyService.GetAllLobbies());
    }

    public async Task SendLobbyChanged(Guid lobbyId)
    {
        _logger.LogInformation("Send lobby changed");

        await Clients.All.SendAsync("LobbyChanged", _gameLobbyService.GetLobby(lobbyId));
    }

    public GameLobby? GetGameLobby(Guid lobbyId)
    {
        return _gameLobbyService.GetLobby(lobbyId);
    }

    public GameLobby CreateLobby()
    {
        _logger.LogInformation("Creating lobby");
        GameLobby lobby = _gameLobbyService.CreateGameLobby();

        _ = SendLobbyData();

        return lobby;
    }

    public async Task<GameLobby?> JoinLobby(Guid lobbyId, Guid playerId)
    {
        try
        {
            GameLobby? gameLobby = _gameLobbyService.JoinGameLobby(lobbyId, playerId);

            if (gameLobby is not null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());
                _ = Clients.Group(lobbyId.ToString()).SendAsync("GroupLobbyPlayersChanged", _gameLobbyService.GetPlayersInLobby(lobbyId));
                _ = SendLobbyChanged(lobbyId);
                
            }

            return gameLobby;
        }catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return null;
    }

    public async Task LeaveLobby(Guid lobbyId, Guid playerId)
    {
        _gameLobbyService.LeaveLobby(lobbyId, playerId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId.ToString());

        _ = Clients.Group(lobbyId.ToString()).SendAsync("GroupLobbyPlayersChanged", _gameLobbyService.GetPlayersInLobby(lobbyId));

        _ = SendLobbyData();
    }

    public List<GameLobby> GetLobbies()
    {
        _logger.LogInformation("Getting lobbies");

        return _gameLobbyService.GetAllLobbies();
    }
}
