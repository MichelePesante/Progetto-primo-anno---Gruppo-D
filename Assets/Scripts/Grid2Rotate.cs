using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2Rotate : MonoBehaviour {

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            StartCoroutine(RotateMe(Vector3.up * 90, 0.8f));
        }
        if (Input.GetKeyDown("u"))
        {
            StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));
        }
    }
}