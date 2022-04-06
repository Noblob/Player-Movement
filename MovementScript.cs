using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public bool isMoving = false;
    public float movementSpeed = 20;
    public Rigidbody rb;
    public float jumpAmount = 10;
    public float range = 1f;
    public bool grounded = false;
    public Camera GroundedCam;
    public float horizontalSpeed = 1.0f;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }

        ShootRaycast();

        if(Input.GetKeyDown(KeyCode.Space) & grounded == true)
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(v, h, 0);

    }

    void ShootRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(GroundedCam.transform.position, GroundedCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                StartCoroutine(JumpBool());
            }
        }
    }

    IEnumerator JumpBool()
    {
        grounded = true;
        yield return new WaitForSeconds(0.1f);
        grounded = false;
    }
}
