using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] GameObject[] wheels;
    [SerializeField] float rotationSpeed;
    [SerializeField] TrailRenderer[] trails;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(Time.deltaTime * rotationSpeed, 0, 0, Space.Self);
        }

        if (horizontalAxis > 0)
        {
            anim.SetBool("turningLeft", false);
            anim.SetBool("turningRight", true);
        }
        else if (horizontalAxis < 0)
        {
            anim.SetBool("turningLeft", true);
            anim.SetBool("turningRight", false);
        }
        else
        {
            anim.SetBool("turningLeft", false);
            anim.SetBool("turningRight", false);
        }

        if (horizontalAxis != 0)
        {
            foreach (var trail in trails)
            {
                trail.emitting = true;
            }
        }
        else
        {
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }
        }
    }
}
