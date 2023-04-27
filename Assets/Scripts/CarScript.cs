using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public static CarScript instance; 

    public Rigidbody sphereRB;
    public Rigidbody carRB;

    public float moveSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    float moveInput;
    [HideInInspector] public float moveModifier;
    float turnInput;
    bool isGrounded;

    private float normalDrag;
    public float modifiedDrag;
    public float alignToGroundTime;
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
        sphereRB.transform.parent = null;
        carRB.transform.parent = null;

        normalDrag = sphereRB.drag;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        if(moveInput == 0 )
        {
            moveInput = 1;
            moveModifier = moveInput;
        }
        turnInput = Input.GetAxisRaw("Horizontal");

        float newRotation = turnInput * turnSpeed * Time.deltaTime * moveModifier;
        if(isGrounded)
        {
            transform.Rotate(0, newRotation, 0, Space.World);
        }
        transform.position = sphereRB.transform.position;

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        moveInput *= moveSpeed > 0 ? moveSpeed : reverseSpeed;
        if (turnInput != 0)
        {
            moveInput *= 0.85f;
        }
        sphereRB.drag = isGrounded ? normalDrag : modifiedDrag;

        sphereRB.transform.rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if(isGrounded)
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
