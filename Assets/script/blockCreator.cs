using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class blockCreator : MonoBehaviour
{
    public Block block2Prefab;
    public GameObject backGroundPanel;
    
    public Block createBlock(Vector3 vec)
    {
        Block block=Instantiate(block2Prefab, backGroundPanel.transform);
        block.transform.position = new Vector3(vec.x, vec.y);
        
        return block;
    }
}
