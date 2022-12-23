using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Data
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelData> _levels = new();

        public void Load()
        {
            _levels = LoadResources<LevelData>(StaticDataPath.LevelData)
                .ToDictionary(_ => _.SceneName, _ => _);
        }

        public T LoadResource<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        public T[] LoadResources<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);

        public Dictionary<string, LevelData> GetLevels() =>
            _levels;
        
    }
}