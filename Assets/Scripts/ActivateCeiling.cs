using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCeiling : MonoBehaviour
{
    public GameObject ceiling01;
    public GameObject ceiling02;
    public GameObject ceiling03;
    public GameObject ceiling04;
    public GameObject ceiling05;
    public GameObject ceiling06;
    public GameObject ceiling07;
    public GameObject ceiling08;
    public GameObject ceiling09;
    public GameObject ceiling10;
    public GameObject ceiling11DoorWayMenu;

    // Start is called before the first frame update
    void Start()
    {
        ceiling01.SetActive(true);
        ceiling02.SetActive(true);
        ceiling03.SetActive(true);
        ceiling04.SetActive(true);
        ceiling05.SetActive(true);
        ceiling06.SetActive(true);
        ceiling07.SetActive(true);
        ceiling08.SetActive(true);
        ceiling09.SetActive(true);
        ceiling10.SetActive(true);
        ceiling11DoorWayMenu.SetActive(true);
    }

}
