using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public BlockSet blockset;
    public blockCreator blockcreator;
    public BlockCollector blockcollector;

    public Block[,] blocks;
    private int level;
    private bool isEnding = false;
    
    public void init(int lv)
    {
        isEnding = false;
        level = lv;
        blockset.SetBackBlock(level);//배경블록세팅
        blocks = new Block[level, level];//현재 블록 배열생성
        blockcollector.SetBlockSize(new Vector2(blockset.CellSize, blockset.CellSize));
        Invoke("newBlock", 0.2f);
        
        
    }
    public void newBlock() {
        int ranx, rany;
        do
        {
            ranx = Random.Range(0, level);
            rany = Random.Range(0, level);
        }
        while (blocks[ranx, rany] != null);
        blocks[ranx,rany] = blockcreator.createBlock(blockset.getPosition(ranx,rany));
        blocks[ranx, rany].createBlock();//애니메이션

       
    }
    public void combineBlock(int r1,int c1,int r2,int c2)
    {

        Destroy(blocks[r2, c2].gameObject);
        blocks[r1, c1].Move(blockset.getPosition(r2, c2), blocks[r1, c1].gameObject);
        blocks[r2, c2] = Instantiate(blockcollector.blockTypes[blocks[r1, c1].number + 1],blockcreator.backGroundPanel.transform);
        blocks[r2, c2].transform.position = blockset.getPosition(r2, c2);

        Debug.Log("컴바인결과"+ (blocks[r1, c1].number + 1));
        blocks[r1, c1] = null;

        if ((blocks[r2, c2].number + 1) == 10)//2048완성일때
            isEnding = true;
        

    }
    public void moveBlock(int r1, int c1,int r2,int c2)
    {
        if (r1 == r2 && c1 == c2) return;

        
        blocks[r1, c1].Move(blockset.getPosition(r2, c2));
        blocks[r2, c2] = blocks[r1, c1];
        blocks[r1, c1] = null;
    }
    public int moveRight()
    {
        int score = 0;
        for(int r=0; r < level; r++)
        {
            for(int c = level-2; c >=0; c--)
            {
                if (blocks[r, c] == null)
                    continue;
                
                int t;
                for(t = c + 1; t < level; t++)
                {
                    if (blocks[r, t] != null)
                        break;
                }

                if (t >= level) //맨끝에 있는경우
                    moveBlock(r, c, r, level - 1);
                else if(blocks[r,c].number==blocks[r,t].number)//같은경우
                {
                    score += (int)Mathf.Pow(2, blocks[r, c].number+2);                        
                    combineBlock(r, c, r, t);
                }
                else
                    moveBlock(r, c, r, t - 1);

               
            }
        }
        if (!isfull()) newBlock();
        return score;
    }
    public int moveLeft()
    {
        int score = 0;
        for (int r = 0; r < level; r++)
        {
            for (int c = 1; c <level; c++)
            {
                if (blocks[r, c] == null)
                    continue;

                int t;
                for (t = c -1; t >=0; t--)
                {
                    if (blocks[r, t] != null)
                        break;
                }

                if (t <0) //맨끝에 있는경우
                    moveBlock(r, c, r, 0);
                else if (blocks[r, c].number == blocks[r, t].number)//같은경우
                {
                    score += (int)Mathf.Pow(2, blocks[r, c].number + 2);
                    combineBlock(r, c, r, t);
                }
                else
                    moveBlock(r, c, r, t + 1);


            }
        }
        if (!isfull()) newBlock();
        return score;
    }
    public int moveUp()
    {
        int score = 0;
        for (int c = 0; c < level; c++)
        {
            for (int r = 1; r < level; r++)
            {
                if (blocks[r, c] == null)
                    continue;

                int t;
                for (t = r - 1; t >= 0; t--)
                {
                    if (blocks[t, c] != null)
                        break;
                }

                if (t < 0) //맨끝에 있는경우
                    moveBlock(r, c, 0, c);
                else if (blocks[r, c].number == blocks[t, c].number)//같은경우
                {
                    score += (int)Mathf.Pow(2, blocks[r, c].number + 2);
                    combineBlock(r, c, t, c);
                }
                else
                    moveBlock(r, c, t+1, c);
            }
        }
        if(!isfull()) newBlock();

        return score;
    }
    public int moveDown()
    {
        int score = 0;
        for (int c = 0; c < level; c++)
        {
            for (int r = level-2; r >=0 ; r--)
            {
                if (blocks[r, c] == null)
                    continue;

                int t;
                for (t = r + 1; t <level; t++)
                {
                    if (blocks[t, c] != null)
                        break;
                }

                if (t >= level) //맨끝에 있는경우
                    moveBlock(r, c, level-1, c);
                else if (blocks[r, c].number == blocks[t, c].number)//같은경우
                {
                    score += (int)Mathf.Pow(2, blocks[r, c].number + 2);
                    combineBlock(r, c, t, c);
                }
                else
                    moveBlock(r, c, t-1, c);

            }
        }
        if(!isfull()) newBlock();
        return score;
    }

    public void ResetBlocks()
    {
        for(int r=0; r<level; r++)
        {
            for(int c=0; c < level; c++)
            {
                if (blocks[r, c] != null)
                    Destroy(blocks[r, c].gameObject);
            }
        }
        blocks.Initialize();//배열 초기화
        newBlock();
       
    }

    public bool isfull()
    {
        for(int i=0; i < level; i++)
        {
            for(int j=0; j < level; j++)
            {
                if (blocks[i, j] == null)
                    return false;  
            }
        }
        return true;
    }

    public bool isGameOver()
    {   if (!isfull()) return false;

        for(int i=0; i<level; i++)
        {
            for(int j=0; j<level-1; j++)
            {
                if (blocks[i, j].number == blocks[i, j + 1].number)
                    return false;
                if (blocks[j, i].number == blocks[j + 1, i].number)
                    return false;
            }
        }
        return true;
    }
    public bool isMake2048()
    {
        if (isEnding) return true;
        else return false;
    }
}
