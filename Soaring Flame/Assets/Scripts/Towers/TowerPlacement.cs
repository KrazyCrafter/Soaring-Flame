using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacement : MonoBehaviour
{
    public GameObject RapiBlaster;
    public GameObject CannonTower;
    public GameObject MageTower;
    public GameObject Knight;
    public GameObject Ranger;

    public Transform SelectTile;
    public Vector3 PlacePos;
    public Quaternion PlaceTurn;
    public Transform HomeBase;
    public float PopTimer;

    public string SelectedButton;
    public GameObject ChoseTurret;
    public GameObject ChoseCannon;
    public GameObject ChoseMagic;
    // Start is called before the first frame update
    void Start()
    {
        V.Coins = 100;
        V.Pops = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 250.0f))
            {
                SelectTile = hit.transform;
                PlacePos = SelectTile.position;
                PlaceTurn = SelectTile.rotation;
                Debug.Log("You selected " + SelectTile.tag + " At " + SelectTile.position.x + ", " + SelectTile.position.y + ", " + SelectTile.position.z + ", "); //check what you clicked
                if (SelectTile.tag == "Placable")
                {
                    if(SelectedButton == "Turret" && V.Coins >= 100)
                    {
                        V.Coins -= 100;
                        Instantiate(RapiBlaster, PlacePos, PlaceTurn);
                        Destroy(SelectTile.gameObject);
                    }
                    if (SelectedButton == "Cannon" && V.Coins >= 100)
                    {
                        V.Coins -= 100;
                        Instantiate(CannonTower, PlacePos, PlaceTurn);
                        Destroy(SelectTile.gameObject);
                    }
                    if (SelectedButton == "Magic" && V.Coins >= 120)
                    {
                        V.Coins -= 120;
                        Instantiate(MageTower, PlacePos, PlaceTurn);
                        Destroy(SelectTile.gameObject);
                    }
                }
            }
        }
        PopTimer += Time.deltaTime;
        if (PopTimer > 10)
        {
            PopTimer = 0;
            V.Pops++;
        }
    }
    public void SpawnSoldier()
    {
        if (V.Coins >= 15 && V.Pops > 0)
        {
            Instantiate(Knight, HomeBase.position, HomeBase.rotation);
            V.Coins -= 15;
            V.Pops--;
        }
    }
    public void SpawnRanger()
    {
        if (V.Coins >= 15 && V.Pops > 0)
        {
            Instantiate(Ranger, HomeBase.position, HomeBase.rotation);
            V.Coins -= 15;
            V.Pops--;
        }
    }
    public void ChooseTower(string Clicked)
    {
        if(SelectedButton == "Turret")
        {
            ChoseTurret.SetActive(false);
        }
        else if (SelectedButton == "Cannon")
        {
            ChoseCannon.SetActive(false);
        }
        else if (SelectedButton == "Magic")
        {
            ChoseMagic.SetActive(false);
        }
        SelectedButton = Clicked;
        if (SelectedButton == "Turret")
        {
            ChoseTurret.SetActive(true);
        }
        else if (SelectedButton == "Cannon")
        {
            ChoseCannon.SetActive(true);
        }
        else if (SelectedButton == "Magic")
        {
            ChoseMagic.SetActive(true);
        }
    }
}
