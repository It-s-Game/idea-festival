using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unit", menuName ="new Unit")]
[System.Serializable]
public class UnitSO : ScriptableObject
{
    public HeroSO[] heroes;
}
