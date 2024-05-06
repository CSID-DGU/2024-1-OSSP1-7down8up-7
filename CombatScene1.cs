using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CombatScene1 : MonoBehaviour
{
   
    void Start()
    {
       
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GameObject.Find("Player").transform.position;
       // Vector3 top_door = new Vector3(0.0f, 4.0f, pos.z);
        Vector3 bottom_door = new Vector3(0.0f, -4.0f, pos.z);
        Vector3 right_door = new Vector3(7.0f, 0.0f, pos.z);
        //Vector3 left_door = new Vector3(-7.0f, 0.0f, pos.z);
        float term = 0.1f;
        //bool top_door_check = (pos.x < top_door.x + term) && (pos.x > top_door.x - term) && (pos.y < top_door.y + term) && (pos.y > top_door.y - term);
        bool bottom_door_check = (pos.x < bottom_door.x + term) && (pos.x > bottom_door.x - term) && (pos.y < bottom_door.y + term) && (pos.y > bottom_door.y - term);
        bool right_door_check = (pos.x < right_door.x + term) && (pos.x > right_door.x - term) && (pos.y < right_door.y + term) && (pos.y > right_door.y - term);
        //bool left_door_check = (pos.x < left_door.x + term) && (pos.x > left_door.x - term) && (pos.y < left_door.y + term) && (pos.y > left_door.y - term);

        if (right_door_check)
        {
            SceneManager.LoadScene("CombatScene2");
        }
        if (bottom_door_check)
        {
            SceneManager.LoadScene("CombatScene4");
        }

    }
}

