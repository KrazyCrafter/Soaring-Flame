using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public int PriorityMultiplier;
    public int Price;
    public float PhysicalDamageRes; //Res needs to be between 0 and 1(maybe -1 if we want to have increased damage)
    public float MagicDamageRes;//Res needs to be between 0 and 1(maybe -1 if we want to have increased damage)
    public AudioSource[] VoiceLines;
    public GameObject EnemyBase;
    public float Dmg;
    public RectTransform BarTrans;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        EnemyBase = GameObject.FindGameObjectWithTag("Gate");
        agent = GetComponent<NavMeshAgent>();
        Spawn();
    }
    void Spawn() // Was thinking about setting up Object pooling for the enemies
    {
        agent.destination = EnemyBase.transform.position;
        HP = MaxHP;
        //V.Enemies.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float Dmg, string DmgType)
    {
        if(DmgType == "Physical")
        {
            Dmg -= Dmg * PhysicalDamageRes;
        }
        else if(DmgType == "Magical")
        {
            Dmg -= Dmg * MagicDamageRes;
        }
        HP -= Dmg;
        if(HP < 0)
        {
            Die();
        }
        else 
        {
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
            BarTrans.localScale = new Vector3(HP / MaxHP, 1, 1);
        }
    }
    public void Die()// Want to add in object pooling here later
    {
        Destroy(gameObject);
    }
}
