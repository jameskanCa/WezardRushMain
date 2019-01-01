using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public class monsterMovementBasic : MonoBehaviour {
    private bool isSelected;

    public GameObject destination;
    private VRTK.VRTK_InteractableObject interactableObjectScript;

    //public Transform pos3;
    //public Transform pos4;

    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        isSelected = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);
        interactableObjectScript = GetComponent<VRTK.VRTK_InteractableObject>();
		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new VRTK.InteractableObjectEventHandler(MovementGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new VRTK.InteractableObjectEventHandler(MovementUngrabbed);
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if(this.transform.position == destination.transform.position){
            agent.isStopped=true;
        }
    }

    private void MovementGrabbed(object sender, InteractableObjectEventArgs e){
        agent.isStopped = true;
        agent.enabled = false;
    }

    private void MovementUngrabbed(object sender, InteractableObjectEventArgs e){
        agent.isStopped = false;
        agent.enabled = true;
        agent.SetDestination(destination.transform.position);
    }
}
