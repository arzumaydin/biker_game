using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeMovement : MonoBehaviour
{
    public GameObject Bicycle; 
    public GameObject Dirt;
    public GameObject DirtBullet;
    public Transform DirtParent;
    public Camera fpsCam;
    public GameObject[] dirts;
    public MeshDeformer meshDeformer;

    private bool onDirtRoad = false;
    private bool collectRoutine = false;
    private bool fireRoutine = true;
    public int bikesDestroyed = 0;

    public float speed;
    private float mouseSensitivity = 100f;
    private int maxDirt = 9; // maximum dirt that can be collected
    [SerializeField] private int dirtCount = 0;
    [SerializeField] private GameObject countText;
    public Transform CountTextParent;

    void Start()
    {
        dirts = new GameObject[maxDirt];
        Physics.IgnoreLayerCollision(3, 6); // ignore collision between main bike and other bikes
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))  //&& (transform.rotation.eulerAngles.z > 0 || transform.rotation.eulerAngles.z < 300) 
        {   
            float x;
            x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            Bicycle.transform.Rotate(Vector3.up * x); // rotate bike around y axis so it can change direction
        }
      
        Bicycle.transform.position += Bicycle.transform.right * Time.deltaTime * speed;

        float minRotation = -50;
        float maxRotation = 60;
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y = ConvertToAngle180(currentRotation.y);
        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "DirtRoad") {
            StopCoroutine(Fire());
            onDirtRoad = true;
        }
        else if(collision.gameObject.name == "RaceTrack") {
            StopCoroutine(CollectDirt());
            onDirtRoad = false;
        }
    }

    void OnCollisionStay(Collision collision) // decrease speed on dirt road
    {   
        if (GetComponent<Rigidbody>().IsSleeping()) {
            GetComponent<Rigidbody>().WakeUp();
        }

        
        if(collision.gameObject.tag == "DirtRoad")
        {
            speed = 17f;
            
            if(!collectRoutine) {
                StartCoroutine(CollectDirt());
                collectRoutine = true;
            }
            
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if(hit.point != null && hit.triangleIndex != -1)
                {
                    meshDeformer.DeformPlane(hit.triangleIndex);
                }
            }
           
        }
        else if(collision.gameObject.name == "RaceTrack")
        {
            if(!fireRoutine){
                StartCoroutine(Fire());
                fireRoutine = true;
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "DirtRoad") // speed back to default
        {
            speed = 20f;
            collectRoutine = false;
            fireRoutine = false;
        }
    }
    
    // function to collect dirt from dirt road
    private IEnumerator CollectDirt()
    {
        while(dirtCount < maxDirt && dirtCount >= 0 && onDirtRoad) {
            float dirtDistance = 0.2f; //distance of dirt from other dirts

            var newDirt = Instantiate(Dirt, DirtParent);
            Vector3 newPosition;
            if(dirtCount % 3 == 2) {
                newPosition = new Vector3(0f, dirtDistance * (dirtCount / 3), -dirtDistance);
            }
            else if(dirtCount % 3 == 1) {
                newPosition = new Vector3(0f, dirtDistance * (dirtCount / 3), 0f);
            }
            else {
                newPosition = new Vector3(0f, dirtDistance * (dirtCount / 3), dirtDistance);
            }
            newDirt.transform.localPosition = newPosition;
            dirtCount++;
            dirts[dirtCount - 1] = newDirt;

            // pop text
            StartCoroutine(ShowText());

            yield return new WaitForSeconds(1);
        }

        yield return null;
    }

    private IEnumerator Fire()
    {
        while(!onDirtRoad && dirtCount > 0 && GameObject.FindWithTag("Dirt")) {
            if(dirtCount - 1 >= 0) {
                Destroy(dirts[dirtCount - 1]);
                var newDirt = Instantiate(DirtBullet, fpsCam.transform.position, fpsCam.transform.rotation);
            }
            if(dirtCount > 0) {
                dirtCount--;
            }
            yield return new WaitForSeconds(1.3f);
        }
        yield return null;
    }

    // get y axis rotation and normalize it to a value in between -180 and 180
    public static float ConvertToAngle180(float input) {
        while(input > 360) {
            input = input - 360;
        }
        while(input < -360) {
            input = input + 360;
        }
        if(input > 180) {
            input = input - 360;
        }
        if(input < -180)
            input = 360 + input;
        return input;
    }

     private IEnumerator ShowText() {
        if(countText) {
            GameObject newText = Instantiate(countText, CountTextParent);
            Vector3 textPosition = new Vector3(0f, 0f, 0f);
            if(dirtCount < 9) {
                newText.GetComponent<TextMesh>().text = "+ " + dirtCount.ToString();
            }
            else if(dirtCount == 9) {
                newText.GetComponent<TextMesh>().text = "max";
            }
            newText.transform.localPosition = textPosition;
            newText.transform.rotation = Quaternion.Euler(0, 0, 0) * newText.transform.rotation;
            yield return new WaitForSeconds(0.5f);
            Destroy(newText);
        }
    }

}
