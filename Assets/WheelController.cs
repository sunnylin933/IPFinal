using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] GameObject[] wheels;
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var wheel in wheels) 
        {
            wheel.transform.Rotate(Time.deltaTime * Input.GetAxisRaw("Vertical") * rotationSpeed, 0, 0, Space.Self);
        }
    }
}
