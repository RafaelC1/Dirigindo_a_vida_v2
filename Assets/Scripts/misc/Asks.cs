using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asks : MonoBehaviour {

    public GameObject AskPreFab,TimeBar;

    [Header("Texts da AskPreFab")]
    public Text ask_text;
    public Text answerA_text;
    public Text answerB_text;
    public Text answerC_text;
    public Text askAmount_text;
    public Image Placa_img;

    [Space(4)]
    [Header("Variaveis da forma de perguntas")]
    public int askAmount;
    public int askMaxAmount;
    public float maxTime;
    public float timeWrongAnswer;

    float currentTime;

    bool isAsking = true;

    string[] texto = new string[5];

    // Use this for initialization
    void Start () {       

	}

    void OnEnable()
    {
        this.askMaxAmount = this.askAmount; //_GC gc = GameObject.Find("_GC").GetComponents<_GC>().;

        CloseAsk();
    }
	
	// Update is called once per frame
	void Update () {

        CountDown();
    }

    void CountDown()
        //metodo de timer para a pessoa responder a pergunta
    {
        if (!this.isAsking)
        {
            return;
        }

        if (this.currentTime > this.maxTime)
        {
            _Responder("d");
            print("acabou");
            this.isAsking = false;
        }

        this.currentTime += Time.deltaTime;

        float percentual = (this.currentTime / this.maxTime) * 1;

        float tamanhoBarra = 1 - percentual;

        if (tamanhoBarra < 0)
        {
            tamanhoBarra = 0;
        }

        TimeBar.transform.localScale = new Vector3(tamanhoBarra, 1, 1);
    }

    public void _Responder(string alternativa)
    {
        if (!this.isAsking)
        {
            return;
        }

        if (alternativa == texto[4])
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }

        this.isAsking = false;
    }

    IEnumerator CorrectAnswer()
    {
        MoneyCrud crud = new MoneyCrud();

        crud._SetMoney(10);

        Debug.Log("+10");

        switch (this.texto[4])
        {
            case "a":
                {
                    this.answerA_text.color = Color.green;
                    break;
                }
            case "b":
                {
                    this.answerB_text.color = Color.green;
                    break;
                }
            case "c":
                {
                    this.answerC_text.color = Color.green;
                    break;
                }
        }
        yield return new WaitForSeconds(timeWrongAnswer/2);

        this.answerA_text.color = Color.white;
        this.answerB_text.color = Color.white;
        this.answerC_text.color = Color.white;

        CloseAsk();
    }

    IEnumerator WrongAnswer()
    {
        this.answerA_text.color = Color.red;
        this.answerB_text.color = Color.red;
        this.answerC_text.color = Color.red;

        switch (this.texto[4])
        {
            case "a":
                {
                    this.answerA_text.color = Color.green;
                    break;
                }
            case "b":
                {
                    this.answerB_text.color = Color.green;
                    break;
                }
            case "c":
                {
                    this.answerC_text.color = Color.green;
                    break;
                }
        }
        yield return new WaitForSeconds(timeWrongAnswer);

        this.answerA_text.color = Color.white;
        this.answerB_text.color = Color.white;
        this.answerC_text.color = Color.white;

        CloseAsk();
    }

    void OpenAsk()
    //esse gera as pergun tas até acabarem
    {
        this.AskPreFab.SetActive(true);

        string[] data = this.gameObject.GetComponent<XMLcrud>()._GetTeInfo();
        print(data[0]);
        this.ask_text.text      = this.texto[0] = data[0];
        this.answerA_text.text  = this.texto[1] = data[1];
        this.answerB_text.text  = this.texto[2] = data[2];
        this.answerC_text.text  = this.texto[3] = data[3];
                                  this.texto[4] = data[4];
    }  
    
    void CloseAsk()
    {
        this.askAmount--;

        this.AskPreFab.SetActive(false);

        if(this.askAmount >= 0)
        {
            this.currentTime = 0;
            //reseta o time
            this.isAsking = true;
            SetAmountText();
            OpenAsk();
            
        }
        else
        {
            GameObject.Find("_GC").GetComponent<MenuController>().ChangeMenu("Death_menu");
        }
    }

    void SetAmountText()
    {
        this.askAmount_text.text = string.Format("{0}/{1}", this.askAmount+1, this.askMaxAmount);
    }
}
