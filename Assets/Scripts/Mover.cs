using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //定一个公共变量speed用来调节子弹飞行的速度
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //在子弹生成时赋予它一个往前的速度
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
