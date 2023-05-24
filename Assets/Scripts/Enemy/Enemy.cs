using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedEnemy = 10;
    private Rigidbody rb;

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
            Debug.Log("INIMIGO");
            MoveToPlayer();
        } 
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("INIMIGO SUMIU");
    }
}
