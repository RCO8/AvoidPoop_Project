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
    public static GameManager Instance;

    // 플레이어 가져오는 함수

    //[SerializeField] private GameObject enemy;

    // 플레이어에 대한 위치정보
    public Transform Player;

    public float spawnRandius = 10;

    // 에너미 시간 주기.
    float spawntime;
    // 전체 시간
    float time;
    float score;

    float spawnInterval = 1f;   // 생성 간격 (초)
    float timeSinceLastSpawn;   // 마지막 생성 이후 경과 시간

    public Text timeTxt;
    //public Text NowScore;
    //public Text BestScore;
    [SerializeField] private Text bulletCountTxt;

    bool isPlay = true;

    public int BulletCount { get; set; } = 0;

    public ObjectPool CurrentObjectPool { get; private set; }

    private GameObject obj;
    [SerializeField] private string windowOutEnemyTag;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        CurrentObjectPool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

            //bulletCountTxt.text = BulletCount.ToString();
        }

        // 화면 밖에서 랜덤하게 생성되는 총알 만들기 - 화면밖 랜덤 좌표 생성

        timeSinceLastSpawn += Time.deltaTime; //경과 시간 업데이트

        if (timeSinceLastSpawn >= spawnInterval)  //일정 시간 간격으로 실행하기
        {
            Vector2 randomPosition = GenerateRandomPositionOutsideScreen(); //화면 밖 랜덤 위치 생성
            Debug.Log("Random Position Outside Screen: " + randomPosition); //생성된 위치 콘솔로 출력하기 (좌표)

            //obj = CurrentObjectPool.LinkedSpawnFromPool(windowOutEnemyTag);
            //obj.transform.position = randomPosition;

            timeSinceLastSpawn = 0f; //생성시간 초기화
        }
    }

    Vector2 GenerateRandomPositionOutsideScreen() 
    {
        Vector2 screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f)); // 화면 오른쪽 경계
        Vector2 screenLeft = -screenRight; // 화면 왼쪽 경계

        Vector2 screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height)); ; // 화면 상단 경계
        Vector2 screenBottom = -screenTop; // 화면 하단 경계

        Vector2 randomPosition = Vector2.zero; // 초기화된 랜덤 위치
        // 화면 밖의 랜덤 위치를 생성하기 위한 랜덤한 경계 선택
        int edge = Random.Range(0, 4);

        // 선택된 경계에 따라 랜덤 위치 설정
        switch (edge)
        {
            case 0: // 왼쪽
                randomPosition = new Vector2(screenLeft.x - 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case 1: // 오른쪽
                randomPosition = new Vector2(screenRight.x + 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case 2: // 위
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenTop.y + 1f);
                break;
            case 3: // 아래
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenBottom.y - 1f);
                break;
        }

        return randomPosition; // 생성된 랜덤 위치 반환
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
