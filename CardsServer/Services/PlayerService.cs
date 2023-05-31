using CardsServer.Entity.Game;

namespace CardsServer.Services;

public class PlayerService
{
    private readonly ILogger<PlayerService> _logger;
    
    private readonly IList<Player> _players;

    public PlayerService(ILogger<PlayerService> logger)
    {
        _logger = logger;
        _players = new List<Player>();
    }

    public Player? GetPlayer(Guid playerId)
    {
        return _players.FirstOrDefault(p => p.Guid == playerId);
    }

    public Player? CreatePlayer(string username)
    {
        if(string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        var player = new Player(username);

        _players.Add(player);

        return player;
    }
}