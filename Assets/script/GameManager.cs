using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int level = 3;

    public Score score;
    public BlockManager blockmanager;

    public GameObject finalPopup;
    public GameObject stopPopup;
    public GameObject endingPopUp;
    public GameObject resetButton;


    private void Awake()
    {
        Screen.SetResolution(1080, 1920,true);
    }
    void Start()
    {
        level = SetGameLevel.instance.getLevel();
        Destroy(SetGameLevel.instance.gameObject);
        blockmanager.init(level);
        StartCoroutine(runGame());

    }

    IEnumerator runGame()
    {
        bool isrun=true;
        while (isrun)
        {
            if (blockmanager.isMake2048())//2048완성
            {
                isrun = false;
                endingPopUp.SetActive(true);
                resetButton.GetComponent<Button>().interactable = false;//리셋버튼 비활성화
                SoundManager.instance.SoundOnOff(false);
            }
            else if (blockmanager.isGameOver())//게임오버
            {
                isrun = false;
                finalPopup.SetActive(true);//팝업창띄우기
                resetButton.GetComponent<Button>().interactable = false;//리셋버튼 비활성화
                SoundManager.instance.SoundOnOff(false);
            }

            int sc = 0;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoundManager.instance.playSlide();
                sc +=blockmanager.moveRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SoundManager.instance.playSlide();
                sc += blockmanager.moveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SoundManager.instance.playSlide();
                sc += blockmanager.moveUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundManager.instance.playSlide();
                sc += blockmanager.moveDown();
            }

            score.UpdateScore(sc);
            yield return null;
        }

        

    }
    public void gotoMainButtonClick()//메인화면으로
    {   
        SceneManager.LoadScene("main");
        SoundManager.instance.SoundOnOff(true);
        SoundManager.instance.playButtonClick();
    }

    public void resetButtonClick()//블록 리셋
    {
        SoundManager.instance.playButtonClick();
        blockmanager.ResetBlocks();
        score.SetScoreZero();
    }
    public void stopButtonClick()
    {
        SoundManager.instance.playButtonClick();
        StopAllCoroutines(); //코루틴 종료
        stopPopup.SetActive(true);//팝업창띄우기
        resetButton.GetComponent<Button>().interactable = false;//리셋버튼 비활성화
    }
    public void continueButtonClick()
    {
        SoundManager.instance.playButtonClick();
        stopPopup.SetActive(false);//팝업창지우기
        resetButton.GetComponent<Button>().interactable = true;//리셋버튼 활성화
        StartCoroutine(runGame());
    }

    public void changedSoundToggle(Toggle toggle)
    {
        SoundManager.instance.playButtonClick();
        SoundManager.instance.SoundOnOff(toggle.isOn);
    }
}
