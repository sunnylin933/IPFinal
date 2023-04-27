using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public Rigidbody sphereRB;
    float moveInput;
    float turnInput;
    public float moveSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        moveInput *= moveSpeed > 0 ? moveSpeed : reverseSpeed;

        transform.position = sphereRB.transform.position;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
