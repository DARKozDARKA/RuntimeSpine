using System.Collections;
using UnityEngine;

namespace CodeBase.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(IEnumerator enumerator);
    }
}