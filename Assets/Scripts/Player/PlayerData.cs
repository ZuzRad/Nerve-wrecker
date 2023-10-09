using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
	public int health;
    public int lastCheckpoint;
    public int lastLevel;
	public float[] position;

    public PlayerData(Player player, int checkpoint, int level)
    {
        health = player.healthManager.currentHealth;
        position = new float[3];
        position[0] = player.model.transform.position.x;
        position[1] = player.model.transform.position.y;
        position[2] = player.model.transform.position.z;
        lastCheckpoint = checkpoint;
        lastLevel = level;
    }
}
