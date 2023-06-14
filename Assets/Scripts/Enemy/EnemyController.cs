using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private PlayerController player;
    [SerializeField] private Enemy enemy;
    AnimationsController animations;

    private void Start() {
        animations = new AnimationsController();
    }

    private  void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            enemy.TakeDamageEnemy(player.damage);
        }
    }
}
