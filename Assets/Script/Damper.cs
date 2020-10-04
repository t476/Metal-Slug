using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damper : MonoBehaviour
{
    public float speed = 120f;
    private  bool isRotate =false;
    private float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        if (isRotate)
        {
            transform.Rotate(Vector3.forward,speed * Time.deltaTime);
            angle += speed * Time.deltaTime;//这个对于角度的累加，放在update里调用。。hummmm。。。。
            if (angle >= 90)
            {
                isRotate = false;
            }
        }
    }
    public void StartRotate()
    {
        isRotate = true;
    }
}
