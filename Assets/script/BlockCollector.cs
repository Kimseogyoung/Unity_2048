using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollector: MonoBehaviour
{
    public List<Block> blockTypes;

    public void SetBlockSize(Vector2 vec)
    {
        //블록 프리펩을 레벨에 맞는 사이즈로 변환
        for(int i=0; i < blockTypes.Count; i++)
        {
            blockTypes[i].GetComponentInChildren<RectTransform>().sizeDelta = vec;
        }
    }
}
