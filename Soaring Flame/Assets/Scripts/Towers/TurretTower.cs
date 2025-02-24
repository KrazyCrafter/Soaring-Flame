using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower
{
    public int Counter;
    protected override void IdleRotating()
    {
        if (Counter > 20)
        {
            base.IdleRotating();
        }
        else
        {
            Charged = true;
            if (targetDist > Range * 1.5f)
            {
                Counter++;
            }
            else
            {
                Doing = States.Attacking;
            }
        }
    }
}
