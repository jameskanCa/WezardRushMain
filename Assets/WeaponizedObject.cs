using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponizedObject : MonoBehaviour {

    [SerializeField]
    public int itemType; 
    [SerializeField]
    public int maxWeight;
    [SerializeField]
    public int minWeight;
    [SerializeField]
    public int throwConstant; 
    [SerializeField]
    public int respawnTime; 
    private int weight;
    private Transform spawnPosition;
    private Transform contactPosition;
    private Transform differencePosition;
    public GameObject controllerHand;
    private float damage;
    private Behaviour halo;
    private int maxVelocity; 
    private System.Random random = new System.Random();
    private bool isGrabbed; 
    private bool isThrown;
    private bool madeContact;
    private bool movementNoHit;
    
    // Use this for initialization
    void Start () {

        // Object will be disabled and destroyed
        if(itemType == null){
            Debug.Log("Missing item type! Must assign value to continue ");
            Destroy(gameObject);
        }
    spawnPosition = gameObject.transform;

    typePropertyAssignment(itemType);

    
    }
    
    // Update is called once per frame
    void Update () {
            
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag =="Monster"){
            contactPosition = gameObject.transform;
            //Must figure out a new way to calculate difference in position
            //difference = |contactPosition - spawnPosition|;

           // damage = damageCalculator(difference);


            Destroy(gameObject);
            
        }

        // If item is dropped on groudn and missed. 
        if(collision.gameObject.tag =="Ground"){
            Destroy(gameObject);
        }

        // If gameObject contacts castle "Fell off of the generation paltform."
        if(collision.gameObject.tag == "Castle"){
            
            if(!isGrabbed){ 
            // Spawns back 
            // need to give it a time delay between spawn 
            // if Grabbed during the respawn delay. Turn it off. break coroutine. 
         	StartCoroutine(respawn()); 
            }
        }
    }



    private void typePropertyAssignment (int type){
        weight = random.Next(minWeight , maxWeight);
    }

    private float damageCalculator(Transform difference){
        float damage = difference.position.y * throwConstant * weight;

        return damage;
    }


    IEnumerator respawn()
    {
       yield return new WaitForSeconds(respawnTime);
                  
    }
}

