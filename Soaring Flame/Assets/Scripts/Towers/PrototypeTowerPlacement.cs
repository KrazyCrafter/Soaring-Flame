using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrototypeTowerPlacement : MonoBehaviour
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
                    Instantiate(RapiBlaster, PlacePos, PlaceTurn);
                    Destroy(SelectTile.gameObject);
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
}
