using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameLevel : MonoBehaviour
{
    public static SetGameLevel instance;
    private int level = 3;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);

    }

    public int getLevel()
    {
        return level;
    }
    public void setLevel(int lv)
    {
        if (lv < 3 || lv > 5)
        {
            Debug.Log("레벨설정 오류");
        }
        else level = lv;
    }
}
