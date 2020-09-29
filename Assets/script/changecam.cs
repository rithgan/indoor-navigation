using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changecam : MonoBehaviour
{
    // Start is called before the first frame update
    public Button bt;
    public Camera cam1,cam2;
 void  Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    void Update()
    {
        Button btn = bt.GetComponent<Button>();
        btn.onClick.AddListener(Ab);
        
          
        
    }
    private void Ab()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
