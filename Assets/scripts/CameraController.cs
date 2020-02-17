using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float scrollSpeed = 50;
    float initialX = 0;

    private bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            initialX = Input.mousePosition.x;
            isRotating = true;
        }
        if (isRotating) transform.RotateAround (Vector3.zero, Vector3.up, (Input.mousePosition.x - initialX ) * Time.deltaTime * speed);
        if (Input.GetMouseButtonUp (0)) isRotating = false;

        transform.localPosition += transform.forward * Input.mouseScrollDelta.y * Time.deltaTime * -scrollSpeed;

        transform.LookAt (Vector3.zero, Vector3.up);
    }
}
