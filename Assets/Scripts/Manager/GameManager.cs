using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
