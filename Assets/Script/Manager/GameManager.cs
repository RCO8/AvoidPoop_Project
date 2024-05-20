using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    float time;
    float score;

    float screenWidth = 1920f;  //���� ��ũ�� ����
    float screenHeight = 1080f; //���� ��ũ�� ����

    float spawnInterval = 1f;   // ���� ���� (��)
    float timeSinceLastSpawn;   // ������ ���� ���� ��� �ð�

    public Text timeTxt;
    //public Text NowScore;
    //public Text BestScore;

    bool isPlay = true;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
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
        }

        // ȭ�� �ۿ��� �����ϰ� �����Ǵ� �Ѿ� ����� - ȭ��� ���� ��ǥ ����

        timeSinceLastSpawn += Time.deltaTime; //��� �ð� ������Ʈ

        if (timeSinceLastSpawn >= spawnInterval)  //���� �ð� �������� �����ϱ�
        {
            Vector2 randomPosition = GenerateRandomPositionOutsideScreen(); //ȭ�� �� ���� ��ġ ����
            Debug.Log("Random Position Outside Screen: " + randomPosition); //������ ��ġ �ַܼ� ����ϱ� (��ǥ)
            timeSinceLastSpawn = 0f; //�����ð� �ʱ�ȭ
        }
    }

    Vector2 GenerateRandomPositionOutsideScreen() 
    {
        float screenLeft = 0f; //ȭ�� ���� ���
        float screenRight = screenWidth; //ȭ�� ������ ���
        float screenTop = screenHeight; //ȭ�� ���� ���
        //float screenBottom = 0f; //ȭ�� �Ʒ��� ���

        // ���� ��� ����
        float screenBottom = screenTop;

        Vector2 randomPosition = Vector2.zero; // �ʱ�ȭ�� ���� ��ġ

        // ���õ� ��迡 ���� ���� ��ġ ����
        randomPosition = new Vector2(Random.Range(screenLeft, screenRight), screenTop + Random.Range(50f, 200f));

        return randomPosition; // ������ ���� ��ġ ��ȯ
    }

    void ResultUI() //��� UI ���
    {

    }
}
