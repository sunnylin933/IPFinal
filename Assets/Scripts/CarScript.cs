using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public static CarScript instance;

    public Rigidbody sphereRB;
    public Rigidbody carRB;

    public float maxFwdSpeed = 150f;
    public float fwdSpeed = 0f;
    public float fwdAccel = 150f;
    public float turnSpeed;
    public LayerMask groundLayer;

    float moveInput;
    [HideInInspector] public float moveBase;
    float turnInput;
    public bool isGrounded;

    private float normalDrag;
    public float modifiedDrag;
    public float alignToGroundTime;
    public float turnModifier;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
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
        sphereRB.transform.parent = null;
        carRB.transform.parent = null;

        normalDrag = sphereRB.drag;
    }

    // Update is called once per frame
    void Update()
    {
        ///Movement
        moveInput = Input.GetAxisRaw("Vertical");
        //Forcibly push player forward
        moveInput = moveInput == 0 ? 1 : moveInput;
        moveBase = moveInput;

        if (moveInput > 0)
        {
            if(fwdSpeed < 0)
            {
                fwdSpeed = 0;
            }

            if (fwdSpeed < maxFwdSpeed)
            {
                fwdSpeed += Time.deltaTime * fwdAccel;
            }
            else
            {
                fwdSpeed = maxFwdSpeed;
            }
        }
        else
        {
            if (fwdSpeed > 0)
            {
                fwdSpeed = 0;
            }

            if (fwdSpeed >= -maxFwdSpeed)
            {
                fwdSpeed -= Time.deltaTime * fwdAccel;
            }
        }

        if (Vector3.Distance(sphereRB.velocity, Vector3.zero) < 0.1f && fwdSpeed > 80f)
        {
            fwdSpeed = 0;
        }

        turnInput = Input.GetAxisRaw("Horizontal");
        //Prevents turning if not moving
        turnModifier = Vector3.Distance(sphereRB.velocity, Vector3.zero)/25f;
        turnModifier = turnModifier > 1 ? 1 : turnModifier;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput * turnModifier;
        if (isGrounded)
        {
            transform.Rotate(0, newRotation, 0, Space.World);
        }
        transform.position = sphereRB.transform.position;

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        moveInput *= Mathf.Abs(fwdSpeed);
        if (turnInput != 0)
        {
            moveInput *= 0.85f;
        }
        sphereRB.drag = isGrounded ? normalDrag : modifiedDrag;
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            sphereRB.AddForce(transform.up * -40f);
        }
        carRB.MoveRotation(transform.rotation);
    }
}
