using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //定义一个公共变量hazard，来设置生成的障碍对象
    public GameObject hazard;
    //继续定义一个公共变量spawnValue，它是一个Vector3的变量，用来设置障碍生成的位置，我们可以在编辑器中设置
    public Vector3 spawnValue;
    //定义了一个公共变量hazardCount，用来设置每次生成的障碍的数量
    public int hazardCount;
    //定义了一个公共变量spawnWait，用来设置每个障碍生成的间隔时间
    public float spawnWait;
    //定义了一个公共变量startWait，用来设置游戏开始生成障碍的准备时间，我们给予玩家一定准备时间
    public float startWait;
    //定义一个公共变量waveWait，用来设置每一波的间隔时间
    public float waveWait;

    //定义了一个私有变量score，来记录玩家的分数，因为我们不希望这个变量可以手动设置，所以设置为私有类型的
    private int score;
    //Text类型的变量属于UnityEngine.UI的内容
    //定义一个公共变量scoreText，用来设置显示分数的UI对象
    public Text scoreText;

    //定义一个公共变量gameOverText，用来设置显示GameOver的UI对象
    public Text gameOverText;
    //定义一个私有的变量，用来保存游戏是否结束的标志，它是Bool类型的变量，只有true和false两种情况
    private bool gameOver;
    //定义一个公共变量restartText，来设置显示重新开始信息的UI对象
    public Text restartText;
    //定义一个私有变量restart，来标记游戏是否处于重新开始状态
    private bool restart;

    // 在Start方法中调用SpawnWaves方法，因为很多我们自定义的方法，Unity都不会自动调用的，需要我们自己在合适的地方去调用
    //在游戏开始时，将score设置为0，接着调用UpdateScore方法来更新分数UI
    void Start()
    {
        //在游戏开始，将gameOverText的文字内容设置为空，因为一开始游戏肯定还没有结束，所以我们不能让玩家看到gameOverText
        gameOverText.text = "";
        //一开始我们也需要将gameOver标记为false，即游戏还未结束
        gameOver = false;
        //在游戏开始的时候，将restartText的文本内容设置为空，因为一开始不需要显示任何信息，同时将restart标记为false
        restartText.text = "";
        restart = false;
        //调用SpawnWaves方法也需要修改为StartCoroutine (SpawnWaves ())
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    //在Update方法中进行检测，如果游戏处于重新开始状态下，并且玩家按下了R键，我们就重新加载当前场景（即重新开始游戏）
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    //修改scoreText的文本内容，文本内容由两部分组成，Score：固定文字加上score变量数值
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    //定义一个公共的方法addScore，之所以是公共的，因为我会在其他的脚本中调用，所以需要将其设置为public类型，函数里面我们就只有两句话，将现在的分数加上增加的分数，然后再更新分数
    public void addScore(int value)
    {
        score += value;
        UpdateScore();
    }

    //定义了SpawnWaves方法
    //为了使用yield return new WaitForSeconds语句，我们还需要将SpawnWaves的返回值改为IEnumerator类型
    IEnumerator SpawnWaves()
    {
        //这里里面最重要的一个语句yield return new WaitForSeconds，这给方法就是让代码等待一定时间，这是个协同程序，可以让游戏不暂停的同时，让代码暂停
        //最开始，使用yield return new WaitForSeconds语句，让代码等待startWait秒，然后再开始执行下面的代码，这样就实现了游戏开始给予玩家准备时间的功能
        yield return new WaitForSeconds(startWait);
        //使用一个While循环，这里我们将里面的判断条件直接写为true，让它可以一直循环，然后将我们生成障碍的代码放入While循环中
        while (true)
        {
            //将生成的代码放入了一个for循环里面，循环的次数就是我们的hazardCount
            for (int i = 0; i < hazardCount; i++)
            {
                //在方法里面定义了一个局部变量spawnPosition，这个变量是用来记录障碍生成的位置
                //通过new Vector3的方法来新建了一个Vector3，而它的xyz值，我们对它进行了分别的设置，x值是在我们设置的spawnValue的x的[-x, x]之间随机，这样才能保证我们的障碍每次生成在不同的位置上
                //y和z我们直接调用spawnValue的y和z值，而spawnValue的y和z值是后面我们在编辑器里面进行设置，来确保障碍生成的位置在屏幕的上方
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                //定义了一个spawnRotation的变量，用来记录障碍的Rotation属性，在这里我们不需要让障碍一开始带着任何的旋转，所以我们直接赋值Quaternion.identity
                Quaternion spawnRotation = Quaternion.identity;
                //用Instantiate方法生成一个障碍实例了
                Instantiate(hazard, spawnPosition, spawnRotation);
                //接着我们在每一次循环结束后，使用yield return new WaitForSeconds语句，让代码等待spawnWait秒，然后再开始执行下一次循环，这样就实现了障碍生存间隔的效果
                yield return new WaitForSeconds(spawnWait);
                //在障碍生成的循环中，进行检测，如果游戏处于GameOver状态，我们就将restart标记为true，同时将restartText的文本内容设置为“Press 'R' to Restart”
                if (gameOver)
                {
                    restart = true;
                    restartText.text = "Press 'R' to Restart";
                }
            }
            //每次循环结束时，我们使用yield return new WaitForSeconds语句，让代码等待waveWait秒
            yield return new WaitForSeconds(waveWait);
        }
    }

    //定义一个公共方法GameOver，来实现游戏结束时的逻辑处理，在这个方法中，我们做了两件事情，一是将gameOver标记为true，然后将gameOverText的文字内容设置为“GameOver”
    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "GameOver";
    }
}
