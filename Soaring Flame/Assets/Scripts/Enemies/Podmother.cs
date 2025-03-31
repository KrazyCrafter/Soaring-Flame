using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podmother : Enemy
{
    public GameObject Pod;

    public override void Die() // Want to add in object pooling here later
    {
        V.Enemies.Remove(gameObject);
        GameObject Summon = Instantiate(Pod, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
