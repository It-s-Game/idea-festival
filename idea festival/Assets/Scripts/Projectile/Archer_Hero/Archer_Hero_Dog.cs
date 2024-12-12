using System.Collections;
public class Archer_Hero_Dog : Projectile
{
    protected override void Move()
    {
        
    }
    protected override IEnumerator Moving()
    {
        //yield return new WaitForSeconds(0);

        yield return base.Moving();
    }
}