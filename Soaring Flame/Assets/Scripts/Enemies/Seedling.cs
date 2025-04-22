using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Seedling : Enemy
{
    public int GrowthTime;
    public float GrowthTimer;
    public GameObject Podmother;
    public override void Spawn() // Was thinking about setting up Object pooling for the enemies
    {
        try
        {
            agent.destination = EnemyBase.transform.position;
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            Die();
        }
        HP = MaxHP;
        V.Enemies.Add(gameObject);
        Talktime = 30;
        talktimer = 30;
    }
    // Update is called once per frame
    protected override void Update()
    {
        GrowthTimer += Time.deltaTime;
        if(GrowthTimer > GrowthTime)
        {
            GameObject NewForm = Instantiate(Podmother, transform.position, transform.rotation) as GameObject;
            NewForm.GetComponent<Enemy>().HP = HP / MaxHP * NewForm.GetComponent<Enemy>().MaxHP;
            Destroy(gameObject);
        }
    }
}
