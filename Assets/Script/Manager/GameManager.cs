using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
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

    // �÷��̾ �ݰ濡�� ������ ���ϴ� �Լ�
    public float spawnRandius = 10;

    // ���ʹ� �ð� �ֱ�.
    float spawntime;
    // ��ü �ð�
    float time;
    float score;

    float spawnInterval = 1f;   // ���� ���� (��)
    float timeSinceLastSpawn;   // ������ ���� ���� ��� �ð�

    public Text timeTxt;
    //public Text NowScore;
    //public Text BestScore;
    [SerializeField] private Text bulletCountTxt;

    bool isPlay = true;
    // ���� ī��Ʈ�� ���� �Լ�
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

            bulletCountTxt.text = BulletCount.ToString();
        }

        // ȭ�� �ۿ��� �����ϰ� �����Ǵ� �Ѿ� ����� - ȭ��� ���� ��ǥ ����

        timeSinceLastSpawn += Time.deltaTime; //��� �ð� ������Ʈ

        if (timeSinceLastSpawn >= spawnInterval)  //���� �ð� �������� �����ϱ�
        {
            Vector2 randomPosition = GenerateRandomPositionOutsideScreen(); //ȭ�� �� ���� ��ġ ����
            Debug.Log("Random Position Outside Screen: " + randomPosition); //������ ��ġ �ַܼ� ����ϱ� (��ǥ)

            obj = CurrentObjectPool.LinkedSpawnFromPool(windowOutEnemyTag);
            obj.transform.position = randomPosition;

            timeSinceLastSpawn = 0f; //�����ð� �ʱ�ȭ
        }
    }

    Vector2 GenerateRandomPositionOutsideScreen() 
    {
        Vector2 screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f)); // ȭ�� ������ ���
        Vector2 screenLeft = -screenRight; // ȭ�� ���� ���

        Vector2 screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height)); ; // ȭ�� ��� ���
        Vector2 screenBottom = -screenTop; // ȭ�� �ϴ� ���

        Vector2 randomPosition = Vector2.zero; // �ʱ�ȭ�� ���� ��ġ
        // ȭ�� ���� ���� ��ġ�� �����ϱ� ���� ������ ��� ����
        int edge = Random.Range(0, 4);

        // ���õ� ��迡 ���� ���� ��ġ ����
        switch ((EnemyPos)edge)
        {
            case EnemyPos.LEFT: // ����
                randomPosition = new Vector2(screenLeft.x - 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case EnemyPos.RIGHT: // ������
                randomPosition = new Vector2(screenRight.x + 1f, Random.Range(screenBottom.y, screenTop.y));
                break;
            case EnemyPos.UP: // ��
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenTop.y + 1f);
                break;
            case EnemyPos.DOWN: // �Ʒ�
                randomPosition = new Vector2(Random.Range(screenLeft.x, screenRight.x), screenBottom.y - 1f);
                break;
            case EnemyPos.RANDEOM:
                randomPosition = CreateEnemy();
                break;
            default:
                Debug.Log("GenerateRandomPosition�� ������ ���� GameManager�� ������ ������"); 
                break;
        }

        return randomPosition; // ������ ���� ��ġ ��ȯ
    }


void ResultUI() //��� UI ���
    {

    }

   
    Vector2 CreateEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRandius;
        Vector2 spawnPosition = randomCircle +(Vector2)Player.transform.position;
        return spawnPosition;
    }
}
