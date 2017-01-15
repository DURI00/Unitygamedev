﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnable : MonoBehaviour {
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Button secondbtn;
    public UnityEngine.UI.Button thirdbtn;
    public int money=0;
    public int randomnum = 0;
    public int JugallumLevel=0;
    public int touchcnt = 0;
    public GameObject Jugallum = null;
    public GameObject waren = null;
    public GameObject box = null;
    public GameObject goldbox = null;
    public Text btn_text=null;
    public bool waren_exist =false;
    public bool box_exist = false;
    public bool goldbox_exist = false;
    public float timespan;
    public float checkTime;

	// Use this for initialization
	void Start () {
        if (btn == null)
        {
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();

        }
        if(secondbtn==null)
        {
            secondbtn = gameObject.GetComponent<UnityEngine.UI.Button>();
        }

        JugallumLevel = 0;
        PlayerPrefs.SetInt("FirstJugallumLevel", JugallumLevel);
        //btn_text.text = "test";

        ColorBlock cd = btn.colors;
        ColorBlock dd = btn.colors;
        cd.normalColor = Color.blue;
        dd.normalColor = Color.white;

        btn.enabled = false;
        secondbtn.enabled = false;
        btn.colors = cd;
        secondbtn.colors = cd;

        timespan = 0.0f;
        checkTime = 5.0f;
    }
	
	// Update is called once per frame
	void Update () {

        Moneyupdate moneyu = GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
        money = moneyu.money;

        ColorBlock cd = btn.colors;
        ColorBlock dd = btn.colors;
        cd.normalColor = Color.blue;
        dd.normalColor = Color.white;


        if (money>101)
        {
            btn.enabled = true;
            btn.colors = dd;
        }	
        else if(money<100)
        {
            btn.enabled = false;
            btn.colors = cd;
        }
        if(money>301)
        {
            secondbtn.enabled = true;
            secondbtn.colors = dd;
        }
        else if(money<300)
        {
            secondbtn.enabled = false;
            secondbtn.colors = cd;
        }

        if(waren_exist==true)
        {
            timespan += Time.deltaTime;
            if(timespan>checkTime)
            {
                eventbox();
                timespan = 0;
            }
            if (touchcnt > 5)
            {
                box.active = false;
                goldbox.active = false;
                touchcnt = 0;
            }

        }
    }
    public void btnClick()
    {
        JugallumLevel = PlayerPrefs.GetInt("FirstJugallumLevel", 0);
        Jugallum.active = true;
        JugallumLevel++;
        PlayerPrefs.SetInt("FirstJugallumLevel", JugallumLevel);

        btn_text.text= "레벨:"+JugallumLevel;
    }   

    public void secondbtnClick()
    {
        waren.active = true;
        waren_exist = true;

    }
    public void eventbox()
    {
        randomnum = Random.Range(0, 10);
        if(randomnum<6)
        {
            box.active = true;
            box_exist = true;
        }
        else
        {
            goldbox.active = true;
            goldbox_exist = true;
        }


    }
    public void Touchbox()
    {
        if(Input.GetMouseButtonDown(0))
        {

            touchcnt++;

        }
    }
}
