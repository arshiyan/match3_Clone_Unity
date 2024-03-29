﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public 	int 	tilesClicked = 0;
	[HideInInspector]
	public 	float 	stablePosition;
	[HideInInspector]
	public 	bool 	playersTurn = false;


	public 	Text 			scoreText;
	private int 			score;
	private	BoardManager 	boardScript;


    void Awake()
    {
    	score = 0;
    	stablePosition = 0;
        boardScript = GetComponent<BoardManager>();
    }

    void Update()
    {
    	if (playersTurn)
    	{

    		if (tilesClicked == 2)
    		{
    			playersTurn = false;
    			boardScript.SwapSelectedPositions();
    		}
    		score += boardScript.RemoveHorizontalTiles() * 10;
    		score += boardScript.RemoveVerticalTiles() * 10;
    		scoreText.text = "Score : " + score;
    	}
    	else 
    	{
    		if (boardScript.CheckTurn())
    		{
    			stablePosition += 1 * Time.deltaTime;
    			
    		}
    		if (stablePosition > 1)
    		{
    			playersTurn = true;
    		}

    	}
    }
}
