using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETaskList : MonoBehaviour
{
    private List<GameObject> tasks = new List<GameObject>();
    private int taskIndex = 0;

    private void Awake()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for(int i = 1; i < children.Length; i++)
        {
            tasks.Add(children[i].gameObject);
        }
    }

    private void OnEnable()
    {
        foreach(GameObject obj in tasks)
        {
            obj.SetActive(false);
        }
        tasks[0].SetActive(true);
        taskIndex = 0;
    }

    public void NextTask()
    {
        if(taskIndex < tasks.Count - 1)
        {
            tasks[taskIndex].SetActive(false);
            taskIndex++;
            tasks[taskIndex].SetActive(true);
        }
    }

    public void PreviousTask()
    {
        if(taskIndex > 0)
        {
            tasks[taskIndex].SetActive(false);
            taskIndex--;
            tasks[taskIndex].SetActive(true);
        }
    }

}
