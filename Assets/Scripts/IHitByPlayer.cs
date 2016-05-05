using UnityEngine;
using System.Collections;

// created by Ho Hoang Tung
public interface IHitByPlayer
{
    void Hit(Enemy enemy);
}

public class GoompaHitByPlayer : IHitByPlayer
{
    public void Hit(Enemy enemy)
    {
        if (enemy._aniamtor.GetCurrentAnimatorStateInfo(0).IsName("GoompaNormal") == false)
            return;
        // Hướng từ trên xuống, goompa chết.
        enemy._aniamtor.SetInteger("status", 1);
        enemy.SetSpeed(Vector3.zero);
    }
}


public class TroopaHitByPlayer : IHitByPlayer
{
    public void Hit(Enemy enemy)
    {
        switch (enemy._aniamtor.GetInteger("status"))
        {
            case (int)Troopa.eStatus.Normal:
                enemy._aniamtor.SetInteger("status", (int)Troopa.eStatus.Shell);
                enemy.SetSpeed(Vector3.zero);
                break;
            //case (int)Troopa.eStatus.Shell:
            //    enemy._aniamtor.SetInteger("status", (int)Troopa.eStatus.SpeedShell);
            //    enemy.SetSpeed(new Vector3(0.1f, 0f, 0f));
            //    break;
            case (int)Troopa.eStatus.SpeedShell:
                enemy._aniamtor.SetInteger("status", (int)Troopa.eStatus.Shell);
                enemy.SetSpeed(Vector3.zero);
                break;
        }
            
    }
}

