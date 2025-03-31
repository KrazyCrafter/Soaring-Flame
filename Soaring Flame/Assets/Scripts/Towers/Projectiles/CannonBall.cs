using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Projectile
{
    public float ExplodeRadius;
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
        List<GameObject> Colateral = new List<GameObject>();
        foreach (GameObject go in V.Enemies)
        {
            float Dist = Vector3.Distance(go.transform.position, transform.position);
            if (Dist < ExplodeRadius)
            {
                Colateral.Add(go);
            }
        }
        for(int i = 0; i < Colateral.Count; i++)
        {
            float Multi = Vector3.Distance(Colateral[i].transform.position, transform.position) / (3 * ExplodeRadius);
            Colateral[i].GetComponent<Enemy>().TakeDamage(Dmg * Multi, "AP");
        }
        Destroy(gameObject);
    }
}
