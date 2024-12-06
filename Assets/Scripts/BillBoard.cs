using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //InfoCanvas가 지속적으로 메인카메라를 바라봄
        transform.LookAt(Camera.main.transform);       
    }
}
