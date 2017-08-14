using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Text kmH;

    Swipe sw = new Swipe();
    
    public float maxSpeed = 1;
    public float minSpeed = 1;
    public float currentSpeed = 1;
    public float acelerateForce = 0.1f;
    public float breakForce = 0.1f;
    public float turnForce = 1;
    
    public int points = 0;

    int currentPosId = 0;

    bool canMove = true;

    public Transform roadsParent;

    Transform posLeft, posCenter, posRight;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        this.kmH = GameObject.Find("playerSpeedValue_text").GetComponent<Text>();

        this.posLeft = GameObject.Find("posLeft").transform;
        this.posCenter = GameObject.Find("PlayerSpawn").transform;
        this.posRight = GameObject.Find("posRight").transform;

        this.currentSpeed = this.minSpeed;

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!this.canMove)
        {
            return;

        }

        string side = this.sw.Drag();

        float turn = 0, speed = 0;

        switch (side)
        //funçao swipe
        {
            case "up":
                {
                    Acelerate();
                    break;
                }
            case "down":
                {
                    Break();
                    break;
                }
            case "left":
                {
                    ChangePos(-1); //turn = -1;
                    break;
                }
            case "right":
                {
                    ChangePos(1); //turn = 1;
                    break;
                }
        }
        if (Input.GetButtonDown("Vertical"))
        {        
            if(Input.GetAxis("Vertical") > 0)
            {
                Acelerate();
            } else if (Input.GetAxis("Vertical") < 0)
            {
                Break();
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                ChangePos(1); //turn = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                ChangePos(-1);//turn = -1;
            }
        }

        //this.rb.AddForce(transform.forward * this.currentSpeed,ForceMode.Acceleration);
        this.rb.velocity = new Vector3(-this.currentSpeed,0, turn);

        this.kmH.text = string.Format("{0}Km/H", ((((int)this.currentSpeed) * 10).ToString()));
        //atualiza a text falando a velocidade

        GoToPos();

    }

    void ChangePos(int i = 0)
    {
        Debug.Log(i);

        this.currentPosId += i;

        if(this.currentPosId > 2)
        {
            this.currentPosId = 2;
        }

        if(this.currentPosId < 0)
        {
            this.currentPosId = 0;
        }       

    }

    void GoToPos()
    {
        Vector3 pos = this.transform.position;

        switch (this.currentPosId)
        {
            case 0:
                {
                    pos.z = this.posLeft.position.z;
                    break;
                }
            case 1:
                {
                    pos.z = this.posCenter.position.z;
                    break;
                }
            case 2:
                {
                    pos.z = this.posRight.position.z;
                    break;
                }
        }

        this.transform.position = Vector3.Lerp(this.transform.position, pos, this.turnForce);
    }

    void Acelerate()
        //acelera o veiculo
    {
        if (this.currentSpeed < this.maxSpeed)
        {
            this.currentSpeed += this.acelerateForce;
        } else
        {
            this.currentSpeed = this.maxSpeed;
        }
    }

    void Break()
        //freia o veiculo gradualmente, mas nunca abaixo do minimo
    {
        if(this.currentSpeed > minSpeed)
        {
            this.currentSpeed -= this.breakForce;
        } else
        {
            this.currentSpeed = minSpeed;
        }
    }

    void Collecte(float value)
    {
        MoneyCrud crud = GameObject.Find("Player_UI").GetComponent<MoneyCrud>();

        crud._SetMoney(value);
    }

    void Lose()
    {
        //sequencia para derrota
        Debug.Log("perdeu");

        GameObject.Find("_GC").GetComponent<MenuController>().ChangeMenu("Bonus_menu");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "obstaculo")
        {
            this.canMove = false;
            Lose();
        }
    }
}
