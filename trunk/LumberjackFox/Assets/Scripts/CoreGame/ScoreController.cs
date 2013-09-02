using UnityEngine;
using System.Collections;

public static class ScoreController {
	
	public static float totalTimePass;
	
	public static string currentLevel;
	
	public static int amountCoins;
	
	public static string[] levels;
	
	private static int BASE = 100;
	private static int MAX_TIME = 9999;
	private static int COIN_VALUE = 5;
	
	public static float CalculateScore()
	{
		Debug.Log(totalTimePass);
				
		return ( ( ( float ) COIN_VALUE * amountCoins ) + ( totalTimePass ) * ( ( float ) BASE / MAX_TIME ) );
	}
	
}
