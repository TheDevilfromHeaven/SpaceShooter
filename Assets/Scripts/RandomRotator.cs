using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    //定义一个公共变量tumble来设置障碍翻滚的幅度
    public float tumble;

    //在Start函数中，通过随机数来随机设置障碍翻滚的速率
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
