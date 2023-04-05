using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TaskController deals with task UI
public class TaskController : MonoBehaviour
{
    [SerializeField] private Task[] taskObjs;
    [SerializeField] private GameObject taskPrefab; // a UI task row prefab
    private int currentTaskIndex = 0;
    private GameObject currentTaskObj;

    private void DoorUnlockEvent_OnDoorUnlock(object sender, EventArgs e)
    {
        // Check if previous task has finished to unlock the door
        if (currentTaskIndex < 1)
        {
            return;
        }
        GameObject.Find("Door_C").transform.rotation = Quaternion.Euler(0, 0, 0);
        CompleteTask();
    }

    private void KeyPickedEvent_OnKeyPickedUp(object sender, EventArgs e)
    {
        CompleteTask();
    }

    void AddNewTask() 
    {
        currentTaskObj = Instantiate(taskPrefab, gameObject.transform);
        currentTaskObj.transform.Find("TaskDescription").GetComponent<Text>().text = taskObjs[currentTaskIndex].taskDescription;

        // Move down the task if not the init one
        if (currentTaskIndex != 0)
        {
            currentTaskObj.GetComponent<RectTransform>().position -= new Vector3(0,20,0);
        }
    }

    void CompleteTask()
    {
        // Change current task UI to indicate task completed
        currentTaskObj.transform.Find("Background").GetComponent<Image>().color = new Color(30.0f/255.0f, 255.0f/255.0f, 0.0f/255.0f);

        // Check if all tasks finished, if not then add new task
        if (currentTaskIndex >= taskObjs.Length - 1)
        {
            print("All tasks finished!");
            return;
        }
        currentTaskIndex++;
        AddNewTask();
    }

    void Start()
    {
        // Get reference to the event publisher scripts
        KeyPickedEvent keyPickedEvent = GameObject.Find("rust_key").GetComponent<KeyPickedEvent>();
        DoorUnlockEvent doorUnlockEvent = GameObject.Find("DoorTrigger").GetComponent<DoorUnlockEvent>();
        // Subscribe functions to event publishers
        keyPickedEvent.OnKeyPickedUp += KeyPickedEvent_OnKeyPickedUp;
        doorUnlockEvent.OnDoorUnlock += DoorUnlockEvent_OnDoorUnlock;

        // Init the first task
        AddNewTask();
    }
}