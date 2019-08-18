using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
//TODO add animation curve to lerp
public class Lerping2 : MonoBehaviour {

    public int GraphHeight = 4;
    public int cameraX = 24;
    public float duration = 1f;
    public float FPS { get { return frames; } }
    public string Table_Name;
    public string Column_Name;
    public int Min_Row;
    public int Max_Row;
    public string orderBy;

    private float frames = 0;
    //private float lerpTime = 1f;
    //private float currentLerpTime;
    private int counter = 0;
    private ParticleSystem Stream;
    private bool canPlay = true;
    private GameObject parent;

    private Vector3 previousLocation;

    private List<Vector3> positions;

    void Start()
    {
        parent = this.transform.parent.gameObject;
        Stream = GetComponentInChildren<ParticleSystem>();
        var main = Stream.main;
        main.startLifetime = 100;
        positions = GetListFromTable(Table_Name, Column_Name, Min_Row, Max_Row);
        counter = 0;
        //startTime = Time.time;
    }
    private void OnGUI()
    {
        //if (!canPlay)
        //{

        //    //Vector3 pos = new Vector3();
        //    //RaycastHit hitInfo = new RaycastHit();
        //    //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        //    //{
        //    //    pos = hitInfo.collider.transform.position;
        //    //}
        //    Vector3 pos = Input.mousePosition;
        //    pos.z = 10;
        //    pos.Scale(new Vector3(1f / 45, 1f / 45, 1f));
        //    Camera c = Camera.main;
        //    Vector3 mousePos = Input.mousePosition;
        //    mousePos.z = c.nearClipPlane;
        //    mousePos = c.ScreenToWorldPoint(mousePos);

        //    Vector3 worldPos = mousePos;// - c.transform.position;
        //    if (pos.x >= 0 && pos.x < 30)
        //    {
        //        GUI.Box(new Rect(20, 20, 250, 120), pos.x.ToString());
        //        //GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        //        //GUILayout.Label("Mouse position: " + mousePos);
        //        //GUILayout.Label("World position: " + p.ToString("F3"));
        //        //GUILayout.EndArea();
        //    }
        //}   
    }

    void Redraw(int _counter)
    {
        Stream.Stop();
        Stream.Clear();
        Stream.Play();
        this.transform.localPosition = parent.transform.position;
        for (int i = 0; i <= _counter; i++)
        {
            this.transform.localPosition = parent.transform.position + positions[_counter];//Vector3.Lerp(positions[counter], positions[counter + 1], 1f);
            if (counter >= (positions.Count - 1))
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        frames = 1f / Time.deltaTime;

        float dataperframe = frames / duration;

        //currentLerpTime += Time.deltaTime;
        //if (currentLerpTime > lerpTime)
        //    currentLerpTime = lerpTime;
        //float rate = currentLerpTime / lerpTime;
        if (this.parent.transform.position != previousLocation && previousLocation != null)
            Redraw(counter);

        if (canPlay)
        {
            for (int i = 0; i < dataperframe; i++)
            {
                this.transform.localPosition = parent.transform.position + positions[counter];//Vector3.Lerp(positions[counter], positions[counter + 1], 1f);
                counter++;
                if (counter >= (positions.Count - 1))
                    break;
            }
            counter++;


            if (counter >= (positions.Count - 1))
            {
                canPlay = false;
                counter = 0;
                Stream.Pause();
                this.transform.localPosition = parent.transform.position + positions[counter];
                Debug.Log(parent.transform.position);
                GameObject sphere = this.gameObject;
                sphere.GetComponent<Renderer>().enabled = false;
                //Stream.Play();
            }
        }
        previousLocation = this.parent.transform.position;
    }

    private List<Vector3> GetListFromTable(string tableName, string columnName, int minRow, int maxRow)
    {
        
        Database database = new Database();
        DataTable table = database.GetTable(tableName, columnName, "ID", minRow, maxRow, orderBy);
        int GraphLength = table.Rows.Count + 1;

        float listRatioX = cameraX /(float)GraphLength;
        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0, 0, 0));

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn column in table.Columns)
            {
                float yData = float.Parse(row[column].ToString()) * 100;
                //float y = GraphHeight / float.Parse(row[column].ToString()); // small numbers don't need this. make better algorithm. EX:  n < 0 = LARGE NUMBER
                //Debug.Log(list.Count * listRatioX);
                list.Add(new Vector3(list.Count * listRatioX, yData, 0));
            }
        }

        return list;
    } 
}
