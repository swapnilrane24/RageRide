using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {

    public static ObjectPooling instance;

    public GameObject[] meteorBig;
    public GameObject[] meteorMid;
    public GameObject juice;

    public int meteorPoolAmount;
    public int juicePoolAmount;

    List<GameObject> BigMeteor;
    List<GameObject> MidMeteor;
    List<GameObject> JuiceList;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        BigMeteor = new List<GameObject>();
        MidMeteor = new List<GameObject>();
        JuiceList = new List<GameObject>();

        //big meteor
        for (int i = 0; i < meteorPoolAmount; i++)
        {
            for (int j = 0; j < meteorBig.Length; j++)
            {
                GameObject obj = Instantiate(meteorBig[j]);
                obj.SetActive(false);
                BigMeteor.Add(obj);
            }
        }

        //small meteor
        for (int i = 0; i < meteorPoolAmount; i++)
        {
            for (int j = 0; j < meteorMid.Length; j++)
            {
                GameObject obj = Instantiate(meteorMid[j]);
                obj.SetActive(false);
                MidMeteor.Add(obj);
            }
        }

        //juice
        for (int i = 0; i < juicePoolAmount; i++)
        {
            GameObject obj = Instantiate(juice);  //midbar
            obj.SetActive(false);
            JuiceList.Add(obj);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    //method to activate and spawn juice
    public GameObject GetJuice()
    {
        for (int i = 0; i < JuiceList.Count; i++)
        {
            if (!JuiceList[i].activeInHierarchy)
            {
                return JuiceList[i];
            }
        }
        GameObject obj = Instantiate(juice);
        obj.SetActive(false);
        JuiceList.Add(obj);
        return obj;
    }


    //method to activate and spawn bigMeteor
    public GameObject GetBigMeteor()
    {
        for (int i = 0; i < BigMeteor.Count; i++)
        {
            if (!BigMeteor[i].activeInHierarchy)
            {
                return BigMeteor[i];
            }
        }
        GameObject obj = new GameObject();

        for (int j = 0; j < meteorBig.Length; j++)
        {
            obj = Instantiate(meteorBig[j]);
        }
        obj.SetActive(false);
        BigMeteor.Add(obj);
        return obj;
    }

    //method to activate and spawn midMeteor
    public GameObject GetMidMeteor()
    {
        for (int i = 0; i < MidMeteor.Count; i++)
        {
            if (!MidMeteor[i].activeInHierarchy)
            {
                return MidMeteor[i];
            }
        }
        GameObject obj = new GameObject();

        for (int j = 0; j < meteorMid.Length; j++)
        {
            obj = Instantiate(meteorMid[j]);
        }
        obj.SetActive(false);
        MidMeteor.Add(obj);
        return obj;
    }
}
