using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod : Enemy
{
    public int SpawnTime;
    public GameObject Podling;
    // Start is called before the first frame update
    protected override void Start()
    {
        Spawn();
    }
    public override void Spawn() // Was thinking about setting up Object pooling for the enemies
    {
        HP = MaxHP;
        MagicDamageRes = 1;
        V.Enemies.Add(gameObject);
    }
    protected override void Update()
    {
        talktimer += Time.deltaTime;
        if(talktimer >= SpawnTime)
        {
            talktimer = 0;
            Instantiate(Podling, transform.position, transform.rotation);
            if(Price > 0)
            {
                Price--;
                SpawnTime++;
            }
            else
            {
                Price = 0;
            }
        }
    }
}
