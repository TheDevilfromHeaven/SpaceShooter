using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本中，只做了一件事情，就是检测有子弹飞出这个盒子后，我们就将子弹销毁掉
public class DestroyByBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnTriggerExit是Unity内置方法，用来检测对象退出碰撞体时的操作
    private void OnTriggerExit(Collider other)
    {
        //Destroy作用是销毁一个对象
        Destroy(other.gameObject);
    }
}
