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
    public Quaternion lastPos;
    public float turnSpeed = 20f;
    public float heightMod;
    public AudioSource FireSound;
    // Start is called before the first frame update
    void Start()
    {
        //Target = getClosestThing(V.Enemies).transform; <- Need to set up a list of enemies
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            if (Target == null)
            {
                targetDist = Mathf.Infinity;
                //Target = getClosestThing(V.Enemies).transform; <- Need to set up a list of enemies
            }
            else
            {
                targetDist = Vector3.Distance(transform.position, Target.transform.position);
                if (targetDist <= Range)
                {
                    Attack();
                }
                //Target = getClosestThing(V.Enemies).transform; <- Need to set up a list of enemies
            }
        }
        if (Target != null)
        {
            TargetPos = Target.position - transform.position;
        }
        lastPos = Quaternion.LookRotation(-TargetPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * turnSpeed);
    }
    public GameObject getClosestThing(List<GameObject> Things)
    {
        GameObject closest = null;
        if (Things.Count > 0)
        {
            float closestDistance = Mathf.Infinity;
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
        return closest;
    }
    void Attack()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * turnSpeed);
        timer = 0;
        FireSound.Play();

        Instantiate(Projectile, AttackSpot.position, AttackSpot.rotation);
    }
}
