using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CameraControl : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */

    #region ATRIBUTOS

    public bool PlayingMode = false;

    public float x, y, z;

    GameObject Player;

    float posX;
    float posZ;
    float posY;


    [Header("Variaveis de controle da camera")]
    [Space(2)]
    public float velocity;

    [Header("Opçoes extras da camera")]
    public bool useLerp;
    public bool useRot;

    #endregion  
	
	void Update () {

        if(Player == null || !this.PlayingMode)
        {
            return;
        }

        this.transform.LookAt(this.Player.transform);

        posZ = Player.transform.localPosition.z +this.z;
        posY = Player.transform.localPosition.y + this.y;
        posX = Player.transform.localPosition.x + this.x;

        if (useLerp == false)
        {
            //se o lerp estiver desligado, a camera seguira o player igualmente, caso contrario
            //havera um delay em relaçao a camera com o personagem
                transform.position = new Vector3(posX, posY, posZ);
        }
        else
        {
            follow();
        }
    }
    

    void follow()
        //metodo para a camera seguir o player com um certo delay
    {
        Vector3 destino = new Vector3(posX, transform.position.y, posZ);
        transform.position = Vector3.Lerp(transform.position, destino, velocity * Time.deltaTime);
    }

    public void _FindPlayer()
    {
        if (GameObject.Find("Player"))
        {
            Player = GameObject.Find("Player");
            this.PlayingMode = true;
        } else
        {
            Debug.Log("Player not found");
        }
        //encontra o player
    }
}
