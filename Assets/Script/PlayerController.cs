using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Animator Anim;
    public float speed;
    public float jumpforce;
    //彭地面监测
  
    private int groundLayerMask;
    public Collider coll;
    public int Cherry = 0;
    public Text CherryNumber;
    private bool isHurt;//布尔的默认是false噢
    private bool isGround = false;//跳跃射线检测用

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()//不同设备平滑运行
    {
        if (!isHurt)
        {
            Movement();
        }
        RaycastHit hitinfo;
        isGround = Physics.Raycast(transform.position -Vector3.up * 2.9f, Vector3.down, out hitinfo, 0.2f, groundLayerMask);
        // ifIsGround();
        SwitchAnim();


    }
    void Movement()
    {   //move
        float horizontalmove;
        horizontalmove = Input.GetAxis("Horizontal");//-1 to 1
        float facedirection = Input.GetAxisRaw("Horizontal");//-1,0,1
        float verticalmove;
        verticalmove = Input.GetAxis("Vertical");
      

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
        Anim.SetFloat("running", Mathf.Abs(facedirection));//动画切换的condition
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector3(horizontalmove * speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        }
        if (verticalmove!=0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, verticalmove * speed * Time.deltaTime);
        }


        //jump
         if (Input.GetButtonDown("Jump") &&isGround)
       // if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpforce * Time.deltaTime,rb.velocity.z);
            Anim.SetBool("jumping", true);//动画切换的condition
        }
       
    }
    void SwitchAnim()//这个函数用来解决按下跳跃键后jump-fall-idle的转化
    {
        Anim.SetBool("idle", false);

        if (isHurt)
        {
            /*if (Mathf.Abs(rb.velocity.x) < 3f)//判断运动停止了
            {
                isHurt = false;
            }*/
            Invoke("Return", 0.5f);
            //这个elseif要做判断是否哦碰地前面，干脆最前面，什么事都不用多考虑

        }
        else if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y <= 0)
            {
                Anim.SetBool("jumping", false);//用两个限制，因为只满足一个的状态可不止一个
                Anim.SetBool("falling", true);
            }


        }

        else if (isGround)
        {
            Anim.SetBool("falling", false);
            Anim.SetBool("idle", true);
        }

    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)//用tag找到collider，因为不止一个嘛//z这个不用调用
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry++;
            CherryNumber.text = Convert.ToString(Cherry);
        }

    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Anim.GetBool("falling"))//写法有轻微区别
            {
                Destroy(collision.gameObject);
                //来个小跳，放在这里，如果在上一级，会一直保持下落动画在弹跳（？），为什么捏？啊因为绕过了判断?
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                Anim.SetBool("jumping", true);//动画切换的condition
            }

            else if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(-10, rb.velocity.y);
            }
            else if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(10, rb.velocity.y);
            }

        }


    }
    private void Return()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        isHurt = false;

    }
    /*private void ifIsGround()
    { //判断是否在地面上
        RaycastHit hitinfo;
        isGround = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hitinfo, 0.2f, ground);

    }
    */
}


