using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Dmg;
    public string Type;
    protected Rigidbody Rb;
    public virtual void Spawn(float damage, float force)
    {
        Dmg = damage;
        Rb = gameObject.GetComponent<Rigidbody>();
        Rb.AddForce(force * transform.forward);
        Destroy(gameObject, 10);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Soldier")
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
        Destroy(gameObject);
    }
}
