using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Queue : MonoBehaviour
{
    private Queue<string> myQueue = new Queue<string>();
    private Queue<AudioSource> mySources = new Queue<AudioSource>();
    //private AudioSource ac = GetComponent<AudioSource>();
    [SerializeField] private TMP_Text queueText;

    public void AddCommand(string queue) //Adds to the queue
    {
        myQueue.Enqueue(queue);
        UpdateQueueText();
    }

    public void AddSound(AudioSource ac)
    {
        mySources.Enqueue(ac);
        ac.Play();
    }

    public void RemoveCommand() //Remove from queue
    {
        if (myQueue.Count == 0) //Counts how many things are in a queue
        {
            Debug.Log("Nothing left to run");
            return;
        }
        string cmd = myQueue.Dequeue();
        Debug.Log(cmd);

        UpdateQueueText();

        StartCoroutine(DequeueWait());
    }

    public void UpdateQueueText()
    {
        queueText.text = string.Empty;

        foreach(string queue in myQueue)
        {
            queueText.text += queue + ", ";
        }
    }

    IEnumerator DequeueWait()
    {
        yield return new WaitForSeconds(1.0f);

        RemoveCommand();
    }
}
