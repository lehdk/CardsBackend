namespace CardsServer.Entity.Game;

public class Player
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Username { get; set; }

    public Guid? JoinedLobbyGuid { get; set; }

    public Player(string username)
    {
        Username = username;
    }

    public Player() { }
}