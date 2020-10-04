using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float speed = 500f;
    private bool isRotate = true;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(isRotate)
        transform.Rotate(Vector3.back, speed * Time.deltaTime);//旋转方向+速度
    }
    public void stop()
    {
        isRotate = false;
    }
    public void start()
    {
        isRotate = true;
    }
}
