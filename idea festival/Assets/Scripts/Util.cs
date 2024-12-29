using System.Collections.Generic;
using UnityEngine;
public static class Util
{
    private static MonoScript monoScript = null;

    public static MonoBehaviour GetMonoBehaviour()
    {
        if (monoScript == null)
        {
            GameObject go = new GameObject("@MonoScript");

            monoScript = go.AddComponent<MonoScript>();

            Object.DontDestroyOnLoad(go);
        }

        return monoScript;
    }
    public static int[] GetRandomValues(int maxValue, int count)
    {
        List<int> valueList = new();
        int[] result = new int[count];

        for (int i = 0; i < maxValue; i++)
        {
            valueList.Add(i);
        }

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, maxValue - i);

            result[i] = valueList[index];

            valueList.RemoveAt(index);
        }

        return result;
    }
}