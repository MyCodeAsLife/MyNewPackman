using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines _instance;

    private static Coroutines Instance
    {
        get
        {
            if (_instance == null)      // Проверям на наличие объекта (компонента)
            {
                var go = new GameObject("[COROUTINE MANAGER]"); // Если объекта нет то создаем его
                _instance = go.AddComponent<Coroutines>();      // Добавляем на объект себя (компонент)
                DontDestroyOnLoad(go);                          // Запрещаем уничтожать объект при смене сцен
            }

            return _instance;
        }
    }

    public static Coroutine StartRoutine(IEnumerator routine)
    {
        return Instance.StartCoroutine(routine);
    }

    public static void StopRoutine(IEnumerator routine)
    {
        Instance.StopCoroutine(routine);
    }
}