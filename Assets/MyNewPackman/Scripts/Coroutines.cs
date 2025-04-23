using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines _instance;

    private static Coroutines Instance
    {
        get
        {
            if (_instance == null)      // �������� �� ������� ������� (����������)
            {
                var go = new GameObject("[COROUTINE MANAGER]"); // ���� ������� ��� �� ������� ���
                _instance = go.AddComponent<Coroutines>();      // ��������� �� ������ ���� (���������)
                DontDestroyOnLoad(go);                          // ��������� ���������� ������ ��� ����� ����
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