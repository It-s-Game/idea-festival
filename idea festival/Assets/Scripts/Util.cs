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
}