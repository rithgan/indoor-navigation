using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class gpsf : MonoBehaviour
{
    // Start is called before the first frame update
    //public static gpsf Instance(set;)
     public static gpsf Instance { set; get; }
    public float latitude;
    public float longitude;
    
    void Start()
    {
        Instance = this;
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Debug.Log("yes");

        }
        else
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
    void Update()
    {
        Input.location.Start();
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
    }

    }
