using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float currentTimer;

    public float expireRate;
    public float speed;
    public float damage;

    private GameObject otherPlayer;

    private void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        currentTimer += 1 * Time.deltaTime;

        if (currentTimer >= expireRate)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPlayer = other.gameObject;
            DealDamage(otherPlayer);
        }
    }

    private void DealDamage(GameObject otherPlayer)
    {
        otherPlayer.GetComponent<Player>().health -= damage;
        Destroy(this.gameObject);
    }
}
