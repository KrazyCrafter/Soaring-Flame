using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    public GameObject partic;

    protected override void Start()
    {
        V.MageCount++;
        base.Start();
<<<<<<< Updated upstream
        if(V.Level == "Mountain")
        {
            Damage *= .75f;
=======
        if(V.ActiveScene == "MountainScene")
        {
            Damage -= Damage * .15f;
>>>>>>> Stashed changes
        }
    }
    protected override void Attack()
    {
        timer = 0;
        //FireSound.Play();
        AttackSpot = Target.transform;
        GameObject Flame = Instantiate(Projectile, AttackSpot.position, Quaternion.identity) as GameObject;
        Flame.transform.Rotate(-90, 0, 0);
        Flame.GetComponent<MagicFlame>().Spawn(Damage);
        Vector3 from = Weapon.transform.position;
        Vector3 to = AttackSpot.transform.position;
        Vector3 direction = to - from;
        Physics.Raycast(Weapon.transform.position, direction, Range);
        float step = 1f / 10;
        float currentStep = 0;
        List<Object> clones = new List<Object>();
        for (int i = 0; i < 10; ++i)
        {
            currentStep += step;
            Vector3 position = Vector3.Lerp(Weapon.transform.position, AttackSpot.transform.position, currentStep);
            // Create object at position
            clones.Add(Object.Instantiate(partic, position, new Quaternion()));
        }
        for (int i = 0; i < clones.Count; i++)
        {
            Destroy(clones[i], 0.5F);
        }
    }
    protected override void Turning()
    {
        Weapon.transform.Rotate(timer * transform.up * Time.deltaTime);
    }
    public override float TargetValue(GameObject Target)
    {
        if (Target.GetComponent<Enemy>().MagicDamageRes > 1)
        {
            return Mathf.Infinity;
        }
        else if (Vector3.Distance(transform.position, Target.transform.position) > Range)
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier * 10;
        }
        else
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier;
        }
    }
    protected override void IdleRotating()
    {
        Target = FindTarget(V.Enemies);
        if (Target == null)
        {
            targetDist = Mathf.Infinity;
        }
        else
        {
            targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
        }
        if (targetDist > Range * 1.5f)
        {
            Weapon.transform.Rotate(timer / 2 * transform.up * Time.deltaTime);
        }
        else
        {
            Doing = States.Attacking;
        }
    }
}
