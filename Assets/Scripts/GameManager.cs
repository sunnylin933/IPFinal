using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player;
    public static GameManager instance;

    [Header("Spawning")]
    [SerializeField] float minSpawnDistance;
    [SerializeField] float maxSpawnDistance;
    public List<GameObject> spawnPoints;
    public List<GameObject> validSpawns;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI survivalScore;
    public float survivalTime = 0;
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
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn"));
        validSpawns = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        survivalTime = survivalTime += Time.deltaTime;
        survivalScore.text = survivalTime.ToString("0.00");

        foreach (GameObject spawnPoint in spawnPoints.ToList())
        {
            if (spawnPoint != null)
            {
                var dist = Vector3.Distance(spawnPoint.transform.position, player.transform.position);
                if (dist <= maxSpawnDistance && dist >= minSpawnDistance)
                {
                    if(!validSpawns.Contains(spawnPoint))
                    {
                        validSpawns.Add(spawnPoint);
                    }
                }
                else
                {
                    if (validSpawns.Contains(spawnPoint))
                    {
                        validSpawns.Remove(spawnPoint);
                    }
                }
            }
            else
            {
                spawnPoints.Remove(spawnPoint);
                if(validSpawns.Contains(spawnPoint)) validSpawns.Remove(spawnPoint);
            }
        }

        SpawnPolice();
    }

    private void LateUpdate()
    {
        
    }

    public void RecheckSpawns(GameObject[] newSpawns)
    {
        foreach(GameObject newSpawn in newSpawns) 
        { 
            spawnPoints.Add(newSpawn);
        }
    }

    public void SpawnPolice()
    {

    }
}
