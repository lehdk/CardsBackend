namespace CardsServer.Entity.Game;

public enum LobbyStatus
{
    Open = 0,
    Locked = 1,
    Started = 2
}

public class GameLobby
{
    public Guid Guid { get; set; }

    public List<Player> players { get; set; }

    public LobbyStatus Status { get; set; }

    public GameLobby()
    {
        Guid = Guid.NewGuid();
        players = new List<Player>();
        Status = LobbyStatus.Open;
    }

    public void AddPlayer(Player player)
    {
        if (players.FindIndex(p => p.Guid == player.Guid) == -1)
            players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    public void RemovePlayer(Guid playerGuid)
    {
        var player = players.FindLast(p => p.Guid == playerGuid);
        if (player is null) return;

        RemovePlayer(player);
    }

    public List<Player> GetAllPlayers()
    {
        return players;
    }
}