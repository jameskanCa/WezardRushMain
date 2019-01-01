using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawn : MonoBehaviour {

    [SerializeField]
    public int monsterGenCountPerRound = 5;
    public int WaveTime = 50;
    [SerializeField]
    public GameObject monster = null;
    
    
	// Use this for initialization
	// Update is called once per frame
    void Start()
    {
       StartCoroutine(MonsterGen()); 
    }

    IEnumerator MonsterGen()
    {
        //This is a coroutine
        if (monster != null)
        {
            for(int i=0; i<=monsterGenCountPerRound; i++){
            Instantiate(monster);
            monsterMovementBasic movementScript = monster.GetComponent<monsterMovementBasic>();
            movementScript.destination = GameObject.Find("Destination");
            monster.transform.position = gameObject.transform.position; 
            scoringScript.enemiesGenerated();
            yield return new WaitForSeconds(WaveTime);
            }
           
        }else{
            Debug.Log("Monster Is Not Placed in Script");
        }

        
        
    }
}
