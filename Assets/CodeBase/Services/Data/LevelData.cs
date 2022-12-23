using UnityEngine;

namespace CodeBase.Services.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string SceneName;

        public Vector3 SpawnPoint;
    }
}