using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TilesControll : MonoBehaviour {

    [Header("Pre Fabs")]
    [Space(2)]
    public GameObject[] tilesPreFabs;    
    public GameObject[] characters;
    
    public Transform PlayerSpawnPos;
    public Transform MapSpawnPos;

    List<GameObject> activeTiles;

    Transform Player;

    float spawnX = 0f;

    public float tileLenght = 6.3f, safeZone = 12;
    public int ammnOnScreen = 10;

    public int selectedCharacter = 0;

    void Start()
    {
        this.activeTiles = new List<GameObject>();
    }

    // Use this for initialization
    public void _Beggin () {

        this.spawnX = 0f;

        SpawnPlayer();

        for (int i = 0; i < ammnOnScreen; i++)
        {
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(this.Player == null)
        {
            return;
        }

        if(this.Player.position.x < (-(spawnX - 2 * tileLenght)))
        {
            SpawnTile(RandTile());
            if (this.Player.position.x < -safeZone)
            {
                DeleteTile();
            }
        }
    }

    void SpawnPlayer()
    {
        if (GameObject.Find("spawnPlayer"))
        //procura onde irá spawnar o mapa
        {
            this.PlayerSpawnPos = GameObject.Find("spawnPlayer").transform;
        }

        GameObject temp;

        temp = Instantiate(this.characters[this.selectedCharacter]);
        temp.transform.SetParent(this.transform);

        temp.transform.position = this.PlayerSpawnPos.position;
        temp.transform.rotation = this.PlayerSpawnPos.rotation;

        temp.transform.name = "Player";

        this.Player = temp.transform;
    }
    
    void SpawnTile(int preFabIndex = 0)
    {
        GameObject temp;
        temp = Instantiate(tilesPreFabs[preFabIndex]);
        temp.transform.SetParent(this.gameObject.transform);
        temp.transform.position = MapSpawnPos.position - new Vector3(spawnX,0,0);
        temp.transform.name = "tile";
        spawnX += tileLenght;

        activeTiles.Add(temp);
    }
    
    void DeleteTile()
    {
        Destroy(this.transform.GetChild(5).gameObject);
    }

    int RandTile()
    {
        int i = 0;
        i = Random.Range(0, tilesPreFabs.Length);


        return i;
    }

    public void _ResetMap()
    {

        foreach(Transform tile in this.transform)
        {
            if(tile.name == "tile")
            {
                Destroy(tile.gameObject);
            }
        }

        Destroy(this.Player.gameObject);
    }


}
