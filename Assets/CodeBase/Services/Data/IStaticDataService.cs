using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public interface IStaticDataService
    {
        void Load();
        T LoadResource<T>(string path) where T : Object;
        T[] LoadResources<T>(string path) where T : Object;
        Dictionary<string, LevelData> GetLevels();
    }
}