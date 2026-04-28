namespace GameLeaderboard;

public class RBTreeLeaderboard : ILeaderboard
{
    private readonly AugmentedRedBlackTree<Player> _tree;
    private readonly Dictionary<string, Player> _playerLookup = new();

    public int PlayerCount => _tree.Count;

    public RBTreeLeaderboard()
    {
        _tree = new AugmentedRedBlackTree<Player>(new PlayerComparer());
    }

    public void AddOrUpdatePlayer(Player player)
    {
        // Remove old version if updating
        if (_playerLookup.TryGetValue(player.SummonerName, out var oldPlayer))
        {
            _tree.Delete(oldPlayer);
        }

        _tree.Insert(player);
        _playerLookup[player.SummonerName] = player;
    }
    
    public void BulkAddPlayers(IEnumerable<Player> players)
    {
        var playerList = players.ToList();

        // Remove duplicates by name, keeping first occurrence
        var uniquePlayers = new List<Player>();
        var seen = new HashSet<string>();

        foreach (var player in playerList)
        {
            if (seen.Add(player.SummonerName))
            {
                uniquePlayers.Add(player);
            }
        }

        _tree.BulkInsert(uniquePlayers);

        // Build lookup dictionary
        _playerLookup.Clear();
        foreach (var player in uniquePlayers)
        {
            _playerLookup[player.SummonerName] = player;
        }
    }

    public int GetPlayerRank(string summonerName)
    {
        if (!_playerLookup.TryGetValue(summonerName, out var player))
            return -1;

        return _tree.GetRank(player);
    }
    
    public Player? GetPlayerAtRank(int rank)
    {
        return _tree.GetItemAtRank(rank);
    }
    
    public List<Player> GetTopPlayers(int count)
    {
        return _tree.InOrderTraversal().Take(Math.Min(count, PlayerCount)).ToList();
    }
    
    public List<Player> GetPlayersAroundRank(int rank, int context)
    {
        int start = Math.Max(1, rank - context);
        int end = Math.Min(PlayerCount, rank + context);

        return _tree.InOrderTraversal()
            .Skip(start - 1)
            .Take(end - start + 1)
            .ToList();
    }
    
    public void UpdatePlayerMMR(string summonerName, int newMMR)
    {
        if (!_playerLookup.TryGetValue(summonerName, out var player))
            return;

        _tree.Delete(player);
        player.UpdateMMR(newMMR);
        _tree.Insert(player);
    }

    public Dictionary<RankTier, int> GetRankDistribution()
    {
        var distribution = new Dictionary<RankTier, int>();
        foreach (RankTier tier in Enum.GetValues<RankTier>())
            distribution[tier] = 0;

        foreach (var player in _tree.InOrderTraversal())
            distribution[player.Tier]++;

        return distribution;
    }
}
