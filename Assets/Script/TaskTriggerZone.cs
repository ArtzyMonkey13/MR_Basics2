using UnityEngine;

public class TaskTriggerZone : MonoBehaviour
{
    [TextArea] public string taskDescription; // Set this in the Inspector
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<TaskManager>().SetTask(taskDescription);
            triggered = true;
        }
    }
}
