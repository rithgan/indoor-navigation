using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.IO;
[System.Serializable]
public class data
{

    public int[] wall;
    public int number;
    public string text;
    public int x, y;
    public float ratio;
}
[System.Serializable]
public class Map
{
    // jsonLessonList is case sensitive and must match the string "jsonLessonList" in the JSON.
   public data[] floor;
    
}


// For showing/storing the result




public class try1 : MonoBehaviour
{
    
    public Text text1 ;
    public Text text2 ;
    public Text text3 ;
    public Text text4 ;
    public Text text5 ;
    public Text text6 ;
    string rt="no";
    public TextAsset textAsset;
    public ArrayList myList = new ArrayList();
    private GameObject cube;
    private Vector3 scaleChange, positionChange;
 
    public Transform tar;
    public InputField input;
    
    void Start()
    {
        
        int cntX, cntY,ctX,ctY;
        double m;
       // input.text = "no";
        
        Map mapInJson = JsonUtility.FromJson<Map>(textAsset.text);
        foreach (data walls in mapInJson.floor)
        {
            if (walls.text != "")
            {
                myList.Add(walls.text);
                myList.Add(walls.x);
                myList.Add(walls.y);

            }
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.layer = LayerMask.NameToLayer("Unwalkable");
            if (walls.wall[0] == walls.wall[1])
            {
                cntX = walls.wall[0];
                cntY = (walls.wall[2] + walls.wall[3]) / 2;
                ctY = walls.wall[2] - walls.wall[3];
                if (ctY < 0)
                {
                    ctY = ctY * -1;
                }
                cube.transform.position = new Vector3(cntX - 240, 0, cntY - 240);
                cube.transform.localScale = new Vector3(5, 50, ctY);
                
            }
            else if (walls.wall[2] == walls.wall[3])
            {
                cntX = (walls.wall[0] + walls.wall[1]) / 2;
                cntY = walls.wall[2];
                ctX = walls.wall[0] - walls.wall[1];
                if (ctX < 0)
                {
                    ctX = ctX * -1;
                }
                cube.transform.position = new Vector3(cntX - 240, 0, cntY - 240);
                cube.transform.localScale = new Vector3(ctX, 50, 5);
            }
            else
            {
                cntX = (walls.wall[0] + walls.wall[1]) / 2;
                cntY = (walls.wall[2] + walls.wall[3]) / 2;
                m = (walls.wall[3] - walls.wall[2]) / (walls.wall[1] - walls.wall[0]);

                m = Math.Atan(m);
                m = m * 180 / 3.14;


                ctY = walls.wall[2] - walls.wall[3];
                if (ctY < 0)
                {
                    ctY = ctY  * -1;
                }
                cube.transform.position = new Vector3(cntX - 240, 0, cntY - 240);
                cube.transform.localScale = new Vector3(50, 5, ctY);
                cube.transform.Rotate(0, 0, (float)m, Space.World);
            }
        }
        text1.text=(string)myList[0];
        text1.rectTransform.anchoredPosition=new Vector2((int) myList[1], (int)myList[2]);
        
        text2.text=(string)myList[3]; 
        text2.rectTransform.anchoredPosition = new Vector2((int)myList[4], (int)myList[5]);
        text3.text=(string)myList[6]; 
        text3.rectTransform.anchoredPosition = new Vector2((int)myList[7], (int)myList[8]);
        text4.text=(string)myList[9]; 
        text4.rectTransform.anchoredPosition = new Vector2((int)myList[10], (int)myList[11]);
        text5.text=(string)myList[12]; 
        text5.rectTransform.anchoredPosition = new Vector2((int)myList[13], (int)myList[14]);
        text6.text=(string)myList[15]; 
        text6.rectTransform.anchoredPosition = new Vector2((int)myList[16], (int)myList[17]);



    }
    void Update()
    {
        string lower;  
        if (input.text != rt)
        {
            for (int i = 0; i < myList.Count; i += 3)
            {
                lower = (string)myList[i];
                if(input.text==(string)myList[i]|| input.text == lower.ToLower())
                {
                    tar.position = new Vector3((int)myList[i + 1]-220, -5,(int) myList[i + 2]-240);
                }
            }
            rt = input.text;
        }  
            
                  
        
       
    }
    

   
} 

 
   

