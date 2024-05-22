using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum EnemyPos
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    RANDEOM
}

public class GameManager : MonoBehaviour
{
    //���ӸŴ����� ���� �ν��Ͻ��ϴ� ����
    public static GameManager Instance;

    // �÷��̾ ���� ��ġ����
    public Transform Player;

    public GameObject powerItemspawnTime;
    public GameObject speedItemspawnTime;
    public GameObject invicibilItemspawnTime;

    // �÷��̾ �ݰ濡�� ������ ���ϴ� �Լ�
    public float spawnRandius = 10;

    // ���ʹ� �ð� �ֱ�.
    float spawntime;
    // ��ü �ð�
    float time;
    float bestScore;

    float spawnInterval = 1f;   // ���� ���� (��)
    float timeSinceLastSpawn;   // ������ ���� ���� ���?�ð�

    public Text timeTxt;
    public Text BestScore;
    [SerializeField] private Text bulletCountTxt;
    [SerializeField] private GameObject resultUI;
    [SerializeField] private Text currentScoreTxt;
    [SerializeField] private Text bestScoreTxt;

    int nowPlayer = 1;   //�÷��̾� �ο�
    [SerializeField] private GameObject Movement2;

    bool isPlay = true;
    // ������ ī��Ʈ�� ���� �Լ�
    public int BulletCount { get; set; } = 0;

    public ObjectPool CurrentObjectPool { get; private set; }

    private GameObject obj;
    [SerializeField] private string windowOutEnemyTag;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;

        CurrentObjectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        isPlay = true;

        if (PlayerPrefs.HasKey("bestScore"))
            bestScore = PlayerPrefs.GetFloat("bestScore");
        else
            bestScore = 0f;

        BestScore.text = bestScore.ToString("N2");


        InvokeRepeating("PowerItemSpawnTime", 3f, 7f); // ?�워???�이???�폰 간격
        InvokeRepeating("SpeedItemSpawnTime", 3f, 5f); // ?�피?�업 ?�이???�폰 간격
        InvokeRepeating("InvincibilItemSpawnTime", 5f, 15f); // 무적 ?�이???�폰 간격

        if (PlayerPrefs.HasKey("Player"))
        {
            nowPlayer = PlayerPrefs.GetInt("Player");
            if (nowPlayer == 2) Movement2.SetActive(true);
            else Movement2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");

            spawntime = time;

            if (spawntime > 1.5f)
            {
                //ReSpawn();
            }

            bulletCountTxt.text = BulletCount.ToString();

            timeSinceLastSpawn += Time.deltaTime; 

            if (timeSinceLastSpawn >= spawnInterval) 
            {
                Vector2 randomPosition = GenerateRandomPositionOutsideScreen(); 
                //Debug.Log("Random Position Outside Screen: " + randomPosition); 

                obj = CurrentObjectPool.LinkedSpawnFromPool(windowOutEnemyTag);
                obj.transform.position = randomPosition;

                timeSinceLastSpawn = 0f;
            }
        }
    }

    Vector2 GenerateRandomPositionOutsideScreen()
    {
        Vector2 screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f)); 
        Vector2 screenLeft = -screenRight; 

        Vector2 screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height)); 
        Vector2 screenBottom = -screenTop;

        Vector2 randomPosition = Vector2.zero; 
        
        int edge = Random.Range(0, 5);

        switch ((EnemyPos)edge)
        {
            case EnemyPos.LEFT: 
                randomPosition = new Vector2(screenLeft.x - 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case EnemyPos.RIGHT: 
                randomPosition = new Vector2(screenRight.x + 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case EnemyPos.UP: 
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenTop.y + 1f);
                break;
            case EnemyPos.DOWN:
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenBottom.y - 1f);
                break;
            case EnemyPos.RANDEOM:
                randomPosition = CreateEnemy();
                break;
            default:
                Debug.Log("GenerateRandomPosition Error");
                break;
        }

        return randomPosition; 
    }

    public void PowerItemSpawnTime() // ?�워???�이???�폰�?
    {
        int a = Random.RandomRange(0, 4);
        if (a == 0)
        {
            GameObject itemSpawn = Instantiate(powerItemspawnTime);
            itemSpawn.SetActive(true);
        } 
    }

    public void SpeedItemSpawnTime() // ?�피?�업 ?�이???�폰�?
    {
        int a = Random.RandomRange(0, 4);
        if (a == 0)
        {
            GameObject itemSpawn = Instantiate(speedItemspawnTime);
            itemSpawn.SetActive(true);
        }
    }

    public void InvincibilItemSpawnTime() // 무적 ?�이???�폰�?
    {
        int a = Random.RandomRange(0, 4);
        if (a == 0)
        {
            GameObject itemSpawn = Instantiate(invicibilItemspawnTime);
            itemSpawn.SetActive(true);
        }
    }

    void ResultUI()
    {
        if (bestScore < time)
        {
            bestScore = time;
            PlayerPrefs.SetFloat("bestScore", bestScore);
        }

        currentScoreTxt.text = time.ToString("N2");
        bestScoreTxt.text = bestScore.ToString("N2");

        resultUI.SetActive(true);
    }

    Vector2 CreateEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRandius;
        Vector2 spawnPosition = randomCircle + (Vector2)Player.transform.position;
        return spawnPosition;
    }

    public void EndGame()
    {
        isPlay = false;

        ResultUI();

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
