using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlame : Projectile
{
    public void Spawn(float damage)
    {
        Dmg = damage;
        Destroy(gameObject, 10);
    }
    protected void OnTriggerEnter(Collider collision)
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
                    Obj.GetComponent<Blob>().TakeDamage(Dmg, Type);
                    Worked = true;
                    if(Counter > 0)
                    {
                        Debug.Log($"Did {Dmg} Damage to {Obj.name} after {Counter} Cycles");
                    }
                }
                catch(System.NullReferenceException)
                {
                    Obj = Obj.transform.parent.gameObject;
                    Counter++;
                    Debug.Log("DamageCycling, counter = "+Counter);
                }
            }
        }
    }
}
