﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moneyupdate : MonoBehaviour {

	public Text moneytext;
	public int money = 0;
	public int moneytouchspeed = 5;
	private int moneyspeed = 2;


	void Start() {

		StartCoroutine ("CountTime", 1);
	}

	IEnumerator CountTime(float delayTime) {

		money += moneyspeed;
		moneytext.text = "Money : " + System.Convert.ToString (money) + "원";

		yield return new WaitForSeconds(delayTime);
		StartCoroutine("CountTime", 1);
	}

	void Update () {

		if (Input.GetMouseButtonDown (0)) {

				money += moneytouchspeed;
				moneytext.text = "Money : " + System.Convert.ToString (money) + "원";

		}
	}




}