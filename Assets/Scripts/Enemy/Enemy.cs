using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform target;
    [SerializeField] private float speedEnemy = 10;
    private Rigidbody rb;

    private float life = 4;
    public float damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void MoveToPlayer()
    {     
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speedEnemy * Time.deltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target);
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            MoveToPlayer();
            player.emCombate = true;
        } 
    }

    public void TakeDamageEnemy(float damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        player.emCombate = false;
    }
}
