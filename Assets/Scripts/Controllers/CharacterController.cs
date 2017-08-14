using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    public Text characterPrice_text,warning_text;
    public Button buyButton, BackButton;
    public GameObject carModelsParent;

    Swipe sw = new Swipe();

    [Space(2)]
    [Header("Arrows")]
    public GameObject backArrow;
    public GameObject nextArrow;

    [Space(2)]
    [Header("Characters position")]
    public Transform  leftPos;
    public Transform  middlePos;
    public Transform  rightPos;

    [Space(2)]
    [Header("Characters")]
    //[Space(2)]
    public float[] characterPrice;
    public GameObject[] characters;
    public GameObject[] charactersPreFab;
    public GameObject nullCharacter;

    public int currentCharacter;

    void Start()
    {
        
    }

    void OnEnable()
    {
        UpdateEverthing();
    }

    void Update()
    {
        string side = this.sw.Drag();

        switch (side)
            //funçao swipe
        {
            case "left":
                {
                    _NextCharacter(1);
                    break;
                }
            case "right":
                {
                    _NextCharacter(-1);
                    break;
                }
            default:
                {
                    break;
                }            
        }
    }

	// Use this for initialization
	void UpdateEverthing () {

        CreateCharacters();

        _PositionCharacters();

        AllowCharacters();

        _MoveCharacters();

        _NextCharacter(0);
        ShowPrice();
        
        
    }

    public void _SelectCharacter()
        //define o veiculo que sera usado
    {
        GameObject.Find("mapParent").GetComponent<TilesControll>().selectedCharacter = this.currentCharacter;
    }

    public void _NextCharacter(int i)
        //seleciona um novo personagem
    {
        this.currentCharacter += i;        

        if(this.currentCharacter == 0)
            //desativa o botao esquerdo caso alcanse o limite 
        {
            this.backArrow.SetActive(false);
        } else
        {
            this.backArrow.SetActive(true);
        }

        if (this.currentCharacter >= this.characters.Length -1)
        //desativa o botao direito caso alcanse o limite 
        {
            this.nextArrow.SetActive(false);
        } else
        {
            this.nextArrow.SetActive(true);
        }

        if (this.currentCharacter < 0)
        {
            this.currentCharacter = 0;
            return;
        }
        else if (this.currentCharacter > this.characters.Length - 1)
        {
            this.currentCharacter = this.characters.Length - 1;
            return;
        }

        _MoveCharacters();
        ShowPrice();

    }

    public void _UnlockNewCharacter()
        //desbloqueia um novo personagem caso o dinheiro seja o suficiente
    {
        MoneyCrud moneyC = new MoneyCrud();

        if(this.characterPrice[this.currentCharacter] <= moneyC._GetMoney()) //SUBSTITUIS MONEY PELA FUNÇAO QUE PEGA O DINHEIRO DO JOGADOR
        {
            PlayerPrefs.SetInt(string.Format("character{0}", this.currentCharacter),1);
            //libera o veiculo
            moneyC._SetMoney(-this.characterPrice[this.currentCharacter]);
            //retira da conta o valor do veiculo

            UpdateEverthing();
        } else
        {
            //CASO NÃO HAJA DINHEIRO SUFICIENTE
            StartCoroutine(ChangeMessageForWhile());
        }
    }
	
	void AllowCharacters()
        //checa se cada personagem ja foi comprado, os que não foram comprados, teram sua skin substituida por algo irreconhecivel
    {
        for(int i = 0; i<= this.characters.Length -1;i++)
        {
            if(this.characterPrice[i] == 0)
                //caso o personagem seja gratis, libera ele
            {
                PlayerPrefs.SetInt(string.Format("character{0}", i), 1);
            }
            if (PlayerPrefs.GetInt(string.Format("character{0}", i.ToString())) == 1)
            //caso ele tenha sido comprado, atualiza o preço para zero o que futuramente ira remover o valor da tela            
            {
                this.characterPrice[i] = 0;
            }    
        }
    }

    void CreateCharacters()
    {
        foreach(GameObject character in this.characters)
        {
            if (character != null)
            {
                Destroy(character);
            }
        }

        int i = 0;
        foreach(GameObject character in this.characters)
        {
            if (PlayerPrefs.GetInt(string.Format("character{0}", i.ToString())) == 0)
            {
                this.characters[i] = Instantiate(this.nullCharacter);                
            } else
            {
                this.characters[i] = Instantiate(this.charactersPreFab[i]);
            }

            this.characters[i].transform.SetParent(this.carModelsParent.transform);
            this.characters[i].transform.rotation = this.characters[i].transform.parent.rotation;
            i++;
        }
    }

    public void _MoveCharacters()
    {
        int i = 0;
        foreach(GameObject character in this.characters)
        {
            if (i < this.currentCharacter)
                //menor que o id atual, o personagem vai para a esquerda da lista
            {
                //character.transform.position = Vector3.Lerp(character.transform.position, this.leftPos.position,0.1f);// this.leftPos.position;
                character.SendMessage("NewPos", this.leftPos, SendMessageOptions.RequireReceiver);
            } else if (i > this.currentCharacter)
            //menor que o id atual, o personagem vai para a direita da lista
            {
                //character.transform.position = this.rightPos.position;
                character.SendMessage("NewPos", this.rightPos, SendMessageOptions.RequireReceiver);
            } else if (i == this.currentCharacter)
            {
                //character.transform.position = this.middlePos.position;
                character.SendMessage("NewPos", this.middlePos, SendMessageOptions.RequireReceiver);
            }
            i++;
        }

    }

    void ChangeButton(bool buy)
        //caso o personagem ainda estiver bloqueado e for selecionado o botao de "selecionar" muda para "comprar"
    {
        if (buy)
        {
            this.buyButton.gameObject.SetActive(true);
            this.BackButton.gameObject.SetActive(false);
        } else
        {
            this.buyButton.gameObject.SetActive(false);
            this.BackButton.gameObject.SetActive(true);
        }
    }

    void ShowPrice()
        //troca o preço na tela
    {
        if (this.characterPrice[this.currentCharacter] <= 0)
        {
            this.characterPrice_text.text = "";
            ChangeButton(false);
            //muda o botao para selecionar uma vez que ja foi comprado
        }
        else
        {
            this.characterPrice_text.text = "$" + this.characterPrice[this.currentCharacter].ToString();
            ChangeButton(true);
            //muda o botao para comprar pois é necessario
        }
    }

    public void _PositionCharacters()
    {
        foreach (GameObject character in this.characters)
        {
            character.transform.position = this.middlePos.position;
        }

        int i = 0;
        foreach (GameObject character in this.characters)
        {
            if (i < this.currentCharacter)
            //menor que o id atual, o personagem vai para a esquerda da lista
            {
                character.transform.position = this.leftPos.position;
            }
            else if (i > this.currentCharacter)
            //menor que o id atual, o personagem vai para a direita da lista
            {
                character.transform.position = this.rightPos.position;
            }
            else if (i == this.currentCharacter)
            {
                character.transform.position = this.middlePos.position;
            }
            i++;
        }
    }

    IEnumerator ChangeMessageForWhile()
        //metodo para mostrar uma mensagem rapida no lugar da outra
    {
        this.warning_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        this.warning_text.gameObject.SetActive(false);
    }
}
