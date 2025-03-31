using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public float PriorityMultiplier;
    public int Price;
    public float PhysicalDamageRes; //Res is in a percent reduction
    public float MagicDamageRes; //Res is in a percent reduction
    public AudioSource[] VoiceLines;
    protected float timer;
    public float Talktime;
    public AudioSource[] DeathLines;
    public GameObject EnemyBase;
    public float Dmg;
    public RectTransform BarTrans;
    protected NavMeshAgent agent;
    public EnemySpawning HomeBase;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        EnemyBase = GameObject.FindGameObjectWithTag("Destination");
        agent = GetComponent<NavMeshAgent>();
        HomeBase = GameObject.FindWithTag("EnemySpawn").GetComponent<EnemySpawning>();
        Spawn();
    }
    public virtual void Spawn() // Was thinking about setting up Object pooling for the enemies
    {
        HomeBase.MaxEnemies++;
        agent.destination = EnemyBase.transform.position;
        if (HP == 0)
        {
            HP = MaxHP;
        }
        V.Enemies.Add(gameObject);
        Talktime = 30;
        timer = 30;
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Talktime)
        {
            timer = 0;
            int RNG = Random.Range(0, 10);
            if(RNG == 0 && VoiceLines.Length > 0)
            {
                Talk();
            }
        }
    }
    public void Talk()
    {
        int selection = Random.Range(0, VoiceLines.Length);
        VoiceLines[selection].Play();
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
    public virtual void Die() // Want to add in object pooling here later
    {
        V.Enemies.Remove(gameObject);
        if (DeathLines.Length > 0)
        {
            int RNG = Random.Range(0, DeathLines.Length);
            DeathLines[RNG].Play();
        }
        Destroy(gameObject);
    }
}
