using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podmother : Enemy
{
    public GameObject Pod;

    public override void Die() // Want to add in object pooling here later
    {
        V.Enemies.Remove(gameObject);
        Vector3 SpawnSpot = transform.position;
        SpawnSpot.y += 1.7f;
        GameObject Summon = Instantiate(Pod, SpawnSpot, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
