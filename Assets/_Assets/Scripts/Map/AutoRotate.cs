using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotationspeed = 50f;

    private void Update()
    {
        transform.Rotate(0, 0, rotationspeed * Time.deltaTime);

    }



}
