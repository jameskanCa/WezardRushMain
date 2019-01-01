using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scoringScript : MonoBehaviour {

    //Potential Race Condition
    /*private static scoringScript instance = null;
        private static object padlock = new object();


        scoringScript(){

        }

        public static scoringScript Instance{
            get{
                lock(padlock){
                    if(instance == null){
                        instance = new scoringScript();
                    }
                    return instance;
                }
            }
        }

     */
     //Must be updated
    public static int monsterCount;
    public static bool monsterGenerated;
    [SerializeField]
    public int restartTime = 10;
	private TextMesh tempUI;
	private static int enemiesInTower = 0;
	private static int enemiesDestroyed = 0;
    [SerializeField]
    public static int loseCondition = 4; 
    [SerializeField]
    public bool resetOn = true;

	void Start(){
		tempUI = GetComponent<TextMesh>();
        monsterCount = 0;
	}

	void Update(){
		tempUI.text = "Monsters Destroyed: " + enemiesDestroyed + "\n" + "Enemies Entered: "
		+ enemiesInTower ;


        if (enemiesInTower > loseCondition) {
            tempUI.text = "You Lost! The monsters invaded your tower!";
            StartCoroutine(SceneRestart());
        }

        if (monsterGenerated && enemiesDestroyed == monsterCount) {
            tempUI.text = "You win!!";
             StartCoroutine(SceneRestart());
        }
	}

    public static void enemiesGenerated(){
        monsterGenerated = true;
        monsterCount++;
    }

	public static void EnemiesEntered(){
		enemiesInTower++;
		Debug.Log("Entered" + enemiesInTower);
	}
	//Incase we find the use where for the longer we hold out the 
	// chance of some monsters being kicked out
	public static void EnemiesRemoved(){
		enemiesInTower--;
		Debug.Log("Removed" + enemiesInTower);
	}

	public static void EnemiesDestroyed(){
		enemiesDestroyed++;
		Debug.Log("Destroyed" + enemiesDestroyed);
	}

    public static void EnemyCountReset() {
        enemiesInTower = 0;
    }

      IEnumerator SceneRestart()
    {   
        if(resetOn){
        yield return new WaitForSeconds(restartTime);
         SceneManager.LoadScene("MainScene");
         scoringReset();
        }
    }
    
    private void scoringReset(){
        enemiesDestroyed=0;
        enemiesInTower=0;
        tempUI.text = "Monsters Destroyed: " + enemiesDestroyed + "\n" + "Enemies Entered: "
		+ enemiesInTower ;
    }
}
