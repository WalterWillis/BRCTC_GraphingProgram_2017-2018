using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
//TODO add animation curve to lerp
public class LineRendererGraph : MonoBehaviour
{

    public int GraphHeight = 4;
    public int cameraX = 24;
    public float FPS { get { return frames; } }
    public string Table_Name;
    public string Column_Name;
    public int Min_Row;
    public int Max_Row;
    public string ColumnModifier;
    public string OrderBy;
    public int textAmount = 20;
    public TextMesh TextObj;
    public int segment;

    private LineRenderer LineRenderer;
    private bool hasRun;
    private bool drawText = false;
    private float frames = 0;
    private float yMulti = 20; // Makes the y axis appear larger
    //private float lerpTime = 1f;
    //private float currentLerpTime;

    private Vector3 previousLocation;
    private List<Vector3> TextLocations = new List<Vector3>();

    private void Start()
    {
        LineRenderer = this.GetComponent<LineRenderer>();
        LineRenderer.startWidth = .08f;
        LineRenderer.endWidth = .08f;
        hasRun = false;
        //Max_Row = Min_Row + 120;
    }

    private void Update()
    {
        if (!hasRun)
        {
            DrawGraph();
            hasRun = true;
            DrawText();
        }
    }
    private void DrawGraph()
    {
        List<Vector3> positions = GetListFromTable(Table_Name, Column_Name, Min_Row, Max_Row);
        LineRenderer.positionCount = positions.Count;
        LineRenderer.SetPositions(positions.ToArray());
    }
    private void DrawText()
    {
        foreach (Vector3 p in TextLocations)
        {
            TextObj.text = (p.y / yMulti).ToString(); // Write the original y axis
            Instantiate(TextObj, p, Quaternion.identity);
        }
    }

    private List<Vector3> GetListFromTable(string tableName, string columnName, int minRow, int maxRow)
    {

        Database database = new Database();
        DataTable table = database.GetTable(tableName, columnName, ColumnModifier, minRow, maxRow, OrderBy);
        int GraphLength = table.Rows.Count + 1;
        int textListLength;

        if (textAmount != 0 && textAmount < GraphLength)
            textListLength = GraphLength / textAmount; // Specific amount of text
        else if (textAmount > GraphLength)
            textListLength = -1; // Text for every line
        else
            textListLength = 0; // No Text

        float listRatioX = cameraX / (float)GraphLength;
        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0, 0, 0));

        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn column in table.Columns)
            {
                float xData = list.Count * listRatioX;
                float yData = float.Parse(row[column].ToString()) * yMulti; // elongate the y axis
                Vector3 Result = new Vector3(xData, yData, 0);

                if ((textListLength != 0 && list.Count % textListLength == 0) || textListLength == -1)
                    TextLocations.Add(Result);
                list.Add(Result);
            }
        }

        return list;
    }
}
