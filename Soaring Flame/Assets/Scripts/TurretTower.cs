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
            Counter++;
        }
    }
}
