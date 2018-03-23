using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float smooth = 1f;
    private Quaternion targetRotation;
    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 4 * smooth * Time.deltaTime);
    }
}