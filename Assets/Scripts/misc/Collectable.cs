using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public float coinValue;

    void OnTriggerEnter(Collider col)
    {
        col.transform.SendMessage("Collecte", this.coinValue, SendMessageOptions.RequireReceiver);
        //destroi a moeda e envia uma mensagem para o player, que ela foi coletada
        Destroy(this.gameObject);
    }

}
