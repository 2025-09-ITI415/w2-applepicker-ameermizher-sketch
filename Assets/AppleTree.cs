using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    void Update () {
        // Basic Movement
        ...
        // Changing Direction
        if ( pos.x < -leftAndRightEdge ) {
            speed = Mathf.Abs(speed);   // Move right
        } else if ( pos.x > leftAndRightEdge ) {
            speed = -Mathf.Abs(speed);  // Move left
        } else if ( Random.value < changeDirChance ) {   // a
            speed *= -1;  // Change direction             // b
        }
    }
}

   void Start()

{
// start dropping Apples
Invoke(&quot;DropApple&quot;, 2f);

}

void DropApple()
{
GameObject apple = Instantiate&lt;GameObject&gt;(applePrefab);
apple.transform.position = transform.position;

Invoke(&quot;DropApple&quot;,appleDropDelay);
}

void Update()

// Basic Movement

Vector3 pos = transform.position;
pos.x += speed * Time.deltaTime;
transform.position = pos;

//changing direction

if (pos.x &lt; -leftAndRightEdge)
{
speed = Mathf.Abs(speed); //Move right
}                        // b
else if (pos.x &gt; leftAndRightEdge)
{
speed = -Mathf.Abs(speed); // move left
}
//else if (Random.value &lt; changeDirChance)
//{
// speed *= -1; // change direction
//}

}

void FixedUpdate()
{
// Random dorection changes are now time-based due to FixUpdate()
if (Random.value &lt; changeDirChance)
{
speed *= -1; // change direction
}

}
