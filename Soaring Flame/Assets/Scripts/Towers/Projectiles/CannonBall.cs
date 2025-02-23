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
        foreach (GameObject go in V.Enemies)
        {
            float Dist = Vector3.Distance(go.transform.position, transform.position);
            if (Dist < ExplodeRadius)
            {
                float Multi = Dist / ExplodeRadius;
                go.GetComponent<Enemy>().TakeDamage(Dmg*Multi / 2, Type);
            }
        }
        Destroy(gameObject);
    }
}
