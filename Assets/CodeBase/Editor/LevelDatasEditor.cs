using CodeBase.Services.Data;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelData))]
    public sealed class LevelDatasEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelData levelData = (LevelData)target;

            if (GUILayout.Button("Collect"))
                CollectData(levelData);


            EditorUtility.SetDirty(target);
        }

        private void CollectData(LevelData levelData)
        {
            levelData.SceneName = SceneManager.GetActiveScene().name;
            levelData.SpawnPoint = GameObject.FindWithTag(Tags.SpawnPoint).transform.position;
        }
    }
}