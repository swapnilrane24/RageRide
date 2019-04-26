using UnityEngine;
using System.Collections;

public class MeteorSpawnerMenu : MonoBehaviour {

    [SerializeField] private float minY; //min y position at which meteor will be spawn
    [SerializeField] private float maxY; //max y position at which meteor will be spawn
    [SerializeField] private float minTime; //after each time interval new meteor will be spawn
    [SerializeField] private float maxTime; //after each time interval new meteor will be spawn

    // Use this for initialization
    void Start ()
    {
        //at start we call the coroutine
        StartCoroutine(SpawnMeteors());
	}

    IEnumerator SpawnMeteors()
    {
        //we get value between two numbers and use it as interval between spawns
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);

        //we choose the random number and depending on that we spawn the type of meteor the 
        int i = Random.Range(0, 2);
        if (i == 0 )
        {
            //SpawnBigMeteor(positionTop);
            SpawnBigMeteor(GeneratedPlace(transform.position.x , Random.Range(minY, maxY)));
        }
        else if (i == 1)
        {
            //SpawnMidMeteor(positionTop);
            SpawnMidMeteor(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
        }

        //when we are done with our process we again we call the coroutine
        StartCoroutine(SpawnMeteors());
    }

    //methods which get the object from object pooler
    void SpawnBigMeteor(Vector3 pos)
    {
        GameObject meteor = ObjectPooling.instance.GetBigMeteor();
        meteor.transform.position = pos;
        meteor.transform.rotation = Quaternion.identity;
        meteor.SetActive(true);
    }

    void SpawnMidMeteor(Vector3 pos)
    {
        GameObject meteor = ObjectPooling.instance.GetMidMeteor();
        meteor.transform.position = pos;
        meteor.transform.rotation = Quaternion.identity;
        meteor.SetActive(true);
    }

    //method which generate new position without repitation
    Vector3 GeneratedPlace(float X, float Y)
    {
        float x, y, z;
        x = X;
        y = Y;
        z = 0;

        return new Vector3(x, y, z);
    }

}
