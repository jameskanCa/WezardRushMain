using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;
using System;
public class BasicMonster : MonoBehaviour
{
    public static float mandatoryMinFlightHeight = 15.0f;
    public static float timeBeforeDissapear = 0.1f;
    [SerializeField]
    public int minHealth = 90;
    [SerializeField]
    public int maxHealth = 120;
    [SerializeField]
    public int minSpeed = 10;
    [SerializeField]
    public int maxSpeed = 20;
    [SerializeField]
    public int minWeight = 10;
    [SerializeField]
    public int maxWeight = 30;
    [SerializeField]
    public float shakeAmplitude = 0.4f;
    [SerializeField]
    public float shakeDuration = 0.3f;
    public bool isGrabbed;
    private bool isStillAlive;
    private bool isReleased;
    private bool wasInAir;
    private int monsterType;
    private int health;
    private int weight;
    private static int walkingSpeed;
    private bool destroyConditionMet;
    private VRTK.VRTK_InteractableObject interactableObjectScript;
    private projectileAttack projectileAttack;
    private NavMeshAgent agent;
    private Behaviour halo;
    private System.Random random = new System.Random();
    private CameraMovements cameraMovementsSimulator;
    private CameraMovements cameraMovementOculus;
    // Use this for initialization
    void Start()
    {
        if (GetComponent<VRTK.VRTK_InteractableObject>() == null)
        {
            Debug.LogError("Missing Iteractable Object attached to gameobject");
            return;
        }

        cameraMovementsSimulator = GameObject.Find("VRTKSDKManager").GetComponent<CameraMovements>();

        monsterPropertyAssignment(random);

        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = true;

        wasInAir = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = walkingSpeed;

        projectileAttack = GetComponent<projectileAttack>();
        if (projectileAttack == null)
        {
            Debug.Log("Projectile Attack is missing from script");
        }

        interactableObjectScript = GetComponent<VRTK.VRTK_InteractableObject>();
        interactableObjectScript.enabled = true;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new VRTK.InteractableObjectEventHandler(ObjectGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new VRTK.InteractableObjectEventHandler(ObjectReleased);
        //GetComponent<VRTK_Pointer>().PointerStateValid += new VRTK.DestinationMarkerEventHandler(PointerValid);
    }

    // Update is called once per frame

    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        projectileAttack.enabled = false;
        interactableObjectScript.enabled = true;
        isGrabbed = true;

    }

    private void ObjectReleased(object sender, InteractableObjectEventArgs e)
    {
        projectileAttack.enabled = true;
        this.isReleased = true;
    }

    private void PointerValid(object sender, DestinationMarkerEventArgs e)
    {


    }

    void Update()
    {
        if (transform.position.y > mandatoryMinFlightHeight)
        {
            this.wasInAir = true;
        }
    }

    private void monsterPropertyAssignment(System.Random random)
    {
        health = random.Next(minHealth, maxHealth);
        walkingSpeed = random.Next(minSpeed, maxSpeed);
        weight = random.Next(minWeight, maxWeight);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if ((this.isReleased && this.wasInAir) || (this.isGrabbed && this.wasInAir))
            { // smash or throw
                Destroy(gameObject, timeBeforeDissapear);
                StartCoroutine(cameraMovementsSimulator.shake(shakeDuration, shakeAmplitude));
                scoringScript.EnemiesDestroyed();
                return;
            }
        }

        // Two Object Colliding
        if (this.isGrabbed)
        {
            if (collision.gameObject.tag == "Monster")
            {
                GameObject collidingEnemy = collision.gameObject;
                BasicMonster monsterIdentityScript = collidingEnemy.GetComponent<BasicMonster>();
                if (monsterIdentityScript.isGrabbed && collision.gameObject)
                { // doees this mean that it is there?
                    Destroy(collision.gameObject);
                    scoringScript.EnemiesDestroyed();
                    return;
                }
            }
        }
    }

    public void specialProperty()
    {

    }
}
