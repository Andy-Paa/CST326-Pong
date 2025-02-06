using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pad : MonoBehaviour
{
    public float speed = 3f;
    public float padforce = 4f;
    public string inputAxis;

    public float collisionSpeed = 1.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        float max = bc.bounds.max.z;
        float min = bc.bounds.min.z;
        Debug.Log($"max = {max}, min = {min}");
    }

    void Update()
    {
        float input = Input.GetAxis(inputAxis);
        Transform paddle = GetComponent<Transform>();
        Vector3 newPosition = paddle.position + new Vector3(0f, 0f , input * speed * Time.deltaTime);
        newPosition.z = Mathf.Clamp(newPosition.z, -2.2f, 2.2f);
        paddle.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"we hit {collision.gameObject.name}");

        var padB = GetComponent<BoxCollider>().bounds;

        float max = padB.max.z;
        float min = padB.min.z;
        //Debug.Log($"max = {max}, min = {min}");

        float where = (collision.transform.position.z - min) / (max - min);
        //Debug.Log($"height = {where}");

        float redir = (where - 0.5f) * 1f;

        Vector3 v = collision.relativeVelocity;
        float rev = -Mathf.Sign(v.z);
        float newZ = -rev;

        float newSpeed = v.magnitude * collisionSpeed;
        Vector3 newV = new Vector3(rev, 0f, 0f) * newSpeed;
        newV = Quaternion.Euler(0f, newZ*50f*redir, 0f) * newV;
        collision.rigidbody.linearVelocity = newV;

        // // get reference to paddle collider
        // BoxCollider bc = GetComponent<BoxCollider>();
        // Bounds bounds = bc.bounds;
        // float maxX = bounds.max.x;
        // float minX = bounds.min.x;

        // Debug.Log($"maxX = {maxX}, minY = {minX}");
        // Debug.Log($"x pos of ball is {collision.transform.position.x}");

        // Quaternion rotation = Quaternion.Euler(0f, 0f, -60f);
        // Vector3 bounceDirection = rotation * Vector3.up;
        
        // Rigidbody rb = collision.rigidbody;
        // rb.AddForce(bounceDirection * 1000f, ForceMode.Force);
    }
}
