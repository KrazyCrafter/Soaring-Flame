using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blob : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public int Price;
    public float PhysicalDamageRes; //Res is in a percent reduction
    public float MagicDamageRes; //Res is in a percent reduction
    public AudioSource[] VoiceLines;
    protected float talktimer;
    public float Talktime;
    public AudioSource[] DeathLines;
    public float Dmg;
    public string DamageType;
    public float AttackSpeed;
    protected float AttackTimer;
    public float AttackRange;
    public RectTransform BarTrans;
    protected NavMeshAgent agent;
    public float targetDist;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Spawn();
        if(V.Level == "Mountain")
        {
            agent.speed *= .75f;
        }
    }
    public virtual void Spawn()
    {
        if (HP == 0)
        {
            HP = MaxHP;
        }
        targetDist = Mathf.Infinity;
        Talktime = 30;
        talktimer = 30;
    }
    protected virtual void Update()
    {
        talktimer += Time.deltaTime;
        if (talktimer >= Talktime)
        {
            talktimer = 0;
            int RNG = UnityEngine.Random.Range(0, 10);
            if (RNG == 0 && VoiceLines.Length > 0)
            {
                Talk();
            }
        }
        if(AttackTimer <= AttackSpeed)
        {
            AttackTimer += Time.deltaTime;
        }
    }
    public virtual void Attack(GameObject Target)
    {
        if(Target == null)
        {
            return;
        }
        if(AttackTimer >= AttackSpeed)
        {
            AttackTimer = 0;
            //Play attack animation(Needs to be added)
            try
            {
                Target.GetComponent<Blob>().TakeDamage(Dmg, DamageType);
            }
            catch (NullReferenceException)
            {
                Debug.Log("Non-Blob entity getting attacked");
            }
        }
    }
    public void Talk()
    {
        int RNG = UnityEngine.Random.Range(0, V.Enemies.Count/2);
        if (RNG == 0)
        {
            int selection = UnityEngine.Random.Range(0, VoiceLines.Length);
            VoiceLines[selection].Play();
        }
    }
    public virtual void TakeDamage(float Dmg, string DmgType)
    {
        if (DmgType == "Physical")
        {
            Dmg -= Dmg * PhysicalDamageRes;
        }
        else if (DmgType == "Magical")
        {
            Dmg -= Dmg * MagicDamageRes;
        }
        HP -= Dmg;
        if (HP < 0)
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
        if (DeathLines.Length > 0)
        {
            int RNG = UnityEngine.Random.Range(0, DeathLines.Length);
            DeathLines[RNG].Play();
        }
        Destroy(gameObject);
    }
    public virtual GameObject FindTarget(List<GameObject> Things)
    {
        GameObject closest = null;
        if (Things.Count == 0)
        {
            return closest;
        }
        float BestPriority = Mathf.Infinity;
        try
        {
            if (Things.Count > 0)
            {
                foreach (GameObject go in Things)
                {
                    if (go != null)
                    {
                        float TargetPriority = Vector3.Distance(transform.position, go.transform.position);
                        if (TargetPriority < BestPriority)
                        {
                            BestPriority = TargetPriority;
                            closest = go;
                        }
                    }
                }
            }
            targetDist = BestPriority;
            return closest;
        }
        catch (NullReferenceException)
        {
            Debug.Log(gameObject.name + " Is tracking dead entity");
            return closest;
        }
    }
}
