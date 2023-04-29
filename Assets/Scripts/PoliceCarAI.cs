using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] Rigidbody motorRB;
    [SerializeField] Rigidbody colliderRB;

    private void Start()
    {
        motorRB.transform.parent = null;
        colliderRB.transform.parent = null;
        playerCar = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Vector3 targetDirection = (playerCar.position - transform.position).normalized;
        targetDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = motorRB.transform.position;

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

        if(Vector3.Distance(transform.position, playerCar.transform.position) > 300f)
        {
            Destroy(gameObject);
        }


    }
    private void FixedUpdate()
    {
        if(backingUp)
        {
            motorRB.AddForce(-transform.forward * fwdSpeed, ForceMode.Acceleration);
        }
        else
        {
            motorRB.AddForce(transform.forward * fwdSpeed, ForceMode.Acceleration);
        }
        colliderRB.MoveRotation(transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            backingUp = true;
        }
    }

}
