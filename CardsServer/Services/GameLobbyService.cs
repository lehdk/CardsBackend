using CardsServer.Entity.Game;

namespace CardsServer.Services;

public class GameLobbyService
{
    private readonly ILogger<GameLobbyService> _logger;
    private readonly PlayerService _playerService;
    private readonly Dictionary<Guid, GameLobby> _lobbies;

    public GameLobbyService(ILogger<GameLobbyService> logger, PlayerService playerService)
    {
        logger.LogInformation("Starting GameLobbyService");
        _logger = logger;
        _playerService = playerService;
        _lobbies = new Dictionary<Guid, GameLobby>();

        var l0 = new GameLobby()
        {
            Status = LobbyStatus.Open
        };
        
        var l1 = new GameLobby()
        {
            Status = LobbyStatus.Locked
        };
        
        var l2 = new GameLobby()
        {
            Status = LobbyStatus.Started
        };

        _lobbies.Add(l0.Guid, l0);
        _lobbies.Add(l1.Guid, l1);
        _lobbies.Add(l2.Guid, l2);
    }

    public List<GameLobby> GetAllLobbies()
    {
        return _lobbies.Values.ToList();
    }

    public GameLobby? GetLobby(Guid id)
    {
        return _lobbies[id];
    }

    public List<Player>? GetPlayersInLobby(Guid lobbyId)
    {
        if (!_lobbies.ContainsKey(lobbyId))
        {
            return null;
        }

        GameLobby lobby = _lobbies[lobbyId];
        return lobby.GetAllPlayers();
    }

    public GameLobby CreateGameLobby()
    {
        GameLobby gameLobby = new GameLobby();

        _lobbies.Add(gameLobby.Guid, gameLobby);

        return gameLobby;
    }

    public GameLobby? JoinGameLobby(Guid gameLobbyId, Guid playerId)
    {
        Player? playerFromService = _playerService.GetPlayer(playerId);

        if(playerFromService is null) return null;

        if (playerFromService.JoinedLobbyGuid is not null) return null;

        if(!_lobbies.ContainsKey(gameLobbyId)) 
        {
            return null;
        }

        GameLobby lobby = _lobbies[gameLobbyId];

        lobby.AddPlayer(playerFromService);

        playerFromService.JoinedLobbyGuid = lobby.Guid;

        return lobby;
    }

    public void LeaveLobby(Guid lobbyId, Guid playerId)
    {
        var lobby = _lobbies[lobbyId];
        if(lobby is not null)
        {
            lobby.RemovePlayer(playerId);
            if(lobby.GetAllPlayers().Count == 0)
            {
                _lobbies.Remove(lobbyId);
            }
        }

        var player = _playerService.GetPlayer(playerId);
        
        if(player is not null)
        {
            player.JoinedLobbyGuid = null;
        }
    }
}
