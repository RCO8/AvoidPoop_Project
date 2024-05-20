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
        float screenLeft = 0f; // ȭ�� ���� ���
        float screenRight = screenWidth; // ȭ�� ������ ���
        float screenTop = screenHeight; // ȭ�� ��� ���
        float screenBottom = 0f; // ȭ�� �ϴ� ���

        Vector2 randomPosition = Vector2.zero; // �ʱ�ȭ�� ���� ��ġ
        // ȭ�� ���� ���� ��ġ�� �����ϱ� ���� ������ ��� ����
        int edge = Random.Range(0, 4);

        // ���õ� ��迡 ���� ���� ��ġ ����
        switch (edge)
        {
            case 0: // ����
                randomPosition = new Vector2(screenLeft - Random.Range(50f, 200f), Random.Range(screenBottom, screenTop));
                break;
            case 1: // ������
                randomPosition = new Vector2(screenRight + Random.Range(50f, 200f), Random.Range(screenBottom, screenTop));
                break;
            case 2: // ��
                randomPosition = new Vector2(Random.Range(screenLeft, screenRight), screenTop + Random.Range(50f, 200f));
                break;
            case 3: // �Ʒ�
                randomPosition = new Vector2(Random.Range(screenLeft, screenRight), screenBottom - Random.Range(50f, 200f));
                break;
        }

        return randomPosition; // ������ ���� ��ġ ��ȯ
    }


void ResultUI() //��� UI ���
    {

    }
}
