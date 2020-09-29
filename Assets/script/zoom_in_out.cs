using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom_in_out : MonoBehaviour
{
    Camera mainCamera;
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    Vector3 ftouchPrevPos;
    Vector3 vector1 = new Vector3(0,0 ,2);
    Vector3 vector2 = new Vector3(1, 300, -3);
    [SerializeField]
    float zoomModifierSpeed = 0.125f;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

    }
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;
         
            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                mainCamera.orthographicSize += zoomModifier;
                mainCamera.transform.position = vector2;
            }
            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                mainCamera.orthographicSize -= zoomModifier;
                
                if (mainCamera.transform.position.x <= 50 && mainCamera.transform.position.z <= 50 && mainCamera.transform.position.x >= -50 && mainCamera.transform.position.z >= -50)
                {
                    if (firstTouch.position.y >= 300)
                        mainCamera.transform.position += vector1;
                   // if (firstTouch.position.y <= 300)
                       // mainCamera.transform.position -= vector1;

                }
            }
            }
        /*else if(Input.touchCount == 1)
        {
            Touch fTouch = Input.GetTouch(0);
           // ftouchPrevPos = 
            //ftouchPrevPos.y = 0f;
            if(mainCamera.transform.position.x<=500&& mainCamera.transform.position.y <= 500&& mainCamera.transform.position.x>= 0)
                mainCamera.transform.position += vector1 ; 
        }*/
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 100f, 267f);


    }
}

    
