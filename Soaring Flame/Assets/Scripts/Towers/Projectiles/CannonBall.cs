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
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Dmg, Type);
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
            float Multi = Vector3.Distance(Colateral[i].transform.position, transform.position) / (2 * ExplodeRadius);
            Colateral[i].GetComponent<Enemy>().TakeDamage(Dmg * Multi, Type);
        }
        Destroy(gameObject);
    }
}
