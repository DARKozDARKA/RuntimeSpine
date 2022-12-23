using System;

namespace CodeBase.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadAsync(string sceneName, Action onLoaded);
        void LoadStraight(string sceneName);
    }
}