using System.Text.Json;

namespace MineSweeper
{
    internal class Database
    {
        private readonly string path;
        private class PlayerData
        {
            public string name { get; set; }
            public int wins { get; set; } = 0;
            public int loses { get; set; } = 0;
        }
        private record Root(List<PlayerData> players);
        private Root root;
        public Database()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MineSweeper by H1K0");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, "db.json");
            if (!File.Exists(path))
                File.WriteAllText(path, "{\"players\":[]}");
            root = JsonSerializer.Deserialize<Root>(File.ReadAllText(path));
        }
        public int PlayersCount() { return root.players.Count; }
        public int FindPlayer(string name) { return root.players.FindIndex(p => p.name == name); }
        public void AddPlayer(string name)
        {
            PlayerData newplayer = new();
            newplayer.name = name;
            root.players.Add(newplayer);
        }
        public int[] Stats(int playerId) { return new int[] { root.players[playerId].wins, root.players[playerId].loses }; }
        public void AddGame(int playerId, bool win)
        {
            if (win)
                root.players[playerId].wins++;
            else
                root.players[playerId].loses++;
        }
        public void Update() { File.WriteAllText(path, JsonSerializer.Serialize(root)); }
    }
}
