using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Heretics;
    public GameObject[] Monsters;
    public Transform SpawnSpot;
    public int[] WaveBudgets;
    public int Coins;
    public int MaxEnemies;
    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(V.Enemies.Count < MaxEnemies/10 || V.Enemies.Count == 0)
        {
            StartWave();
        }
    }
    public void StartWave()
    {
        MaxEnemies = 0;
        Coins += WaveBudgets[V.Wave];
        V.Wave++;
        int Counter = 0;
        if (V.Wave == 1)
        {
            while(Coins >= 5)
            {
                GameObject Choice = Enemies[0];
                Instantiate(Choice, SpawnSpot.position, SpawnSpot.rotation);
                Coins -= Choice.GetComponent<Enemy>().Price;
            }
        }
        else
        {
            int ListPicker = Random.Range(0, V.Towers.Count);
            while (Coins >= 5)
            {
                GameObject Choice;
                if (ListPicker < V.MageCount)
                {
                    int RNG = Random.Range(0, Heretics.Length);
                    Choice = Heretics[RNG];
                }
                else if (ListPicker > V.Towers.Count - (1 + V.TurretCount))
                {
                    int RNG = Random.Range(0, Monsters.Length);
                    Choice = Monsters[RNG];
                }
                else
                {
                    int RNG = Random.Range(0, Enemies.Length);
                    Choice = Enemies[RNG];
                }
                if (Coins > Choice.GetComponent<Enemy>().Price)
                {
                    Instantiate(Choice, SpawnSpot.position, SpawnSpot.rotation);
                    Coins -= Choice.GetComponent<Enemy>().Price;
                }
                else
                {
                    Counter++;
                }
                if (Counter > 50)
                {
                    break;
                }
            }
        }
    }
}
