using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Soldier
{
    public bool Attacking;
    public Transform SpawnSpot;
    public GameObject Arrow;
    public override void Spawn()
    {
        Attacking = true;
        base.Spawn();
    }
    public override void ApproachBlob()
    {
        targetDist = Vector3.Distance(TargetBlob.transform.position, transform.position);
        if (Attacking)
        {
            if (targetDist < Mathf.Max(AttackRange, TargetBlob.GetComponent<Blob>().AttackRange) + 2.5f)
            {
                agent.destination = HomeBase;
                Attacking = false;
            }
            else
            {
                if (targetDist < Mathf.Max(AttackRange, TargetBlob.GetComponent<Blob>().AttackRange) + 5)
                {
                    agent.destination = transform.position;
                    Attack(TargetBlob);
                }
                else
                {
                    agent.destination = TargetBlob.transform.position;
                    if (targetDist > 10)
                    {
                        HealTimer += Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            if (targetDist > Mathf.Max(AttackRange, TargetBlob.GetComponent<Blob>().AttackRange) + 7.5)
            {
                agent.destination = TargetBlob.transform.position;
                Attacking = true;
            }
        }
    }
    public override void Attack(GameObject Target)
    {
        Vector3 TargetPos = Target.transform.position;
        try
        {
            TargetPos.y += Target.GetComponent<NavMeshAgent>().height;
        }
        catch (MissingReferenceException)
        {
            TargetPos.y += 1;
        }
        Quaternion lastPos = Quaternion.LookRotation(TargetPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * 30);
        SpawnSpot.transform.rotation = Quaternion.Slerp(SpawnSpot.transform.rotation, lastPos, Time.deltaTime * 30);
        //lastPos.z += 90;
        if (AttackTimer >= AttackSpeed)
        {
            GameObject Shot = Instantiate(Arrow, SpawnSpot.position, lastPos);
            Shot.GetComponent<Projectile>().Spawn(Dmg, 100);
            AttackTimer = 0;
        }
    }
}
