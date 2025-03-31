using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower
{
    protected override void Start()
    {
        V.TurretCount++;
        base.Start();
    }
    public int Counter;
    protected override void IdleRotating()
    {
        if (Counter > 20)
        {
            base.IdleRotating();
        }
        else
        {
            Target = FindTarget(V.Enemies);
            if (Target == null)
            {
                targetDist = Mathf.Infinity;
            }
            else
            {
                if (Doing == States.Attacking)
                {
                    TargetPos = Target.position;
                }
                targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
            }
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
    public override float TargetValue(GameObject Target)
    {
        if (Target.GetComponent<Enemy>().PhysicalDamageRes > 1)
        {
            return Mathf.Infinity;
        }
        else if(Vector3.Distance(transform.position, Target.transform.position) > Range)
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier * 10;
        }
        else
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier;
        }
    }
}
