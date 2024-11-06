using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Queue : MonoBehaviour
{
    private Queue<string> myQueue = new Queue<string>();
    private Queue<AudioClip> mySources = new Queue<AudioClip>();
    [SerializeField] private TMP_Text queueText;

    public void AddCommand(string queue) //Adds to the queue
    {
        myQueue.Enqueue(queue);
        UpdateQueueText();
    }

    public void AddSound(AudioClip ac)
    {
        mySources.Enqueue(ac);
        AudioSource as = GetComponent<AudioSource>();
        as.PlayOneShot(ac);
    }

    public void RemoveCommand() //Remove from queue
    {
        if (myQueue.Count == 0) //Counts how many things are in a queue
        {
            Debug.Log("Nothing left to run");
            return;
        }
        string cmd = myQueue.Dequeue();
        AudioClip snd = mySources.Dequeue();
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
