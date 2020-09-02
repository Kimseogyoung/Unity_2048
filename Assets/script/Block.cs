using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Animator anim;
    public int number;
   
    public void createBlock()
    {
        anim.SetTrigger("create");
    }
    public void Move(Vector3 targetPosition,GameObject removeBlock= null)
    {
        StopAllCoroutines(); //코루틴 종료
        StartCoroutine(slideBlock(targetPosition, removeBlock));//코루틴 시작       

    }
    
    IEnumerator slideBlock(Vector3 targetPosition,GameObject removeBlock=null) {

        float t = 0.2f;
        while(transform.position != targetPosition)
        {
            t += Time.deltaTime * 0.3f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
            yield return null;
        }
        if (removeBlock != null) Destroy(removeBlock);
        
        
    }
    

}
