using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class ScoreRecordRepository : IRepository<ScoreRecord>
    {
        private readonly string _filename;
        private List<ScoreRecord> _playerScores;

        public ScoreRecordRepository(string filename)
        {
            _filename = filename;
        }

        public void Add(ScoreRecord source)
        {
            InvalidatePlayerList();
            _playerScores.Add(source);
        }

        public ICollection<ScoreRecord> GetAll()
        {
            InvalidatePlayerList();
            return _playerScores;
        }

        public void Load()
        {
            if (File.Exists(_filename))
            {
                string text = File.ReadAllText(_filename);
                _playerScores = JsonConvert.DeserializeObject<List<ScoreRecord>>(text);
                _playerScores.Sort(new ScoreRecordComparer());
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
                    _playerScores = new List<ScoreRecord>();
                }
            }
        }

        private class ScoreRecordComparer : IComparer<ScoreRecord>
        {
            public int Compare(ScoreRecord x, ScoreRecord y)
            {
                if (x != null && y != null && x.Points > y.Points)
                {
                    return 1;
                }

                if (x != null && y != null && x.Points < y.Points)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
