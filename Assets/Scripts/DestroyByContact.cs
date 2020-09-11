using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //定义了一个公共变量，用来设置爆炸的效果
    public GameObject explosion;
    //定一个公共变量playerExplosion，来设置飞船爆炸的效果
    public GameObject playerExplosion;
    //定义一个公共变量score，来设置每个障碍的击碎后的得分
    public int score;
    //定义一个私有变量gameController，因为我们需要通过调用GameController脚本中的addScore方法来增加分数
    private GameController gameController;

    //在Start方法中，需要获取GameController的实例，这里我们的步骤是，首先找到GameController的GameObject，然后在通过GetComponent方法来获取GameController脚本的实例
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        //这里面有两个if语句，第一个if是表示当我们获取的GameController对象不为空时，我才通过GetComponent方法来获取GameController脚本的实例
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        //第二个if是表示当我们获取的GameController对象是空的时候，我们输出Debug信息，告诉我们脚本没有找到
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    //利用OnTriggerEnter方法来检测障碍是否和子弹碰撞
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }
        //利用Instantiate方法生成一个爆炸效果
        Instantiate(explosion, transform.position, transform.rotation);
        //在碰撞里面进行判断，如果碰撞对象的Tag为Player，也就是我们的飞船，我们就在飞船的位置播放一个爆炸效果
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        }
        //在障碍被击碎的逻辑里面，调用GameController的addScore方法来增加分数
        gameController.addScore(score);
        //删除子弹
        Destroy(other.gameObject);
        //将障碍也删除
        Destroy(gameObject);
    }
}
