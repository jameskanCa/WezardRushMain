using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class projectileAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public int projectileCount = 5;
    [SerializeField]
    public Transform projectileDestination;
    [SerializeField]
    public float minProjectileInterval = 20f;
    [SerializeField]
    public float maxProjectileInterval = 40f;
    private float projectileSpeed;
    private System.Random randomGenerator = new System.Random();
    private VRTK.VRTK_InteractableObject interactableObjectScript;
    private bool stopShooting = false;
    private Coroutine co;
    // Use this for initialization
    void Start()
    {
        if (minProjectileInterval > maxProjectileInterval)
        {
            Debug.Log("Min max interval needs to be swapped");
        }
        interactableObjectScript = GetComponent<VRTK.VRTK_InteractableObject>();
        interactableObjectScript.enabled = true;
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new VRTK.InteractableObjectEventHandler(MovementGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new VRTK.InteractableObjectEventHandler(MovementUngrabbed);

        startShootingCoroutine();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator shoot(float shootTime)
    {
        WaitForSeconds wait = new WaitForSeconds(shootTime);
        int i = 0;
        while (!stopShooting && i < projectileCount)
        {
            Debug.Log("Waiting" + shootTime + "new time " + Time.time);
            GameObject generatedProejctile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            yield return wait;
            i++;
        }
    }


    private void MovementGrabbed(object sender, InteractableObjectEventArgs e)
    {
        StopCoroutine(co);
    }

    private void MovementUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        startShootingCoroutine();
    }

    private void startShootingCoroutine()
    {
        co = StartCoroutine(shoot(Random.Range(minProjectileInterval, maxProjectileInterval)));
    }
}
