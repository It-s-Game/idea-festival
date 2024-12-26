using System.Collections.Generic;
using UnityEngine;
public class UIManager
{
    public Stack<GameObject> uiStack = new();

    public void ActiveUI(GameObject obj)
    {
        obj.SetActive(true);

        uiStack.Push(obj);
    }
    public void ActiveUI(string uiName)
    {
        GameObject go = GameObject.Find(uiName);

        go.SetActive(true);

        uiStack.Push(go);
    }
    public void DisableUI()
    {
        if(uiStack.TryPeek(out GameObject obj))
        {
            obj.SetActive(false);

            uiStack.Pop();
        }
    }
}