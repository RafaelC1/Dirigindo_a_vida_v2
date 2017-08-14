using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GC : MonoBehaviour {

    public int a;
    public int askAmount;

    public enum currentGameMode
    {
        game,menu
    }

    public currentGameMode gameMode;

    public void _SetMode(int i)
    {
        switch (i)
        {
            case 0 :
                {
                    this.gameMode = currentGameMode.game;
                    break;
                }
            case 1:
                {
                    this.gameMode = currentGameMode.menu;
                    break;
                }
            case 2:
                {
                    break;
                }
        }
    }

    public int _GetMode()
    {
        int i = 0;

        if(gameMode == currentGameMode.game)
        {
            i = 0;
        } else if (gameMode == currentGameMode.menu){
            i = 1;
        }

        return i;
    }
}
