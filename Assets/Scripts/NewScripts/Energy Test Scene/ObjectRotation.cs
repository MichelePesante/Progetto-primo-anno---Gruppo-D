using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {


    private Quaternion TotemRotation;
    private float smooth = 1f;

    private void Start()
    {
        TotemRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update ()
    {

        RotateTotem();

    }


    public void RotateTotem()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TotemRotation *= Quaternion.AngleAxis(45, Vector3.up);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, TotemRotation, 4 * smooth * Time.deltaTime);

    }
}
