using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kameraControl : MonoBehaviour
{
    // public transform target;
    public Transform bg1;
    public Transform bg2;
    public Transform wall1_1;
    public Transform wall1_2;
    public Transform wall2_1;
    public Transform wall2_2;

    private float size;
    private float wallSize;
    public TMP_Text scoreValueText;
    public float scoreValue = 30.0f;
    public float pointDecreasePerSecond = 1f;

    /// <Zemin değişkenleri>
    Vector3 spawnKonumu;
    public GameObject zemin;
    public GameObject fener;

    private Transform tr;
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public int zeminSayisi;
    public float zeminGenisligi;
    public float minimumy, maximumy;
    private float sonZeminElemaniYDegeri;
    private float sonZeminElemaniY = -3.0f;
    private float _yenilenme_noktasi = -3.0f;
    private float ilkZeminElemaniYDegeri;
    public List<GameObject> zeminGroup1 = new List<GameObject>();
    public List<GameObject> zeminGroup2 = new List<GameObject>();

    public List<GameObject> zeminGroupTemp = new List<GameObject>();
    /// </Zemin değişkenleri>
    // Start is called before the first frame update



    void Start()
    {
        
        ZeminCreate(sonZeminElemaniY,7);
        zeminGroupTemp = zeminGroup1;
        // ZeminCreate(sonZeminElemaniY);



        size = (bg1.GetComponent<BoxCollider2D>().size.y) * -3f;

        wallSize = (wall1_1.GetComponent<BoxCollider2D>().size.y) * -70f;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("AAA "+transform.position.y);
        // Debug.Log("BBB "+ ( bg2.position.y));
       


        if (transform.position.y <= (bg2.position.y))
        {
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + size, bg1.position.z);
            wall1_1.position = new Vector3(wall1_1.position.x, wall1_2.position.y + size, wall1_1.position.z);
            wall2_1.position = new Vector3(wall2_1.position.x, wall2_2.position.y + size, wall2_1.position.z);
            SwitchBg();
        }
        if (transform.position.y <= _yenilenme_noktasi)
        {

            DestroyZeminGroup(); //ilk 10 elemanını sil


            //   ZeminCreateOneGroup();

        }
    }
   
    private void SwitchBg()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;


        Transform temp1 = wall1_1;
        wall1_1 = wall1_2;
        wall1_2 = temp1;

        Transform temp2 = wall2_1;
        wall2_1 = wall2_2;
        wall2_2 = temp2;
    }

    private void ZeminCreate(float zeminEleman,int kacFenerOlsun)
    {
        //  tr = zemin.GetComponent<Transform>();
        Vector3 spawnKonumu = new Vector3();
        Vector3 spawnKonumu2 = new Vector3();
        Vector3 fenerSpawnKonumu=new Vector3();
        List<int> _fenerKonum = new List<int>();
    
        for (int i = 0; i < kacFenerOlsun; i++)
        {
            
           int _tempKonum= Random.Range(0, zeminSayisi);
            if(!_fenerKonum.Contains(_tempKonum))
            {
            _fenerKonum.Add(_tempKonum);
            }
            else{
                i--;
            }
                    
            
        }
        foreach (var item in _fenerKonum)
        {
            Debug.Log("Fener konum... " + item);
        }
        
        Random.Range(minimumy, maximumy);

        for (int i = 0; i < zeminSayisi; i++)
        {
            if (i == 0)
            {
                spawnKonumu.y += zeminEleman;
            }
            
            spawnKonumu.y -= Random.Range(minimumy, maximumy);
            sonZeminElemaniY = spawnKonumu.y;


            spawnKonumu2.y = spawnKonumu.y;
            
            spawnKonumu.x = Random.Range(-zeminGenisligi, zeminGenisligi);
            spawnKonumu2.x = -1 * spawnKonumu.x;


                ///Fener spawn konumları
               fenerSpawnKonumu.y= spawnKonumu.y+0.4f;
               fenerSpawnKonumu.x= spawnKonumu.x;
                ///Fener spawn konumları
            if (spawnKonumu.x > 0.5f) //
            {

                spawnKonumu.x = -1.5f;
                spawnKonumu2.x = 1.5f;

                GameObject _temp1 = Instantiate(zemin, spawnKonumu, Quaternion.identity);
                _temp1.name = "zemin" + i;
                GameObject _temp2 = Instantiate(zemin, spawnKonumu2, Quaternion.identity);
                _temp2.name = "zemin" + i;

                if (i < zeminSayisi)
                {
                    zeminGroup1.Add(_temp1);
                    zeminGroup1.Add(_temp2);
                }
                if (i == (zeminSayisi - 3))
                {
                    _yenilenme_noktasi = _temp1.transform.position.y;
                }


            }
            else
            {
                GameObject _temp3 = Instantiate(zemin, spawnKonumu, Quaternion.identity);
                _temp3.name = "zemin" + i;
                if (i < zeminSayisi)
                {

                    zeminGroup1.Add(_temp3);
                }
                if (i == (zeminSayisi - 3))
                {
                    _yenilenme_noktasi = _temp3.transform.position.y;
                }
            }
            foreach (var item in _fenerKonum)
            {
            }
           
            if(_fenerKonum.Contains(i))
            {
                Debug.Log("Fener oluşturulacaktır....");

                GameObject _yeniFener = Instantiate(fener, fenerSpawnKonumu, Quaternion.identity);
                
            }



        }
        Debug.Log("0zeminGroup1: " + zeminGroup1.Count);







    }


    public void DestroyZeminGroup()
    {

        Debug.Log("zeminGroup1: " + zeminGroup1.Count);



        for (int i = 0; i < 10; i++)
        {
            if (zeminGroup1.Count > zeminSayisi / 2)
            {
                for (int j = 0; j < zeminGroup1.Count / 2; ++j)
                {
                    Destroy(zeminGroup1[j]);
                    zeminGroup1.RemoveAt(j);
                    // Debug.Log(i + 1 + ". dongu ");
                }

            }
            else
            {
                break;
            }
        }
        Debug.Log("2zeminGroup1: " + zeminGroup1.Count);
        zeminGroup1 = new List<GameObject>();
        Debug.Log("3zeminGroup1: " + zeminGroup1.Count);
        ZeminCreate(sonZeminElemaniY,3); //sona 10 eleman daha ekle


    }
}
