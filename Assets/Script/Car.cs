using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Vector3 targetPos;//车运动到目标位置
    public Vector3 endTargetPos;
    //通过插值运算来控制车运动的位置
    public int smoothing = 2;
    public Wheel[] wheelArray;
    public Damper D;
    private bool isReaching = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlaySound", 0.5f);//延迟一下播放刹车音效时间
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);//通过插值运算来控制车运动的位置
        if(!isReaching)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.4f)
            {
                isReaching = true;
                OnReach();
            }
        }
    }
    void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();

    }
    void  OnReach()//调用wheel里的stop方法
    {
        foreach(Wheel w in wheelArray)
        {
            w.stop();
        }
        D.StartRotate();
        //人走出去，车离开
        Invoke("GoOut", 2f);

    }
    void GoOut()
    {
        targetPos = endTargetPos;
        foreach (Wheel w in wheelArray)
        {
            w.start();
        }
        Destroy(this.gameObject, 2f);//开没后销毁车
    }

}
