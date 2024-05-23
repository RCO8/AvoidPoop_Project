using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBtn : MonoBehaviour
{
    public void SelectGame()
    {
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
