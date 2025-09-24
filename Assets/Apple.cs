using UnityEngine;

public class Apple : MonoBehaviour
{
    private float  bottomY = -20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }
    // Update is called once per frame
    void Update () {
         if ( transform.position.y < bottomY ) {                                       // a
             Destroy( this.gameObject );        
 
             // Get a reference to the ApplePicker component of Main Camera
             Applepicker apScript = Camera.main.GetComponent<Applepicker>();           // b
             // Call the public AppleMissed() method of apScript
             apScript.AppleMissed();                                                   // c
         }                                                                             // a
     }
 }
