﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 3f;
	float currentHealthPoints = 3f;

	public float healthAsPercentage
	{
		get {
			return currentHealthPoints / (float)maxHealthPoints;
		}
	}
}
