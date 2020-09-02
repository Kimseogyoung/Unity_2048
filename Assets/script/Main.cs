using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    public int curLevel = 3;
    public TextMeshProUGUI levelText;

    
    public void nextLevel()
    {
        SoundManager.instance.playButtonClick();
        curLevel = curLevel+1 > 5 ? 3 : curLevel+1;
        levelText.text = curLevel + " x " + curLevel;
    }

    public void prevLevel()
    {
        SoundManager.instance.playButtonClick();
        curLevel = curLevel - 1 < 3 ? 5 : curLevel-1;
        levelText.text = curLevel + " x " + curLevel;

    }

    public void MoveNextScene()
    {
        Debug.Log("클릭");
        SoundManager.instance.playButtonClick();
        SetGameLevel.instance.setLevel(curLevel);
        SceneManager.LoadScene("game");
    }
    public void changedSoundToggle(Toggle toggle)
    {
        Debug.Log("클릭");
        SoundManager.instance.playButtonClick();
        SoundManager.instance.SoundOnOff(toggle.isOn);
    }
}
