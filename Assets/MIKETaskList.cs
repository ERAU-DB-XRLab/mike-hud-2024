using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETaskList : MonoBehaviour
{

    [SerializeField] private string taskListName;

    private List<GameObject> tasks = new List<GameObject>();
    private int taskIndex = 0;

    private void Awake()
    {
        foreach(Transform t in transform)
        {
            tasks.Add(t.gameObject);
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

    public string GetTaskListName()
    {
        return taskListName;
    }

    public int GetTaskIndex()
    {
        return taskIndex;
    }

    public int GetTaskCount()
    {
        return tasks.Count;
    }

}
