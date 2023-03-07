using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    float xAxis;
    float yAxis;

    Vector3 moveVector;

    public float MoveSpeed = 10.0f;
    public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;

    // No Sprint
    // No Jump

    private void Awake() 
    {
        
    }

    private void Start() 
    { 
    
    }

    private void Update()
    {
        Move();
    }

    private void Move() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(xAxis, 0, yAxis).normalized;
        transform.position += moveVector * MoveSpeed * Time.deltaTime;
    }

    private void Dead() { 
        
    }

    private void Dodge() 
    {
    
    }

    private void FixedUpdate() {
    
    }
}
