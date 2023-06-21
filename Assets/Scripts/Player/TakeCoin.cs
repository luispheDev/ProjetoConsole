using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCoin : MonoBehaviour
{
    [SerializeField]PlayerController player;

    public Text moedas;
    public int amount = 0;

    
    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Bola");
            Destroy(gameObject);
            // Por algum motivo que eu não sei, não esta incrementando as moedas e sim repetindo os numeros 
            amount++;
            moedas.text = amount.ToString();
            player.tocou = true;
        }
    }

    //Por isso não ta chamando esse metódo, pq nunca fica igual a 7 a soma do amount
    private void Update() {
        if(amount == 7)
        {
            player.Venceu.SetActive(true);
        }
    }
}
