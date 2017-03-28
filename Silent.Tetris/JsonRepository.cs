using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class JsonRepository : IRepository<Player>
    {
        private readonly string _filename;
        private List<Player> _playerScores;

        public JsonRepository(string filename)
        {
            _filename = filename;
        }

        public void Add(Player source)
        {
            InvalidatePlayerList();
            _playerScores.Add(source);
        }

        public ICollection<Player> GetAll()
        {
            InvalidatePlayerList();
            return _playerScores;
        }

        public void Load()
        {
            if (File.Exists(_filename))
            {
                string text = File.ReadAllText(_filename);
                _playerScores = JsonConvert.DeserializeObject<List<Player>>(text);
                _playerScores.Sort(new PlayerComparer());
                _playerScores = _playerScores.Take(10).ToList();
            }
        }

        public void Save()
        {
            string text = JsonConvert.SerializeObject(_playerScores);
            File.WriteAllText(_filename, text);
        }

        private void InvalidatePlayerList()
        {
            if (_playerScores == null)
            {
                Load();

                if (_playerScores == null)
                {
                    _playerScores = new List<Player>();
                }
            }
        }

        private class PlayerComparer : IComparer<Player>
        {
            public int Compare(Player x, Player y)
            {
                if (x.Score > y.Score)
                {
                    return 1;
                }

                if (x.Score < y.Score)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
