using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public enum EPosEnemy
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    RANDEOM
}



public class GameManager : MonoBehaviour
{

    [SerializeField] private string playertag;

    public static GameManager instance;

    // 플레이어 가져오는 함수
    
    //[SerializeField] private GameObject enemy;

    // 플레이어에 대한 위치정보
    public Transform Player { get; private set; }

    public float spawnRandius = 10;

    // 에너미 시간 주기.
    float spawntime;
    // 전체 시간
    float time;
    float score;

    public Text timeTxt;
    //public Text NowScore;
    //public Text BestScore;

    bool isPlay = true;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag(playertag).transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            spawntime = time;

            if(spawntime >1.5f)
            {
                //ReSpawn();
            }
        }
    }

    void ResultUI() //결과 UI 출력
    {

    }

    void ReSpawn()
    {
        int x = 10;
        int y = 10;

        int number = Random.Range(0, 5);

        switch ((EPosEnemy)number)
        {
            case EPosEnemy.UP:
                CreateEnemy(Random.Range(0, x * 2) - x, y);
                break;
            case EPosEnemy.DOWN:
                CreateEnemy(Random.Range(0, x * 2) - x, -y);
                break;
            case EPosEnemy.LEFT:
                CreateEnemy(-x, Random.Range(0, y * 2) - y);
                break;
            case EPosEnemy.RIGHT:
                CreateEnemy(x, Random.Range(0, y * 2) - y);
                break;
            // 플레이어 주변에서 랜덤생성하기위해서
            case EPosEnemy.RANDEOM:
                CreateEnemy();
                break;
            default:
                Debug.Log("ReSpawn함수가 문제가있는 거다.");
                break;
        }

    }
   
    void CreateEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRandius;
        Vector2 spawnPosition = randomCircle +(Vector2)Player.transform.position;
        //Instantiate(enemy, spawnPosition, Quaternion.identity);
    }


    void CreateEnemy(float x, float y)
    {
        //Instantiate(enemy, new Vector3(x, y), Quaternion.identity);
    }

}
