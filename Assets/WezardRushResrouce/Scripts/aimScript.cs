using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimScript : MonoBehaviour
{
    // Whether an enemy is selected
    private bool lockedIn;
    // What the enemy type is
    private int enemyType;
    // Distance from you to the enemy 
    public float distance;
    // Type of attack/lock type 
    private int lockType;
    private bool inUse;
    private LineRenderer aimLine;
    private RaycastHit hit;
    private Ray ray;
    // Player hand
    public GameObject userHand;
    // Enemy Game Object
    public GameObject enemy;

    // Raycast
    // Raycast distance
    public float maxDistance;



    // Use this for initialization
    void Start()
    {
        lockedIn = false;
        enemyType = 0;
        distance = 0.0f;
        lockType = 0;
        enemy = null;
        aimLine = GetComponentInParent<LineRenderer>();
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            distance = hit.distance;
            // Refactoring may be needed to accomdate multiple monster type or weapons
            if (hit.collider.gameObject.name.Contains("Monster"))
            {
                Debug.Log("Object hit " + hit.collider.gameObject.name);

                // Enemy type may be embeded in script to avoid naming issues 
                if (Input.GetMouseButtonDown(0))
                {
                    this.enemy = hit.collider.gameObject;
                   Behaviour behave = (Behaviour)enemy.GetComponent("Halo");
                   behave.enabled = true;
                }

            }

        }
        // Cam should be replaced with hand of player user.
        aimLine.SetPosition(0, this.transform.position);
        if (this.enemy)
        {
            if (Input.GetMouseButton(0))
            {
                //ray = ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // Replace camera.main with user's hands
                    //enemy.transform.position = cam.ScreenToWorldPoint(new Vector3(hit.point.x, hit.point.y, hit.point.z))
                    enemy.transform.position = (new Vector3(hit.point.x, hit.point.y, enemy.transform.position.z));
                    aimLine.SetPosition(1, hit.point);
                }

            }
        }else{
                //Replace camera with hand position
                aimLine.SetPosition(1, (transform.forward*100));
        }

        if (Input.GetMouseButtonUp(0))
        {       
                  Behaviour behave = (Behaviour)enemy.GetComponent("Halo");
                   behave.enabled = false;
            this.enemy = null;

            
        }

    }

}
