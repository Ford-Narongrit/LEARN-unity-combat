using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkopint : MonoBehaviour
{
    public GameManager GM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("new checkpoint");
            GM.respawnPoint = this.gameObject;
        }
    }
}
