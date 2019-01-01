using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileProperties : MonoBehaviour {
	[SerializeField]
	public int attackDamage;
	[SerializeField]
	public int projectileType;
	[SerializeField]
	public GameObject player;
	public GameObject towerLocation; 
	[SerializeField]
	public int maxProjectileSpeed = 40;
	[SerializeField]
	public int minProjectileSpeed = 30;
	[SerializeField]
	public int minAttack = 15;
	[SerializeField]
	public int maxAttack = 20;
	public float speed;
	private System.Random random = new System.Random();
	// Use this for initialization
	void Start () {
		speed = (float)(random.Next(minProjectileSpeed, maxProjectileSpeed));
		attackDamage = random.Next(minAttack, maxAttack);
		player = GameObject.Find("PlayerAbstraction");
		towerLocation = GameObject.Find("Tower1");

	}
	
	// Update is called once per frame
	void Update () {
		int yOffset = random.Next(0,1);
		int xOffset = random.Next(0,1);
		int zOffset = random.Next(0,1);
		Vector3 targetPosition = player.transform.position;
		targetPosition.Set(targetPosition.x+xOffset, targetPosition.y+yOffset, targetPosition.z+zOffset);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		if(targetPosition == transform.position){
			Destroy(this.gameObject);
		}
	}
}
