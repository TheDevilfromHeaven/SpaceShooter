using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//序列化了一个Boundary类，用来保存飞船在X和Z轴上的范围，也就是xMin、xMax、zMin、zMax
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    //公共的变量speed
    public float speed;
    //添加一个公共变量tilt，来设置飞船倾斜的幅度
    public float tilt;
    //定义了一个变量boundary
    public Boundary boundary;
    //定义一个私有变量nextFire，用来记录下一发子弹的时间
    private float nextFire;
    //定义一个公共变量fireRate，用来控制子弹发射的间隔时间
    public float fireRate;
    //定义一个公共变量shot，用来保存我们发射的子弹Prefab，也就是告诉程序，我们发射的是哪个子弹
    public GameObject shot;
    //定义一个公共变量shotSqawn，用来保存子弹的发射点，也就是告诉程序，子弹从哪里发射出去
    public Transform shotSqawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //在Update函数中，判断用户是否有按下Fire1键，这边的Fire1键就是键盘上的左Ctrl键和鼠标左键，同时当前时间超过我们下一发子弹所需的时间，换句话来说，这个if的作用就是确保我们武器没有在冷却时间时按下发射键，才会发射子弹

     void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //当if条件通过后，我们先记录下一发子弹的时间，然后通过Instantiate方法生成一发子弹，他有3个参数，一个是发射的对象，一个是发射对象的position，一个是发射对象的rotation
            Instantiate(shot, shotSqawn.position, shotSqawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        //用两个变量moveHorizontal和moveVertical记录玩家输入的方向数据
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //新建一个Vector3类型的变量movement，并且将moveHorizontal和moveVertical分别赋值给movement的X和Z
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //获取Player的刚体的速度属性，将movement乘上speed的值赋给它
        GetComponent<Rigidbody>().velocity = movement * speed;
        //获取Player刚体属性的rotation，将我们移动的速度x值乘上-tilt赋值给rotation属性
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        //获取飞船的位置属性，然后通过Mathf.Clamp来确保飞船在X和Z轴上的范围处于我们设定的值内
        GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );
    }
}
