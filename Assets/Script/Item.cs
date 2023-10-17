using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int heal = 3;
    [SerializeField]
    private int attackUp;
    private GameObject player;
    private Player myplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myplayer = player.GetComponent<Player>();
    }


     private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(myplayer.playerHp >= myplayer.playerMaxHp)
            {
                myplayer.playerHp = myplayer.playerMaxHp;
            }else
            myplayer.playerHp += heal;
            myplayer.damage += attackUp;
            Destroy(gameObject);
        }

    }



}
