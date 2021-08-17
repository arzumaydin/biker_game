using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public GameObject AIBicycle;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    //public Animator animator;
    [SerializeField] public Transform targetPosition;
    private bool rotating = false;
    private bool reachedTarget = false;
    private float smoothTime = 1.0f; //rotate over 1 seconds
    private bool bikeIsHit = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent.updateRotation = true;
    }

    void Start()
    {
        navMeshAgent.stoppingDistance = 1f;
        navMeshAgent.speed = 20f;
        Physics.IgnoreLayerCollision(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.enabled) {

            navMeshAgent.destination = targetPosition.position;
            navMeshAgent.Move(transform.forward * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.tag == "DirtBullet" && !rotating && !bikeIsHit) // if rotating don't enter the loop
        {
            bikeIsHit = true;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
            if(this.gameObject.GetComponent<NavMeshAgent>().enabled == true) {
                this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                //AIBicycle.transform.Rotate(new Vector3(80, 0, 0) * Time.deltaTime);
                rotating = true;
                // fall either right or left side
                float rando = Random.Range(0, 100); // pick a random number between 1 and 100
                int multiplier = 1;
                if(rando > 50) {
                    multiplier = -1;
                }
                StartCoroutine(RotateOverTime(AIBicycle.transform.localEulerAngles.z, AIBicycle.transform.localEulerAngles.z + (85 * multiplier), smoothTime));
            }
            else if(collision.collider.gameObject.tag == "Target" && !reachedTarget) {
                reachedTarget = true;
                navMeshAgent.destination = AIBicycle.transform.position;
            }
        }
    }

    /*
    private IEnumerator Rotate(Vector3 angles, float duration) {
        rotating = true;
        Quaternion startRotation = this.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for(float t = 0; t < duration; t += Time.deltaTime) {
            this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }
        this.transform.rotation = endRotation;
        rotating = false;
    }

    public void StartRotation() {
        if(!rotating)
            StartCoroutine(Rotate(new Vector3(90, 0, 0), 10));
    }*/

    IEnumerator RotateOverTime(float currentRotation, float desiredRotation, float overTime) {
        rb.useGravity = true;
        float i = 0.0f;
        while(i <= 1) {
            AIBicycle.transform.localEulerAngles = new Vector3(AIBicycle.transform.localEulerAngles.x, AIBicycle.transform.localEulerAngles.y, Mathf.Lerp(currentRotation, desiredRotation, i));
            i += Time.deltaTime / overTime;
            yield return null;
        }
        //yield return new WaitForSeconds(overTime);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rotating = false; // no longer rotating
        Destroy(this.gameObject);
    }
}
