using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Blob
{
    public GameObject TargetBlob;
    public Vector3 HomeBase;
    // Start is called before the first frame update
    protected override void Start()
    {
        HomeBase = GameObject.FindGameObjectWithTag("Destination").transform.position;
        base.Start();
    }
    public override void Spawn()
    {
        V.Soldiers.Add(gameObject);
        base.Spawn();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(V.Enemies.Count > 0)
        {
            TargetBlob = FindTarget(V.Enemies);
        }
        if (TargetBlob != null)
        {
            targetDist = Vector3.Distance(TargetBlob.transform.position, transform.position);
            if (targetDist < Mathf.Max(AttackRange, TargetBlob.GetComponent<Blob>().AttackRange))
            {
                Attack(TargetBlob);
            }
            agent.destination = TargetBlob.transform.position;
        }
        else
        {
            agent.destination = HomeBase;
        }
        base.Update();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == TargetBlob)
        {
            Attack(TargetBlob);
        }
    }
    public override GameObject FindTarget(List<GameObject> Things)
    {
        GameObject closest = base.FindTarget(Things);
        if (Things.Count == 0)
        {
            return null;
        }
        float BestPriority = Mathf.Infinity;
        if (closest != null)
        {
            BestPriority = Vector3.Distance(transform.position, closest.transform.position);
        }
        if (BestPriority < 10)
        {
            return closest;
        }
        try
        {
            if (Things.Count > 0)
            {
                foreach (GameObject go in Things)
                {
                    if (go != null)
                    {
                        float TargetPriority = Vector3.Distance(HomeBase, go.transform.position);
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
