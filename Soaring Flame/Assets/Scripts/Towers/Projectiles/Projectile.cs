using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Dmg;
    public string Type;
    protected Rigidbody Rb;
    public void Spawn(float damage, float force)
    {
        Dmg = damage;
        Rb = gameObject.GetComponent<Rigidbody>();
        Rb.AddForce(force * transform.forward);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Dmg, Type);
        }
        Destroy(gameObject);
    }
}
