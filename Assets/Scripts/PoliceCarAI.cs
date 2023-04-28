using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoliceCarAI : MonoBehaviour
{
    [SerializeField] Transform playerCar;
    [SerializeField] float maxFwdSpeed = 150f;
    [SerializeField] float fwdSpeed = 0f;
    [SerializeField] float fwdAccel = 150f;
    [SerializeField] float rotationSpeed;
    bool backingUp = false;
    [SerializeField] float backingUpTime;
    float backingUpTimer = 0f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetDirection = (playerCar.position - transform.position).normalized;
        targetDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (backingUp)
        {
            if(backingUpTimer >= backingUpTime)
            {
                backingUpTimer = 0f;
                backingUp = false;
            }
            else
            {
                backingUpTimer += Time.deltaTime;
                transform.Rotate(Vector3.up, Space.World);
                if (fwdSpeed > 0)
                {
                    fwdSpeed = 0;
                }

                if (fwdSpeed >= -maxFwdSpeed)
                {
                    fwdSpeed -= Time.deltaTime * fwdAccel;
                }
            }
        }
        else
        {
            if (fwdSpeed < 0)
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

    }
    private void FixedUpdate()
    {
        if(backingUp)
        {
            rb.AddForce(-transform.forward * (fwdSpeed * 0.1f), ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(transform.forward * fwdSpeed, ForceMode.Acceleration);
        }
    }

    public void DetectCollision()
    {
        backingUp = true;
    }
        
}
