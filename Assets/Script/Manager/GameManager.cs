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

    // �÷��̾� �������� �Լ�
    
    //[SerializeField] private GameObject enemy;

    // �÷��̾ ���� ��ġ����
    public Transform Player { get; private set; }

    public float spawnRandius = 10;

    // ���ʹ� �ð� �ֱ�.
    float spawntime;
    // ��ü �ð�
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

    void ResultUI() //��� UI ���
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
            // �÷��̾� �ֺ����� ���������ϱ����ؼ�
            case EPosEnemy.RANDEOM:
                CreateEnemy();
                break;
            default:
                Debug.Log("ReSpawn�Լ��� �������ִ� �Ŵ�.");
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
