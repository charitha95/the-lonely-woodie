using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoomIn : MonoBehaviour
{
    private int zoom = 20;
    private int normal = 60;
    private float smooth = 5;
    private bool isZoomed = false;

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
        }
        if (!isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }

    //public float smoothSpeed = 9;
    //public Vector3 offset;
    //private bool isZoomed = false;

    //private void FixedUpdate()
    //{
    //    Debug.Log("hmmm ss");
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, transform.position + offset, smoothSpeed);
    //    transform.position = smoothedPosition;
    //    isZoomed = true;
    //}
}