﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    public Sprite damageSprite;
    public int hitPoints = 4;

    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		
	}
	
	public void DamageWall(int loss)
    {
        spriteRenderer.sprite = damageSprite;
        hitPoints -= loss;
        if(hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
