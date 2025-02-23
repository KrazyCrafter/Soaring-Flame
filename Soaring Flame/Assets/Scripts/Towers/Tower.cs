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
    protected float timer;
    protected float targetDist;
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
    void Start()
    {
        Target = getClosestThing(V.Enemies);
        if(Target == null)
        {
            targetDist = Mathf.Infinity;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Target == null)
        {
            Target = getClosestThing(V.Enemies);
            if (Target == null)
            {
                targetDist = Mathf.Infinity;
            }
        }
        if (Doing == States.Attacking && Charged)
        {
            timer = attackTimer;
        }
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            if (Doing == States.Attacking)
            {
                if(targetDist <= Range)
                {
                    Attack();
                }
                else if(targetDist > Range * 1.5f)
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
            }
        }
        Turning();
    }
    public Transform getClosestThing(List<GameObject> Things)
    {
        if(Things.Count == 0)
        {
            return null;
        }
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        if (Things.Count > 0)
        {
            foreach (GameObject go in Things)
            {
                if (go != null)
                {
                    float currentDistance;
                    currentDistance = Vector3.Distance(transform.position, go.transform.position) / go.GetComponent<Enemy>().PriorityMultiplier;
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        closest = go;
                    }
                }
            }
        }
        if(closestDistance > Range * 1.5f)
        {
            Doing = States.Idle;
        }
        else
        {
            Doing = States.Attacking;
        }
        return closest.transform;
    }
    protected void Attack()
    {
        Weapon.transform.rotation = Quaternion.Slerp(Weapon.transform.rotation, lastPos, Time.deltaTime * turnSpeed);
        timer = 0;
        //FireSound.Play();

        GameObject Bullet = Instantiate(Projectile, AttackSpot.position, AttackSpot.rotation);
        Bullet.GetComponent<Projectile>().Spawn(Damage, Damage*10);
        Target = getClosestThing(V.Enemies);
    }
    protected virtual void IdleRotating()
    {
        if (targetDist > Range * 1.5f)
        {
            TargetPos = transform.position;
            float RNG = Random.Range(Range / 2, Range);
            int Decider = Random.Range(0, 2);
            if(Decider == 0)
            {
                RNG *= -1;
            }
            TargetPos.x += RNG;
            TargetPos.y += Weapon.transform.position.y;
            RNG = Random.Range(Range / 2, Range);
            Decider = Random.Range(0, 2);
            if (Decider == 0)
            {
                RNG *= -1;
            }
            TargetPos.z += RNG;
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
