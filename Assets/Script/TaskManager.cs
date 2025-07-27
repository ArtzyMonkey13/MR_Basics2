using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private TMP_Text taskText;

    private string currentTask;

    public void SetTask(string taskDescription)
    {
        currentTask = taskDescription;
        taskText.text = "Task: " + currentTask;
    }

    public void CompleteTask()
    {
        taskText.text = "Task Complete!";
    }
}
