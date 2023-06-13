using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player")){
            Debug.Log("Inimigo no range");
        }
    }
}
