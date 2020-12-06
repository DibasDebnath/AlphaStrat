using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    public float movementMultiplier;

    [Header("Target Locker")]
    public float Up;
    public float Down;
    public float Left;
    public float Right;


    private Vector3 oldPos;

    // Start is called before the first frame update
    private void Update()
    {
        if(this.transform.position.x >= Left && this.transform.position.x <= Right && this.transform.position.z <= Up && this.transform.position.z >= Down)
        {
            oldPos = this.transform.position;
            this.transform.Translate(new Vector3(RefHolder.instance.inputController.MoveDisplacementX * movementMultiplier, 0, RefHolder.instance.inputController.MoveDisplacementY * movementMultiplier));
        }
        else
        {
            this.transform.position = oldPos;
        }
        
    }
}
