﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {

    public UnityEngine.UI.Button btn;
	public ulong ClickMoney = 0;
    public ulong JugallumLev = 0;

    public ulong NextLevelPrice = 0;
    public ulong TwoNextLevelPrice = 0;
    public ulong TouchMoney = 5;
   // public ulong TouchMoneyGobhagiBonusPersent = 1;
   // public ulong BonusgobTouchmoney = 0;

    public float bntcm = 0;
    public float GFBonus = 1;
    public float HouseBonus = 1;
    public float NationBonus = 1;
    public float totalClickmoney = 0;

    public int sosoodelete = 0;
    public int noretry = 0;
    public int[] noretryhouse = new int[7];
    public int[] noretryNation = new int[13];
    public int TotalHousePersent = 0;
    public int TotalNationPersent = 0;
    public int TotalgfPersent = 0;

    public Text LevelText = null;
    public Text LevelupbtnText = null;
    public Text NowClickWonTx = null;
    public Text NationBonusTx = null;
    public Text GFBonusTx = null;
    public Text HouseBonusTx = null;

    public bool[] GFTruetoFalse = new bool[6];
    public bool[] HouseTruetoFalse = new bool[7];
    public bool[] NationTruetoFalse = new bool[13];

    public int[] GFbg = new int[6] { 30, 50, 100, 300, 500, 800 };
    public int[] Hsbg = new int[7] { 10, 30, 50, 80, 100, 150, 300 };
    public int[] Nsbg = new int[13] { 100, 130, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 800 };

    public float[] GFgob = new float[6] { 1.3f, 1.5f, 2.0f, 3.0f, 5.0f, 8.0f };
    public float[] Housegob = new float[7] { 1.1f, 1.3f, 1.5f, 1.8f, 2.0f, 2.5f, 3.0f };
    public float[] Nationgob = new float[13] { 2.0f, 2.3f, 2.5f, 3f, 3.5f, 4.0f, 4.5f,5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 8.0f };
    
    public float[] GFnonugi = new float[6] {10/13f, 10/15f, 1/2f, 1/3f, 1/5f, 1/8f };
    public float[] Housenonugi = new float[7] { 10 / 11f, 10 / 13f, 10 / 15f, 10 / 18f, 1 / 2f, 10 / 25f, 1 / 3f };
    public float[] Nationnonugi = new float[13] { 1 / 2f, 10 / 23f, 10 / 25f, 1 / 3f, 10 / 35f, 1 / 4f, 10 / 45f, 1 / 5f, 10 / 55f, 1 / 6f, 10 / 65f, 1 / 7f, 1 / 8f };

    public Image[] HouseImg = new Image[7];
    public Image[] NationImg = new Image[13];
    public Image[] Gfimg = new Image[6];

    public Color open;
    public Color close;

    public string GFTransform = null;
    public string HouseTransform = null;
    public string NationTransform = null;
    public string ToTalTransform = null;
	// Use this for initialization

	void Start () {
        GrilFriendManager gf = GameObject.Find("GirlFriendManager").GetComponent<GrilFriendManager>();
        HouseButtonEvent house = GameObject.Find("HouseManager").GetComponent<HouseButtonEvent>();
        CountryButtonEvent Country = GameObject.Find("CountryManager").GetComponent<CountryButtonEvent>();

        if (btn==null)
        {
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();

        }
        TwoNextLevelPrice = ((JugallumLev + 1) * (JugallumLev + 1) * (JugallumLev + 1) * (JugallumLev + 1)) * 6;
        for(int i=0;i<6;i++)
        {
            if(gf.GFExist[i]==false)
            {
                Gfimg[i].color = close;
            }
        }
        for(int i=0;i<7;i++)
        {
            if(house.BG_BuyList[i]==true)
            {
                HouseImg[i].color = close;
            }
        }
        for(int i=0;i<13;i++)
        {
            if(Country.BuyList[i]==true)
            {
                NationImg[i].color = close;
            }
        }

        if(GFBonus==0)
        {
            GFBonus = 1;
        }
        if(HouseBonus==0)
        {
            HouseBonus = 1;
        }
        if(NationBonus==0)
        {
            NationBonus = 1;
        }

        GFBonusTx.text = "" + TotalgfPersent + "%";
        HouseBonusTx.text = "" + TotalHousePersent + "%";
        NationBonusTx.text = "" + TotalNationPersent + "%";

    }
	
	// Update is called once per frame
	void Update () {

        Moneyupdate moneyu = GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
        GrilFriendManager gf = GameObject.Find("GirlFriendManager").GetComponent<GrilFriendManager>();
        HouseButtonEvent house = GameObject.Find("HouseManager").GetComponent<HouseButtonEvent>();
        CountryButtonEvent Country = GameObject.Find("CountryManager").GetComponent<CountryButtonEvent>();
        


        //여자친구 생기면 클릭당 돈의 % 상승
        //ㄴㅁㅇㅋㅌㅊ
        //
     /*
      * 
      * 
      *
      *
      */
        for (int i = 0; i < 6; i++)
        {
            if(gf.GFExist[i] == true&&noretry==0)
            {

                bntcm = (float)moneyu.touchspeed * GFgob[i];
                GFBonus = GFgob[i];
                sosoodelete =(int) bntcm;
                GFTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(GFTransform);
                GFTruetoFalse[i] = true;
                noretry++;
                GFBonusTx.text =""+GFbg[i]+"%";
                TotalgfPersent = GFbg[i];
                Gfimg[i].color = open;

            }
            else// if(gf.GFExist[i]==false && GFTruetoFalse[i]==true)
            {
                bntcm = (float)moneyu.touchspeed * GFnonugi[i];
                Debug.Log(GFnonugi[0]);
                GFBonus = 1;
                sosoodelete = (int)bntcm;
                GFTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(GFTransform);
                GFTruetoFalse[i] = false;
                noretry--;
                GFBonusTx.text = "0%";
                Gfimg[i].color = close;
            }
        }
        //집 사면 클릭돈 % 상승 

        for(int i=0;i<7;i++)
        {
            if(house.BG_BuyList[i]==false&&noretryhouse[i]==0)
            {
                bntcm = (float)moneyu.touchspeed * Housegob[i];
                if (HouseBonus != 1)
                { HouseBonus = HouseBonus + Housegob[i]; }

                else if (HouseBonus==1)      //본체 버튼 클릭할때 곱해줘서 보너스퍼센트가 초기화 되지 않게함 
                {                            //update 함수에선 집사기에서 사자마자 퍼센트가 올라감
                    HouseBonus = Housegob[i];
                }
                TotalHousePersent = TotalHousePersent + Hsbg[i];

                sosoodelete = (int)bntcm;
                HouseTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(HouseTransform);
                HouseTruetoFalse[i] = true;
                noretryhouse[i]++;
                HouseBonusTx.text = "" + TotalHousePersent+"%";
                HouseImg[i].color=open;
            }
            else if(house.BG_BuyList[i]==true && HouseTruetoFalse[i]==true)
            {
                bntcm = (float)moneyu.touchspeed * Housenonugi[i];
                HouseBonus = HouseBonus - Housegob[i];
                if(HouseBonus==0)
                {
                    HouseBonus++;
                }
                TotalHousePersent = TotalHousePersent - Hsbg[i];
                sosoodelete = (int)bntcm;
                HouseTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(HouseTransform);
                HouseTruetoFalse[i] = false;
                noretryhouse[i]--;
                HouseBonusTx.text = "" + TotalHousePersent + "%";
                HouseImg[i].color = close;
            }
        }

        //국가를 삿을때 상승
        for(int i=0;i<13;i++)
        {
            if(Country.BuyList[i]==false && noretryNation[i]==0)
            {
                bntcm = (float)moneyu.touchspeed * Nationgob[i];
                if(NationBonus!=1)
                {
                    NationBonus = NationBonus + Nationgob[i];
                }
                if (NationBonus==1)
                {
                    NationBonus = Nationgob[i];
                }
                
                TotalNationPersent = TotalNationPersent + Nsbg[i];
                sosoodelete = (int)bntcm;
                NationTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(NationTransform);
                NationTruetoFalse[i] = true;
                noretryNation[i]++;
                NationBonusTx.text = "" + TotalNationPersent + "%";
                NationImg[i].color = open;
            }
            else if(Country.BuyList[i]==true && NationTruetoFalse[i]==true)
            {
                bntcm = (float)moneyu.touchspeed * Nationnonugi[i];
                NationBonus = NationBonus - Nationgob[i];
                if(NationBonus==0)
                {
                    NationBonus++;
                }
                TotalNationPersent = TotalNationPersent - Nsbg[i];
                sosoodelete = (int)bntcm;
                NationTransform = sosoodelete.ToString();
                moneyu.touchspeed = ulong.Parse(NationTransform);
                NationTruetoFalse[i] = false;
                noretryNation[i]--;
                NationBonusTx.text = "" + TotalNationPersent + "%";
                NationImg[i].color = close;
            }
            {

            }
        }


        NowClickWonTx.text = TouchMoney + "원 ->" + moneyu.touchspeed + "원";


        LevelText.text = "둥신 LV" + JugallumLev;
        LevelupbtnText.text = "비용:" + TwoNextLevelPrice + "\n" + "+" + "100" + "/클릭";


    }
    public void btnClick()
    {
        Moneyupdate moneyu = GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();

        TouchMoney = TouchMoney + 100;

        NextLevelPrice = (JugallumLev * JugallumLev * JugallumLev * JugallumLev) * 6;
        TwoNextLevelPrice = ((JugallumLev + 1) * (JugallumLev + 1) * (JugallumLev + 1) * (JugallumLev + 1)) * 6;
        if (moneyu.money > NextLevelPrice)
        {
            moneyu.money=moneyu.money - NextLevelPrice;
            JugallumLev++;
            totalClickmoney = (float)TouchMoney * GFBonus*HouseBonus*NationBonus;//기업 인수 보너스 알바 보너스 추가 하기
            sosoodelete = (int)totalClickmoney;
            ToTalTransform = sosoodelete.ToString();
            moneyu.touchspeed = ulong.Parse(ToTalTransform);
            LevelText.text = "둥신 LV" + JugallumLev;
            LevelupbtnText.fontSize = 8;
            LevelupbtnText.text = "비용:" + TwoNextLevelPrice + "\n" + "+" + "100" + "/클릭";
            NowClickWonTx.text = TouchMoney+"원 ->"+moneyu.touchspeed+"원";

        }
        else if(moneyu.money<NextLevelPrice)
        {
            TouchMoney = TouchMoney - 100;

        }

    }

}
