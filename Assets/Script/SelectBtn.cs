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
}
