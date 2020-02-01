using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
public class BackgroundScroller : MonoBehaviour
{
    private Sprite sprite;
    public Vector2Reference playerPosition;
    public float scrollFactor;
    public int offset = 0;
    float width = 8;
    
    void Awake(){
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
    void Start(){

        width = sprite.bounds.max.x - sprite.bounds.min.x;
        width = Mathf.Abs(width);

    }

    void Update()
    {
        if(playerPosition.Value.x-(width*offset) > 0)
        {
            offset++;
            GameObject.Instantiate(gameObject,transform.position+Vector3.right*width*offset,Quaternion.identity,transform).GetComponent<BackgroundScroller>().enabled = false;
        }
      //  transform.position = new Vector3(-playerPosition.Value.x*scrollFactor,transform.position.y,transform.position.x);
    }
}
