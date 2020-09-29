using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class findgps : MonoBehaviour
{
   
    //public Text coordinates;
    public Transform user;
    public Transform user2;
    public float latitude;
    public float longitude;
    //public int i = 0;
    int lat, log;
      void Start()
    {
        
       
        

        Input.location.Start();
        latitude = gpsf.Instance.latitude;
        longitude = gpsf.Instance.longitude;
        lat = (int)(latitude*1000000 - 77091625);
        log = (int)(longitude*1000000 - 28640149);
        user.position.Set(lat, 0, log);
        user2.position = user.position;
    }
    
    void Update()
    {
        
        Input.location.Start();
        latitude = gpsf.Instance.latitude;
        longitude = gpsf.Instance.longitude;
        lat = (int)(latitude - 77.091625);
        log = (int)(longitude - 28.640149);
        user.position.Set(lat,0,log);

    }
}
