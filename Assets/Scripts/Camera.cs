using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraTarget;  

    public Vector3 offset = new Vector3 (-11f, 3f, -10f);

    public Vector3 velocity = new Vector3 (0, 0, 0);

    public float dampingTime = 0.3f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void Reset()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(cameraTarget.position.x-offset.x, offset.y, offset.z);   
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position,destination, ref velocity, dampingTime);   
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
