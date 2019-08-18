using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
//TODO add animation curve to lerp
public class Lerping : MonoBehaviour {

    public Vector3 startpos, endpos;
    public bool repeateable = false;
    public float speed = 1.0f;
    public float duration = 3.0f;
    public int ListSize = 400;
    public int GraphLength = 40;
    public int GraphHeight = 4;
    public int cameraX = 24;
    public string orderBy;


    private float currentlerptime;
    private int counter = 0;
    private ParticleSystem Stream;
    private float startTime, totalDistance;

    // Use this for initialization
    IEnumerator Start () {
        
        Stream = GetComponentInChildren<ParticleSystem>();
        var main = Stream.main;
        main.startLifetime = 100;
        //List<Vector3> positions = SetList();
        List<Vector3> positions = GetListFromTable();
       // startpos = new Vector3(0, 0, 0);
       // endpos = new Vector3(8, 12, 0);
        Vector3 otherPoint = new Vector3(16, 0, 0);
        startTime = Time.time;
        totalDistance = Vector3.Distance(startpos, endpos);

        while (repeateable)
        {
           // yield return RepeatLerp(startpos, endpos, duration);
            
            //yield return RepeatLerp(endpos, otherPoint, duration);
            //yield return RepeatLerp(otherPoint, startpos, duration);
            yield return RepeatLerpList(positions, duration);          
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!repeateable) // used to test sin wave drawing
        {
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            Vector3 newPos = Vector3.Lerp(startpos, endpos, journeyFraction);

            this.transform.position = new Vector3(newPos.x, Mathf.Sin(newPos.x), newPos.z);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {

        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator RepeatLerpList(List<Vector3> list, float time)
    {
        int listSize = list.Count;
        float t = 0.0f;
        float rate = (1.0f / time) * speed;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(list[counter], list[counter + 1], t);
            yield return null;
        }
        counter++;
        if (counter >= (listSize - 1))
        {
            counter = 0;
            Stream.Pause();
            this.transform.position = list[counter];
            Stream.Play();
        }
    }

    private List<Vector3> GetListFromTable()
    {
        
        Database database = new Database();
        DataTable table = database.GetTable("FirstImport", "PiezoX", "ID", 1, 60000, orderBy);
        GraphLength = table.Rows.Count + 1;

        float listRatioX = cameraX /(float)GraphLength;

        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0, 0, 0));

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn column in table.Columns)
            {
                //Debug.Log(list.Count * listRatioX);
                list.Add(new Vector3(list.Count * listRatioX, GraphHeight / float.Parse(row[column].ToString()), 0));
            }
        }

        return list;
    }

    private List<Vector3> SetList()
    {
        float listRatioX;
        float listRatioY;

        float tempLength = 1f;
        float tempHeight = 1f;


            listRatioX = (float)GraphLength / ListSize;
            listRatioY = GraphHeight / tempHeight;
        Debug.Log("X Axis length = " + (tempLength * listRatioX) * ListSize);

        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0, 0, 0)); // start of list index
        Vector3 positiveConstant = new Vector3(tempLength * listRatioX, tempHeight * listRatioY, 0);
        Vector3 negativeConstant = new Vector3(tempLength * listRatioX, -tempHeight * listRatioY, 0);

        for (int i = 0; i < (ListSize - 1); i++)
        {
            if(i % 2 == 0)
                list.Add(list[i] + negativeConstant);
            else
                list.Add(list[i] + positiveConstant);
        }
        //Debug.Log(list.Count);
        return list;
    }

}
