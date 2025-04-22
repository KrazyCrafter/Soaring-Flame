using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform Target;
    public GameObject Projectile;
    public Transform AttackSpot;
    public float Range = 20;
    public float attackTimer = .25f;
    public float timer;
    public float targetDist;
    public Vector3 TargetPos;
    protected Quaternion lastPos;
    public GameObject Weapon;
    public float turnSpeed = 20f;
    public AudioSource FireSound;
    public float Damage;
    public enum States { Idle, Attacking};
    public States Doing = States.Idle;
    public bool Charged;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        V.Towers.Add(gameObject);
        Target = FindTarget(V.Enemies);
        if(Target == null)
        {
            targetDist = Mathf.Infinity;
        }
        else
        {
            if (Doing == States.Attacking)
            {
                TargetPos = Target.position;
            }
            targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Target == null)
        {
            Target = FindTarget(V.Enemies);
            if (Target == null)
            {
                targetDist = Mathf.Infinity;
                Doing = States.Idle;
            }
            else
            {
                if(Doing == States.Attacking)
                {
                    TargetPos = Target.position;
                }
                targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
            }
        }
        if (Doing == States.Attacking && Charged)
        {
            Charged = false;
            timer = attackTimer;
        }
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            if (Doing == States.Attacking)
            {
                TargetPos = Target.position;
                targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
                if (targetDist < Range)
                {
                    Attack();
                }
                Target = FindTarget(V.Enemies);
                if (Target == null)
                {
                    targetDist = Mathf.Infinity;
                }
                else
                {
                    if (Doing == States.Attacking)
                    {
                        TargetPos = Target.position;
                    }
                    targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
                }
                if(targetDist > Range * 1.5f)
                {
                    Doing = States.Idle;
                }
            }
            else if (Doing == States.Idle)
            {
                IdleRotating();
            }
        }
        if (Doing == States.Attacking)
        {
            if (Target != null)
            {
                TargetPos = Target.position - Weapon.transform.position;
                targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
                if (targetDist > Range * 1.5f)
                {
                    Doing = States.Idle;
                }
            }
        }
        Turning();
    }
    public Transform FindTarget(List<GameObject> Things)
    {
        try
        {
            if (Things.Count == 0)
            {
                return null;
            }
            GameObject closest = null;
            float BestPriority = Mathf.Infinity;
            if (Things.Count > 0)
            {
                foreach (GameObject go in Things)
                {
                    if (go != null)
                    {
                        float TargetPriority;
                        TargetPriority = TargetValue(go);
                        if (TargetPriority < BestPriority)
                        {
                            BestPriority = TargetPriority;
                            closest = go;
                        }
                    }
                }
            }
            targetDist = Vector3.Distance(transform.position, closest.transform.position);
            if (targetDist > Range * 1.5f)
            {
                Doing = States.Idle;
            }
            else
            {
                Doing = States.Attacking;
            }
            return closest.transform;
        }
        catch (NullReferenceException)
        {
            Debug.Log("Tracking dead enemy");
            if (V.Enemies[0] == null)
            {
                V.Enemies.Remove(V.Enemies[0]);
            }
            return null;
        }
    }
    public virtual float TargetValue(GameObject Target)
    {
        if(Vector3.Distance(transform.position, Target.transform.position) > Range)
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier * 10;
        }
        else
        {
            return Vector3.Distance(transform.position, Target.transform.position) / Target.GetComponent<Enemy>().PriorityMultiplier;
        }
    }
    protected virtual void Attack()
    {
        Weapon.transform.rotation = Quaternion.Slerp(Weapon.transform.rotation, lastPos, Time.deltaTime * turnSpeed);
        timer = 0;
        //FireSound.Play();

        GameObject Bullet = Instantiate(Projectile, AttackSpot.position, AttackSpot.rotation);
        Bullet.GetComponent<Projectile>().Spawn(Damage, Damage*10);
        Target = FindTarget(V.Enemies);
    }
    protected virtual void IdleRotating()
    {
        Target = FindTarget(V.Enemies);
        if (Target == null)
        {
            targetDist = Mathf.Infinity;
        }
        else
        {
            if (Doing == States.Attacking)
            {
                TargetPos = Target.position;
            }
            targetDist = Vector3.Distance(TargetPos, Weapon.transform.position);
        }
        if (targetDist > Range * 1.5f)
        {
            TargetPos = transform.position;
            TargetPos.x += UnityEngine.Random.Range(-Range, Range);
            TargetPos.z += UnityEngine.Random.Range(-Range, Range);
            timer = 0;
            Charged = true;
        }
        else
        {
            Doing = States.Attacking;
        }
    }
    protected virtual void Turning()
    {
        lastPos = Quaternion.LookRotation(TargetPos);
        Weapon.transform.rotation = Quaternion.Slerp(Weapon.transform.rotation, lastPos, Time.deltaTime * turnSpeed);
    }
}
