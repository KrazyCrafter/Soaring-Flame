using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    bool Stuck;
    protected override void OnCollisionEnter(Collision collision)
    {
        if (!Stuck)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Soldier")
            {
                bool Worked = false;
                int Counter = 0;
                GameObject Obj = collision.gameObject;
                while (!Worked && Counter < 5)
                {
                    try
                    {
                        Obj.GetComponent<Enemy>().TakeDamage(Dmg, Type);
                        Worked = true;
                        if (Counter > 0)
                        {
                            Debug.Log($"Did {Dmg} Damage to {Obj.name} after {Counter} Cycles");
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Obj = Obj.transform.parent.gameObject;
                        Counter++;
                        Debug.Log("DamageCycling, counter = " + Counter);
                    }
                }
            }
            Destroy(gameObject, 5);
            Stuck = true;
        }
    }
}
