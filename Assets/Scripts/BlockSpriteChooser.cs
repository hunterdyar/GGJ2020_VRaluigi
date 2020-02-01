using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpriteChooser : MonoBehaviour
{
    public Sprite aloneSprite;
    public Sprite leftEdgeSprite;
    public Sprite middleSprite;
    public Sprite rightEdgeSprite;
    public LayerMask blocksLayerMask;
    void Start()
    {  
        bool freeLeft = !Physics.Raycast(transform.position,Vector3.left,1f,blocksLayerMask);
        bool freeRight = !Physics.Raycast(transform.position,Vector3.right,1f,blocksLayerMask);

        if(freeLeft && freeRight)
        {
            GetComponent<SpriteRenderer>().sprite = aloneSprite;
        }else if(!freeLeft && !freeRight)
        {
            GetComponent<SpriteRenderer>().sprite = middleSprite;
        }else if(freeLeft && !freeRight)
        {
            GetComponent<SpriteRenderer>().sprite = leftEdgeSprite;
        }else if(!freeLeft && freeRight)
        {
            GetComponent<SpriteRenderer>().sprite = rightEdgeSprite;
        }else{
            //should never hit this?
            GetComponent<SpriteRenderer>().sprite = aloneSprite;
        }
    }
}
