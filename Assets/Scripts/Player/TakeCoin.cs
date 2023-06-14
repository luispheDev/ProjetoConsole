using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCoin : MonoBehaviour
{
    PlayerController player;

    public Text moedas;
    public int amount = 0;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Bola");
            Destroy(gameObject);
            amount++;
            moedas.text += amount.ToString();
            player.tocou = true;
        }
    }
}
