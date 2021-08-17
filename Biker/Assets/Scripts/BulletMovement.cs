using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed = 80f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        Destroy(this.gameObject, 5f); //destroy after 5 seconds
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.tag == "AIBike") // speed back to default
        {
            Destroy(this.gameObject);
        }
    }
}
