using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDetector : MonoBehaviour
{
    [SerializeField] int ID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.transform.parent.transform.parent.GetComponent<tileGeneration>().DetectSpawn(ID);
        }
    }
}
