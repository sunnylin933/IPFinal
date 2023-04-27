using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] northSpawns;
    [SerializeField] GameObject[] eastSpawns;
    [SerializeField] GameObject[] southSpawns;
    [SerializeField] GameObject[] westSpawns;
    [SerializeField] GameObject[] cityPrefabs;
    [SerializeField] LayerMask gLayer;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 350f)
        {
            Destroy(gameObject);
        }
    }

    public void DetectSpawn(int ID)
    {
        //North
        switch(ID)
        {
            case 0:
                //North
                CheckStatus(northSpawns);
                break;
            case 1:
                //East
                CheckStatus(eastSpawns);
                break;
            case 2:
                //South
                CheckStatus(southSpawns);
                break;
            case 3:
                //West
                CheckStatus(westSpawns);
                break;
        }
    }

    private void CheckStatus(GameObject[] spawnList)
    {
        foreach (GameObject spawn in spawnList)
        {
            Collider[] hitColliders = Physics.OverlapBox(spawn.transform.position, transform.localScale * 2, Quaternion.identity, gLayer);
            if(hitColliders.Length == 0) 
            {
                var ground = Instantiate(cityPrefabs[UnityEngine.Random.Range(0,cityPrefabs.Length)]);
                ground.transform.position = spawn.transform.position;
                ground.name = "Ground";
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
