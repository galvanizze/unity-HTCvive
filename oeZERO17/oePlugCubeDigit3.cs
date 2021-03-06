﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oePlugCubeDigit3 : MonoBehaviour
{
    public string nameObj = "oeCD3";
    private int horizontal = 5;
    private int vertical = 9;
    //public int size = 1;
    public float scale = 0.1f;
    public bool onlyBlack = false; //
    public Transform startTrans;
    public Color digColor = Color.red;  


    private int cntU = 0;
    private int startTime = 10;
    private Renderer rend1;
    private Vector3 positionP;
    private GameObject[,,] goD = new GameObject[3, 5, 9];

    private bool[,] numDigit = new bool[,] {
        { true, true, true, true, true, true, false },  //0
        { false, true, true, false, false, false, false },  //1
        { true, true, false, true, true, false, true }, //2
        { true, true, true, true, false, false, true }, //3
        { false, true, true, false, false, true, true }, //
        { true, false, true, true, false, true, true }, //5
        { true, false, true, true, true, true, true },    //6
        { true, true, true, false, false, false, false }, //7
        { true, true, true, true, true, true, true },  //8
        { true, true, true, true, false, true, true }  //9
    };

    // Use this for initialization
    void Start()
    {
        Debug.Log("oePlugCubeDigit3");
        //startTrans.position = startTrans.position + new Vector3(10, 1, 0); // Vector3.zero;
        createCubePoints();
    }

    // Update is called once per frame
    void Update()
    {
        cntU++;
        //int i = 0; int j = 0;
        //GameObject go = GameObject.Find(nameObj + j + "." + i); //?i obj reference     
        if (cntU % 100 == 0)
        {
            //var randomN = Random.Range(-1, 9);
            //var randomD = Random.Range(0, 2);
            //drawNum(randomD, randomN);
            drawNum(0, 1);
            drawNum(1, 2);
            drawNum(2, 3);
        }
    }

    int[,] digA() { int[,] points = new int[,] { { 3, 8 }, { 2, 8 }, { 1, 8 } }; return points; }
    int[,] digB() { int[,] points = new int[,] { { 0, 7 }, { 0, 6 }, { 0, 5 } }; return points; }
    int[,] digC() { int[,] points = new int[,] { { 0, 3 }, { 0, 2 }, { 0, 1 } }; return points; }
    int[,] digD() { int[,] points = new int[,] { { 3, 0 }, { 2, 0 }, { 1, 0 } }; return points; }
    int[,] digE() { int[,] points = new int[,] { { 4, 1 }, { 4, 2 }, { 4, 3 } }; return points; }
    int[,] digF() { int[,] points = new int[,] { { 4, 5 }, { 4, 6 }, { 4, 7 } }; return points; }
    int[,] digG() { int[,] points = new int[,] { { 3, 4 }, { 2, 4 }, { 1, 4 } }; return points; }

    void drawSegment(int d,bool light, int[,] segment)
    {
        for (int s = 0; s < 3; s++)
        {
            //Debug.Log("--- segment ---" + s +": "+ segment[s, 0] + "." + segment[s, 1]);
            //go = GameObject.Find(nameObj + segment[s,0] + "." + segment[s, 1]);
            rend1 = goD[d, segment[s, 0], segment[s, 1]].GetComponent<Renderer>();
            if (light) rend1.material.color = digColor;
            else rend1.material.color = Color.black;
        }
    }

    void drawNum(int d,int num)
    {
        if (num > 9) num = 9;
        if (num < 0)
        {
            drawSegment(d,false, digA());
            drawSegment(d,false, digB());
            drawSegment(d,false, digC());
            drawSegment(d,false, digD());
            drawSegment(d,false, digE());
            drawSegment(d,false, digF());
            drawSegment(d,false, digG());
        }

        if (num > -1)
        {
            drawSegment(d,numDigit[num, 0], digA());
            drawSegment(d,numDigit[num, 1], digB());
            drawSegment(d,numDigit[num, 2], digC());
            drawSegment(d,numDigit[num, 3], digD());
            drawSegment(d,numDigit[num, 4], digE());
            drawSegment(d,numDigit[num, 5], digF());
            drawSegment(d,numDigit[num, 6], digG());
        }
    }

    void createCubePoints()
    {       
        float deltaX = 0;
        float deltaY = 0;
        positionP.z = startTrans.position.z + scale*3;

        for (int d = 0; d < 3; d++)
        {
            Debug.Log("---digit: "+d);
            for (int j = 0; j < vertical; j++)
            {
                deltaY = j * scale * 1.1f;
                for (int i = 0; i < horizontal; i++)
                {
                    deltaX = i * scale * 1.1f;
                    positionP.x = startTrans.position.x + deltaX + scale * 7 * d + scale * 5;
                    positionP.y = startTrans.position.y + deltaY;
                    //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    goD[d, i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    goD[d, i, j].name = nameObj + d+"."+j + "." + i;
                    goD[d, i, j].transform.position = positionP;
                    goD[d, i, j].transform.localScale = new Vector3(scale, scale, scale / 2);
                    goD[d, i, j].transform.SetParent(startTrans);
                    //go.transform.position = startTrans.position;
                    rend1 = goD[d, i, j].GetComponent<Renderer>();
                    if (onlyBlack)
                    {
                        Color colorBla = new Color(0, 0, 0);
                        rend1.material.color = colorBla;
                    }
                    else
                    {
                        float deltaRnd1 = Mathf.Floor(Random.Range(0,10));
                        int nasCol = 100;
                        Color colorRnd = new Color((deltaRnd1 / nasCol), deltaRnd1 / nasCol, deltaRnd1 / nasCol);
                        rend1.material.color = colorRnd;
                    }
                }  //i    
                
                //positionP.x = 0;
                
            } //j
            //position0.y = 0;
        }
    }
}
