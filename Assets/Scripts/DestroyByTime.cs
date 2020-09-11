using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    //定义一个公共变量lifeTime，来设置GameObject的存活时间，时间一到就会被销毁
    public float lifeTime;

    // 在Start方法中，调用Destroy方法来消除GameObject，第二个参数就是我们的lifeTime
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
