using UnityEngine;

namespace CodeBase.Services.CoroutineRunner
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(this);
        }

    }
}
