using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 spawnKonumu;
    public GameObject zemin;
    private Transform tr;
    public int zeminSayisi;
    public float zeminGenisligi;
    public float minimumy, maximumy;
    void Start()
    {
        Debug.Log(minimumy);
        Debug.Log(maximumy);
        tr = zemin.GetComponent<Transform>();
        Vector3 spawnKonumu = new Vector3();
        Vector3 spawnKonumu2 = new Vector3();

        for (int i = 0; i < zeminSayisi; i++)
        {
            spawnKonumu.y -= Random.Range(minimumy, maximumy);
            spawnKonumu2.y = spawnKonumu.y;
            spawnKonumu.x = Random.Range(-zeminGenisligi, zeminGenisligi);
            spawnKonumu2.x = -1 * spawnKonumu.x;
            
            if(spawnKonumu.x>0.5f)
            {

                spawnKonumu.x = -1.5f;
                spawnKonumu2.x = 1.5f;

                Instantiate(zemin, spawnKonumu, Quaternion.identity);
                Instantiate(zemin, spawnKonumu2, Quaternion.identity);
            }
            else
            {
                Instantiate(zemin, spawnKonumu, Quaternion.identity);
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
