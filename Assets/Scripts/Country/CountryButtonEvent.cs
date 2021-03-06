﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonEvent : MonoBehaviour {
	public const int SIZE=13;
	//country, icon img, btn obj
	public GameObject[] countryObj = new GameObject[SIZE];
	public GameObject[] imgObj = new GameObject[SIZE];
	public GameObject[] btnObj = new GameObject[SIZE];
	public GameObject countryPopup;

	public TransMoney myTransMoney;
	public HouseButtonEvent housebg;

	public GameObject flagObj;
	static ulong temp1000JO= 1000000000000000; //1000조
	public ulong[] Price = new ulong[SIZE]; //가격
	public bool[] BuyList = new bool[SIZE] {true, true, true, true, true, 
		true, true, true, true, true, 
		true, true, true}; //구매 여부

	public ulong money=0;

	bool signal = true;

	void Awake(){//초기화
		for (int i = 0; i < SIZE; i++) {
			countryObj[i] = null;
			imgObj[i] = null;
			btnObj[i] = null;

			if (i > 0)
				Price [i] = (temp1000JO << i);// (1000000000000*5*(i+1));//5조~ 75조
			else
				Price [0] = 1818000000000000;
		}
	}

	public void countryInitiate()//기본 셋팅, countryButton onClick에 연결
	{	
		if (countryPopup.activeSelf == true)
		{
			for (int i = 0; i < SIZE; i++) 
			{
				//country,iconimg,btn오브젝트
				countryObj[i] = GameObject.Find("Country (" + i + ")").gameObject;
				//countryObj [i].transform.position = new Vector3(99.5f,-18.0f-(36.0f*i)); //위치변환 포기..ㅠ 
				imgObj[i] = countryObj[i].transform.FindChild("Image").gameObject;
				btnObj[i] = countryObj[i].transform.FindChild("Button").gameObject;

			}
		}
	}
	void Update () {
		Moneyupdate MU= GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
		money = MU.money;
		if (countryPopup.activeSelf == true) {
			countryCheck (money);
		}
		else {
			if (signal) {		
				//saveManager로 저장한 List 띄우는건데...;
				//추후에 렉걸리면 방법을 바꿔야할듯...
				for (int i = 0; i < SIZE; i++) {
					if (BuyList [i] == false)
						countryAdd (i);
				}
				signal = false; //한번만 실행하게끔...
			}
		}
	}

	public void countryCheck(ulong money){
		for (int i = 0; i < SIZE; i++) {
			//돈 있을 때,
			if (money > Price [i]) {
				//이미 구매한 경우
				if (BuyList [i] == false) {
					//구매완료, 어둡게, 컬러아이콘, 버튼비활성화 
					countryObj[i].transform.GetChild(1).GetComponent<Text>().text = "구매 완료";
					countryObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Flag/flag ("+i+")") as Sprite;
					btnObj[i].SetActive(false);
				}
				//돈도 있고, 안샀다면,
				else {
					//가격표기, 밝게, 컬러아이콘, 버튼활성화
					countryObj [i].transform.GetChild (1).GetComponent<Text> ().text = myTransMoney.strTransMoney(Price[i]);
					countryObj[i].GetComponent<Image> ().color = new Color (154 / 255, 154 / 255, 154 / 255, 154 / 255);
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Flag/flag ("+i+")") as Sprite;
					btnObj[i].SetActive(true);

				}
			}
			//살 돈 없을 때,
			else {
				//살 돈 없고, 이미 구매한 경우
				if (BuyList[i]==false) {
					//구매완료, 어둡게, 컬러아이콘, 버튼비활성화 
					countryObj[i].transform.GetChild(1).GetComponent<Text>().text = "구매 완료";
					countryObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Flag/flag ("+i+")") as Sprite;
					btnObj[i].SetActive(false);
				} 
				//아직 구매 안한 경우
				else {
					//가격표기, 어둡게, 흑백아이콘, 버튼비활성화
					countryObj [i].transform.GetChild (1).GetComponent<Text> ().text = myTransMoney.strTransMoney(Price[i]);
					countryObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Flag/flag ("+i+")") as Sprite;//gray_flag ("+i+")") as Sprite;
					//이미지 어둡게
					imgObj [i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					btnObj[i].SetActive(false);
				}	
			}
		}
	}

	//Button onClick event
	public void setBG (int btn){
		countryMoneyEvent (btn);
		countryAdd (btn);
		CountryChangeBG (btn);
		changeBuyEnable (btn);

		//자산 housenowPrice 업데이트
		AssetsEvent myAssetList= GameObject.Find("AssetsManager").GetComponent<AssetsEvent>();
		myAssetList.Asset_countryNowPrice[btn] = Price[btn];


	}
	public void countryMoneyEvent(int sel){
		//MoneyManager에서 country의 가격에 따라 MoneyUpdate
		Moneyupdate MU= GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
		MU.money -= Price[sel];
	}
	public void countryAdd(int sel){ //국기 활성화
		GameObject flagObj = GameObject.Find("FlagFolder").transform.GetChild(sel).gameObject;
		flagObj.SetActive (true);
	}

	public void countryDel(int sel){ //국기 비활성화
		GameObject flagObj = GameObject.Find("FlagFolder").transform.GetChild(sel).gameObject;
		flagObj.SetActive (false);
	}

	public void CountryChangeBG(int sel){
		sel += 7;
		//리소스에서 배경파일 로드
		GameObject.Find ("Background").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+sel) as Sprite;
		Debug.Log ("btn "+sel+"_onClick");
		housebg.Selected_BG = sel; //저장을 위해...

	}

	public void changeBuyEnable(int sel){//구매한 경우 false로 표시 해줌
		BuyList [sel] = !BuyList [sel];
	}
}

