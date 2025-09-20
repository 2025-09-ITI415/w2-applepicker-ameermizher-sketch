using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AppleTree : MonoBehaviour {
     [Header("Inscribed")]                                                  // a
     // Prefab for instantiating apples
     public GameObject   applePrefab;
 
     // Speed at which the AppleTree moves
     public float        speed = 1f;
 
     // Distance where AppleTree turns around
     public float        leftAndRightEdge = 10f;
 
     // Chance that the AppleTree will change directions
     public float        changeDirChance = 0.1f;
 
     // Seconds between Apples instantiations
     public float        appleDropDelay = 1f;

     void Start () {
         // Start dropping apples                                           // b
     }

     void Update () {
         // Basic Movement                                                  // b
         // Changing Direction                                              // b
     }
 }
 public class AppleTree : MonoBehaviour {
     
     void Start() {
         // Start dropping apples                                          
        Invoke( "DropApple", 2f );                                        // a
     }

     void DropApple() {                                                    // b
         GameObject apple = Instantiate<GameObject>( applePrefab );        // c
         apple.transform.position = transform.position;                    // d
         Invoke( "DropApple", appleDropDelay );                            // e
    }

     void Update() {  }                                                   // f
     
 }

 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 public class Basket : MonoBehaviour {
     
     void OnCollisionEnter( Collision coll ) {                         
         // Find out what hit this basket
         GameObject collidedWith = coll.gameObject;                    
         if ( collidedWith.CompareTag("Apple") ) {                          
             Destroy( collidedWith );
             // Increase the score
             scoreCounter.score += 100;  
             HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score );        
         }
     }
 }
 using System.Collections;
 using System.Collections.Generic;                                          // a
 using UnityEngine;
 using UnityEngine.SceneManagement;                                         // b

 public class ApplePicker : MonoBehaviour {
     [Header("Inscribed")]
     public GameObject       basketPrefab;
     public int              numBaskets     = 3;
     public float            basketBottomY  = -14f;
     public float            basketSpacingY = 2f;
     public List<GameObject> basketList;                                   // c

     void Start () {
         basketList = new List<GameObject>();                              // d
         for (int i=0; i <numBaskets; i++) {
             GameObject tBasketGO = Instantiate<GameObject>( basketPrefab );
             Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + ( basketSpacingY * i );             tBasketGO.transform.position = pos;
             basketList.Add( tBasketGO );                                  // e
         }
     }

     public void AppleMissed() {                                        
         // Destroy all of the falling Apples
         GameObject[] appleArray=GameObject.FindGameObjectsWithTag("Apple");
         foreach ( GameObject tempGO in appleArray ) {
             Destroy( tempGO );
         }

         // Destroy one of the Baskets                                    // f
         // Get the index of the last Basket in basketList
         int basketIndex = basketList.Count -1;
         // Get a reference to that Basket GameObject
         GameObject basketGO = basketList[basketIndex];
         // Remove the Basket from the list and destroy the GameObject
        basketList.RemoveAt( basketIndex );
         Destroy( basketGO );
 
         // If there are no Baskets left, restart the game 
         if ( basketList.Count == 0 ) {
             SceneManager.LoadScene( "_Scene_0" );                       // g
         }
     }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     // We need this line for uGUI to work.

 public class HighScore : MonoBehaviour {
     static private Text      _UI_TEXT;                                   
     static private int       _SCORE = 1000;                              

     private Text txtCom;  // txtCom is a reference to this GO’s Text component

     void Awake () {                                                      
         _UI_TEXT = this.GetComponent<Text>();                            
        
         // If the PlayerPrefs HighScore already exists, read it
         if (PlayerPrefs.HasKey("HighScore")) {                                        // a
             SCORE = PlayerPrefs.GetInt("HighScore");
         }
         // Assign the high score to HighScore
         PlayerPrefs.SetInt("HighScore", SCORE);                                       // b
     }

     static public int SCORE {
         get { return _SCORE; }
         private set {
             _SCORE = value;
             PlayerPrefs.SetInt("HighScore", value);                                   // c
             if ( _UI_TEXT != null ) {
                 _UI_TEXT.text = "High Score: " + value.ToString( "#,0" );
             }
         }
     }

     static public void TRY_SET_HIGH_SCORE( int scoreToTry ) {           
         if ( scoreToTry <= SCORE ) return; // If scoreToTry is too low, return
         SCORE = scoreToTry;
     }

     // The following code allows you to easily reset the PlayerPrefs HighScore
     [Tooltip( "Check this box to reset the HighScore in PlayerPrefs" )]
     public bool resetHighScoreNow = false;                                           // d
 
     void OnDrawGizmos() {                                                            // e
         if ( resetHighScoreNow ) {
             resetHighScoreNow = false;
             PlayerPrefs.SetInt( "HighScore", 1000 );
             Debug.LogWarning( "PlayerPrefs HighScore reset to 1,000." );
         }        
     }
 }