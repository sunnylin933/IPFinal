using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] spawnPoints;
    [SerializeField] TextMeshProUGUI survivalScore;
    float survivalTime = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        survivalTime += Time.deltaTime;
        print(survivalTime);
        survivalScore.text = survivalTime.ToString();
    }

    public void RebuildNavmesh(NavMeshSurface surface)
    {
        NavMesh.AddNavMeshData(surface.navMeshData);
    }

    public void RecheckSpawns()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
    }

    /*public void RecheckSpawnsDelayed()
    {
        Invoke("RecheckSpawns", 0.1f);
    }*/
}
