using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIKETaskList : MonoBehaviour
{
    private List<GameObject> tasks = new List<GameObject>();
    private int taskIndex = 0;

    [SerializeField] private string taskListName;

    private void Awake()
    {
        foreach(Transform t in this.transform)
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

    public int GetTaskCount()
    {
        return tasks.Count;
    }

    public int GetCurrentTaskNumber()
    {
        return taskIndex;
    }

    public string GetTaskListName()
    {
        return taskListName;
    }

}
