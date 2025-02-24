using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    public GameObject partic;
    protected override void Attack()
    {
        timer = 0;
        //FireSound.Play();
        AttackSpot = Target.transform;
        GameObject Flame = Instantiate(Projectile, AttackSpot.position, Quaternion.identity) as GameObject;
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
    protected override void IdleRotating()
    {
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
