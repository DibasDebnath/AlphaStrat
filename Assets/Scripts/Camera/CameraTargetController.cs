using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    public float movementMultiplier;
    // Start is called before the first frame update
    private void Update()
    {
        this.transform.Translate(new Vector3(RefHolder.instance.inputController.MoveDisplacementX * movementMultiplier, 0,RefHolder.instance.inputController.MoveDisplacementY * movementMultiplier));
    }
}
