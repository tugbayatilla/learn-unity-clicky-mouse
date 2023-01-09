using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private TargetStore targetStore;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float torque = 10f;
    private float spawnRangeX = 4f;
    private float spawnPosY = -2f;
    public int pointValue = 10;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetStore = GameObject.Find("TargetStore").GetComponent<TargetStore>();

        // how high they can jump
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        //rotate them
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // starting position of the Targets
        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //NOTE: everytime when you click on any collider, this method will be executed.
    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, transform.rotation);
        targetStore.OnTargetDestroyed(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        targetStore.OnTargetDestroyed(this);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY);
    }
}
