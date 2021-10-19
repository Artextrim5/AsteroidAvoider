using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float maxZ;


    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        // Invoke (nameof (AsteroidRotation), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth == null) { return; }
        playerHealth.Crash();
    }

    private void AsteroidRotation()
    {
        float x = Random.Range(0, maxX);
        float y = Random.Range(0, maxY);
        float z = Random.Range(0, maxZ);

        myRigidbody.angularVelocity =new Vector3 (x, y, z);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
