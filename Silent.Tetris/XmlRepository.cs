using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class XmlRepository : IRepository<Player>
    {
        private readonly string _filename;
        private List<Player> _playerScores;

        public XmlRepository(string filename)
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));

                using (StreamReader reader = File.OpenText(_filename))
                {
                    _playerScores = (List<Player>)serializer.Deserialize(reader);
                    _playerScores.Sort(new PlayerComparer());
                    _playerScores = _playerScores.Take(10).ToList();
                }
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));

            using (StreamWriter writer = new StreamWriter(File.Open(_filename, FileMode.Create)))
            {
                serializer.Serialize(writer, _playerScores);
            }
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
