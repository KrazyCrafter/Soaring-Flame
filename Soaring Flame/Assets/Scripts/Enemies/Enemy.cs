using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : Blob
{
    public float PriorityMultiplier;
    public GameObject EnemyBase;
    public EnemySpawning HomeBase;
    public GameObject TargetSoldier;
    // Start is called before the first frame update
    protected override void Start()
    {
        EnemyBase = GameObject.FindGameObjectWithTag("Destination");
        HomeBase = GameObject.FindWithTag("EnemySpawn").GetComponent<EnemySpawning>();
        base.Start();
    }
    public override void Spawn() // Was thinking about setting up Object pooling for the enemies
    {
        HomeBase.MaxEnemies++;
        agent.destination = EnemyBase.transform.position;
        V.Enemies.Add(gameObject);
        base.Spawn();
    }
    protected override void Update()
    {
        if (V.Soldiers.Count > 0)
        {
            TargetSoldier = FindTarget(V.Soldiers);
        }
        if (TargetSoldier != null)
        {
            targetDist = Vector3.Distance(TargetSoldier.transform.position, transform.position);
            if (targetDist < Mathf.Max(AttackRange, TargetSoldier.GetComponent<Blob>().AttackRange))
            {
                Attack(TargetSoldier);
            }
            else if (targetDist < 5)
            {
                agent.destination = TargetSoldier.transform.position;
            }
            else
            {
                agent.destination = EnemyBase.transform.position;
            }
        }
        else
        {
            agent.destination = EnemyBase.transform.position;
        }

        base.Update();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == EnemyBase)
        {
            Attack(EnemyBase);
        }

    }
    public override void Attack(GameObject Target)
    {
        if (Target == EnemyBase)
        {
            V.BaseHealth -= Dmg / AttackSpeed;
            if (V.BaseHealth < 0)
            {
                SceneManager.LoadScene("MenuScene");
            }
            V.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        else
        {
            base.Attack(Target);
        }
    }
    public override void Die() // Want to add in object pooling here later
    {
        V.Enemies.Remove(gameObject);
        V.Coins += Price / 2;
        base.Die();
    }
}
