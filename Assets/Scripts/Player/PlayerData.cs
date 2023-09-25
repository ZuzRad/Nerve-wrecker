using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData : MonoBehaviour
{
	public int health;
    public int level;
   // public Checkpoint lastCheckpoint;
    public int lastLevel;
	public float[] position;

    public PlayerData(Player player, Checkpoint checkpoint, int level)
    {
        health = player.healthManager.currentHealth;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
       // lastCheckpoint = checkpoint;
        lastLevel = level;
    }
}
