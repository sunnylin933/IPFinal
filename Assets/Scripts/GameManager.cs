using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject[] policeCars;
    float spawnTimer = 0;
    [SerializeField] float spawnTime;

    [Header("Scoring")]
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] TextMeshProUGUI survivalScore;
    [SerializeField] TextMeshProUGUI bustedScene;
    [SerializeField] TextMeshProUGUI finalScore;
    public float survivalTime = 0;
    [SerializeField] bool isPlaying = true;
    public bool isStarted = false;
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
        spawnPoints = new List<GameObject>();
        validSpawns = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
        {
            foreach (GameObject spawnPoint in spawnPoints.ToList())
            {
                if (spawnPoint != null)
                {
                    var dist = Vector3.Distance(spawnPoint.transform.position, player.transform.position);
                    if (dist <= maxSpawnDistance && dist >= minSpawnDistance)
                    {
                        if (!validSpawns.Contains(spawnPoint))
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
                    if (validSpawns.Contains(spawnPoint)) validSpawns.Remove(spawnPoint);
                }
            }
            if (isPlaying)
            {
                survivalTime = survivalTime += Time.deltaTime;
                survivalScore.text = survivalTime.ToString("0.00") + " sec";

                if(spawnTimer > spawnTime)
                {
                    SpawnPolice();
                    spawnTimer = 0;
                }
                else
                {
                    spawnTimer += Time.deltaTime;
                }
            }
        }
        else
        {
            
        }
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

    public void StartGame()
    {
        isStarted = true;
        survivalScore.gameObject.SetActive(true);
        startButton.SetActive(false);
    }

    public void SpawnPolice()
    {
        var randomInt = Random.Range(0, validSpawns.Count);
        if (survivalTime < 35f)
        {
            Instantiate(policeCars[0], validSpawns[randomInt].transform.position, Quaternion.identity);
        }
        else
        {
            var randomCar = Random.Range(0, 2);
            Instantiate(policeCars[randomCar], validSpawns[randomInt].transform.position, Quaternion.identity);
        }
    }

    public void Busted()
    {
        player.GetComponent<CarScript>().enabled = false;
        isPlaying = false;
        bustedScene.gameObject.SetActive(true);
        survivalScore.gameObject.SetActive(false);
        restartButton.SetActive(true);
        finalScore.text = "<color=#FFFFFF> Your final score: </color>" + survivalTime.ToString("0.00") + " sec"; 
        print("Busted!");
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
