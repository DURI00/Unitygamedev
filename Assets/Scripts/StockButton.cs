﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockButton : MonoBehaviour {

	public Button buttonComponent;
	public Text nameLabel;
	public Text priceText;
	public Text countText;

	private int minprice=700;
	private int maxprice=70000;
	private Item item;
	private ShopScrollList scrollList;


	// Use this for initialization
	void Start () 
	{
		buttonComponent.onClick.AddListener (HandleClick);
	}

	public void Setup(Item currentItem, ShopScrollList currentScrollList)
	{
		
		item = currentItem;

		if (item.price < minprice) {
			float stockRate = (float)Random.Range (0, 30) / 100;
			item.price = (int)(item.price + item.price * stockRate);

		} else if (item.price >= minprice) {
			
			float stockRate = (float)Random.Range (-28, 30) / 100;
			item.price = (int)(item.price + item.price * stockRate);

		} else if (item.price > maxprice) {
			float stockRate = (float)Random.Range (-30, 0) / 100;
			item.price = (int)(item.price + item.price * stockRate);
		}

		nameLabel.text = item.stockName;
		priceText.text = item.price.ToString ();
		countText.text = item.count.ToString ();
		scrollList = currentScrollList;

	}

	public void HandleClick()
	{
		scrollList.ViewInfo (item);
	}

}
