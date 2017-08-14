using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swipe : MonoBehaviour
{
    public int minDragDistance;

    float dragDistance;

    float dragSpeed;

    Vector3 firstPos;
    Vector3 endPos;

    void Start()
    {
        this.dragDistance = Screen.height * this.minDragDistance / 100;
    }

    public string Drag()
    {
        string dragSide = "null";

        if (Input.touchCount == 0) { this.dragSpeed = 0; return dragSide; }

        if (Input.touchCount == 1)
            //trabalha apenas com um toque
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
                //detecta o primeiro toque
            {
                this.firstPos = touch.position;
                this.endPos = touch.position;
            } else if (touch.phase == TouchPhase.Moved)
            {
                this.endPos = touch.position;
            } else if (touch.phase == TouchPhase.Ended) 
                //fim do toque
            {
                this.endPos = touch.position;

                if(Mathf.Abs(endPos.x - firstPos.x) > Mathf.Abs(endPos.y - firstPos.y))
                    //detecta se é vertical ou horizontal
                {
                    //horizontal
                    if(endPos.x > firstPos.x)
                    {
                        //DIREITA
                        dragSide = "right";

                        
                    } else
                    {
                        //ESQUERDA
                        dragSide = "left";
                    }

                    this.dragSpeed = Mathf.Abs(endPos.x - firstPos.x);

                } else
                {
                    if (endPos.y > firstPos.y)
                    {
                        //CIMA
                        dragSide = "up";
                    }
                    else
                    {
                        //BAIXO
                        dragSide = "down";
                    }

                    this.dragSpeed = Mathf.Abs(endPos.y - firstPos.y);
                }
            }
        } else
        {
            //APENAS UM TOQUE
            dragSide = "tap";
        }

        return dragSide;
    }

    public float DragSpeed()
        //retorna a distancia do drag
    {
        return this.dragSpeed;
    }
}
