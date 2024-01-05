using UnityEngine;

public class FireBallPRJ : Projectile
{
    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        return true;
    }

    public override Projectile SetInfo(Creature owner, float damage)
    {
        base.SetInfo(owner, damage);

        return this;
    }
}
