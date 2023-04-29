using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDetector : MonoBehaviour
{
    [SerializeField] int ID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            this.transform.parent.transform.parent.GetComponent<tileGeneration>().DetectSpawn(ID);
        }

    }
}
