using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBtn : MonoBehaviour
{
    public void SelectGame()
    {
        //�� Ű �Ӽ����� ���� �ڵ����� ����
        if(!PlayerPrefs.HasKey("Player"))
            PlayerPrefs.SetInt("Player", 1);
        if (!PlayerPrefs.HasKey("Difficalty"))
            PlayerPrefs.SetString("Difficalty", "Easy");

        SceneManager.LoadScene("MainGameScene");
    }

    public void SetPlayer(int player)
    {
        PlayerPrefs.SetInt("Player", player);
    }

    public void SetDifficalty(string diff)
    {
        PlayerPrefs.SetString("Difficalty", diff);
    }
}
