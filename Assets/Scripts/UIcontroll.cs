using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroll : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MakeRoom_Panel;
    public GameObject Room_Empty;
    public GameObject Room_Full;
    
    public void room_Empty_ok()
    {
        Room_Empty.SetActive(false);
    }
    public void is_room_Empty()
    {
        Room_Empty.SetActive(true);
    }

    public void room_Full_ok()
    {
        Room_Empty.SetActive(false);
    }
    public void is_room_Full()
    {
        Room_Empty.SetActive(true);
    }
    public void is_gameStart()
    {
        MakeRoom_Panel.SetActive(true);
    }
    public void gameStart_ok() 
    { 
        MakeRoom_Panel.SetActive(false);
    }
}
