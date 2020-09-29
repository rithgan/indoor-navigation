using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class follow_camera : MonoBehaviour
{
   
   
    public Transform trg;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    // Update is called once per frame
    
    private void FixedUpdate()
    {
        
        
            Vector3 desiredPosition = trg.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
           transform.LookAt(trg);
        
            }

}
