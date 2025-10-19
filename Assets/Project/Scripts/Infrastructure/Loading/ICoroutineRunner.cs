using System.Collections;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Loading
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator load);
    }
}