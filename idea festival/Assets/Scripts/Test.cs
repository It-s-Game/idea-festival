using UnityEngine;
public class Test : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Managers.Game.characterName.Add("Archer Hero");
            Managers.Game.characterName.Add("Bat Hero");

            Managers.Game.GameStart();
        }
    }
}