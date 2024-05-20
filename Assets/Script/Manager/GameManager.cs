using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    float time;
    float score;

    float screenWidth = 1920f;  //현재 스크린 넓이
    float screenHeight = 1080f; //현재 스크린 높이

    float spawnInterval = 1f;   // 생성 간격 (초)
    float timeSinceLastSpawn;   // 마지막 생성 이후 경과 시간

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

        // 화면 밖에서 랜덤하게 생성되는 총알 만들기 - 화면밖 랜덤 좌표 생성

        timeSinceLastSpawn += Time.deltaTime; //경과 시간 업데이트

        if (timeSinceLastSpawn >= spawnInterval)  //일정 시간 간격으로 실행하기
        {
            Vector2 randomPosition = GenerateRandomPositionOutsideScreen(); //화면 밖 랜덤 위치 생성
            Debug.Log("Random Position Outside Screen: " + randomPosition); //생성된 위치 콘솔로 출력하기 (좌표)
            timeSinceLastSpawn = 0f; //생성시간 초기화
        }
    }

    Vector2 GenerateRandomPositionOutsideScreen() 
    {
        float screenLeft = 0f; //화면 왼쪽 경계
        float screenRight = screenWidth; //화면 오른쪽 경계
        float screenTop = screenHeight; //화면 윗쪽 경계
        //float screenBottom = 0f; //화면 아랫쪽 경계

        // 위쪽 경계 선택
        float screenBottom = screenTop;

        Vector2 randomPosition = Vector2.zero; // 초기화된 랜덤 위치

        // 선택된 경계에 따라 랜덤 위치 설정
        randomPosition = new Vector2(Random.Range(screenLeft, screenRight), screenTop + Random.Range(50f, 200f));

        return randomPosition; // 생성된 랜덤 위치 반환
    }

    void ResultUI() //결과 UI 출력
    {

    }
}
