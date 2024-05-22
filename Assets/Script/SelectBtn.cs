using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBtn : MonoBehaviour
{
    public void SelectGame(int player)
    {
        PlayerPrefs.SetInt("Player", player);

        SceneManager.LoadScene("MainGameScene");
    }
}
