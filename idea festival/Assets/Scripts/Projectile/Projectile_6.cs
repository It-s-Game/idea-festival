using System.Collections;
using UnityEngine;
public class Projectile_6 : Projectile
{
    [SerializeField]
    private Vector2 offset1;
    [SerializeField]
    private Vector2 offset2;
    [SerializeField]
    private Vector2 size1;
    [SerializeField]
    private Vector2 size2;

    protected override void OnEnable()
    {
        base.OnEnable();

        col.offset = offset1;
        col.size = size1;
    }
    protected override IEnumerator EnterCollide()
    {
        col.offset = offset2;
        col.size = size2;

        return base.EnterCollide();
    }
}