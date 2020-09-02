using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSet : MonoBehaviour
{
    public int CellSize=235;
    public GameObject blockPrefab;

    public GridLayoutGroup backgroundPanel;
    public GameObject[,] Backblocks;

    public Vector3 getPosition(int x,int y)
    {
        return Backblocks[x, y].transform.position;
    }
    public void SetBackBlock(int level)
    {
        Backblocks = new GameObject[level, level];
        CellSize = (1000 - (20 * (level - 1)))/level;
        backgroundPanel.cellSize = new Vector2(CellSize,CellSize);

        for(int i=0; i<level; i++)
        {
            for(int j=0; j < level; j++)
            {
                Backblocks[i,j]=Instantiate(blockPrefab, backgroundPanel.transform);
                
            }
        }
        
    }

    
}
