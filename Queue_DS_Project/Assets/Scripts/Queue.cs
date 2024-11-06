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

    public void AddSound(AudioClip ac) //Adds to the queue the piano sound
    {
        mySources.Enqueue(ac);
        AudioSource AS = GetComponent<AudioSource>(); //Checks the unity if there is an audio source
        AS.PlayOneShot(ac); //plays the note
    }

    public void RemoveCommand() //Remove from queue
    {
        if (myQueue.Count == 0) //Counts how many things are in a queue
        {
            Debug.Log("Nothing left to run");
            return;
        }
        string cmd = myQueue.Dequeue(); //Dequeues the string
        AudioClip snd = mySources.Dequeue(); //Deques the sound
        Debug.Log(cmd);
        Debug.Log(snd);

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
