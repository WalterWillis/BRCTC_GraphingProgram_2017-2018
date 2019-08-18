using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLerping : MonoBehaviour
{

    public Vector3 startpos, endpos;
    public bool repeateable = false;
    public float speed = 1.0f;
    public float duration = 3.0f;
    public int ListSize = 40;
    private int counter = 0;

    private float startTime, totalDistance;

    // Use this for initialization
    IEnumerator Start()
    {
        List<Vector3> positions = SetList();
        startpos = new Vector3(0, 0, 0);
        endpos = new Vector3(8, 12, 0);
        startTime = Time.time;
        totalDistance = Vector3.Distance(startpos, endpos);

        while (repeateable)
        {
            //yield return RepeatLerp(startpos, endpos, duration);
            //yield return RepeatLerp(endpos, startpos, duration);
            yield return RepeatLerpList(positions, duration);
            Instantiate(this.gameObject, new Vector3(0,0,0), new Quaternion(0,0,0,0));
            Object.Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!repeateable)
        {
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            this.transform.position = Vector3.Lerp(startpos, endpos, journeyFraction);
        }

        Instantiate(GameObject.FindWithTag("Clone"));
        if (Time.time > startTime + 100)
            Debug.Break();
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator RepeatLerpList(List<Vector3> list, float time)
    {
        float t = 0.0f;
        float rate = (1.0f / time) * speed;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(list[counter], list[counter + 1], t);
            yield return null;
        }
        
        counter++;
        if (counter >= (ListSize - 1))
        {
            counter = 0;
            this.transform.position = list[counter];
        }
    }

    private List<Vector3> SetList()
    {
        List<Vector3> list = new List<Vector3>();
        list.Add(new Vector3(0, 0, 0)); // start of list index
        Vector3 positiveConstant = new Vector3(.1f, .2f, 0);
        Vector3 negativeConstant = new Vector3(.1f, -.2f, 0);

        for (int i = 0; i < (ListSize - 1); i++)
        {
            if (i % 2 == 0)
                list.Add(list[i] + negativeConstant);
            else
                list.Add(list[i] + positiveConstant);
        }
        Debug.Log(list.Count);
        return list;
    }


}
