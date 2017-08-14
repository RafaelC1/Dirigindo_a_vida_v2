using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour {

    public Button[] Buttons;

    public int currentId = 0;

    void Start()
    {
        _GC gc = GameObject.Find("_GC").GetComponent<_GC>();
        this.currentId = gc._GetMode();

    }

	void OnEnable()
    {
        Start();

        int i = 0;
        foreach(Button btn in Buttons)
        {
            if(i == this.currentId)
            {
                btn.gameObject.SetActive(true);
            } else
            {
                btn.gameObject.SetActive(false);
            }

            i++;
        }
    }

    void _SetCurrentBtn()
    {
    }
}
