using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {

    #region ATRIBUTOS

    Animator animator;
    public GameObject nextEvent;

    public string nextScreen;    

    public enum nextTrigger
    {
        allowAnim,
        nextScene,
        endApp
    }

    public nextTrigger trigger;

    #endregion

    void Start()
    {
        this.animator = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        StartCoroutine(NextAnimation(clipInfo[0].clip.length));
    }

    IEnumerator NextAnimation(float time)
    {
        yield return new WaitForSeconds(time);

        if(trigger == nextTrigger.allowAnim)
        {
            this.nextEvent.SetActive(true);
            this.gameObject.SetActive(false);
        } else if (trigger == nextTrigger.nextScene)
        {
            GameObject.Find("_GC").SendMessage("_ChangeSceneTo", nextScreen, SendMessageOptions.RequireReceiver);
            this.gameObject.SetActive(false);
        } else if (trigger == nextTrigger.endApp)
        {
            this.gameObject.SetActive(false);
        }

        
    }

}
