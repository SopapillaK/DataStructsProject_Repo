using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using Unity.VisualScripting;

public class Queue : MonoBehaviour
{
    private Queue<string> myQueue = new Queue<string>();
    private Queue<AudioClip> myAudioSources = new Queue<AudioClip>();
    private Queue<ParticleSystem> myParticleSystemSources = new Queue<ParticleSystem>();
    [SerializeField] private TMP_Text queueText;
    [SerializeField] private TMP_Text dequeueText;

    public void AddCommand(string queue) //Adds to the queue, cant take more than one argument so we had to make a seperate queue for the audio
    {
        myQueue.Enqueue(queue);
        UpdateQueueText();
    }

    public void AddSound(AudioClip audioClip) //Adds to the queue the piano sound
    {
        AudioSource audioSource = GetComponent<AudioSource>(); //Checks the unity if there is an audio source
        audioSource.PlayOneShot(audioClip); //plays the note
        myAudioSources.Enqueue(audioClip); //adds the audio to the queue
    }

    public void AddParticleEffect(ParticleSystem particleSystem)
    {
        particleSystem.Play();
        myParticleSystemSources.Enqueue(particleSystem);
        Debug.Log("Particle Added");
    }

    public void RemoveCommand() //Remove from queue
    {
        if (myQueue.Count == 0) //Counts how many things are in a queue
        {
            Debug.Log("Nothing left to run");
            return;
        }
        string cmd = myQueue.Dequeue(); //Dequeues the string
        AudioClip snd = myAudioSources.Dequeue(); //Dequeues the sound
        ParticleSystem ps = myParticleSystemSources.Dequeue(); //Dequeues the Particle System

        AudioSource audioSource = GetComponent<AudioSource>(); //Checks the unity if there is an audio source
        audioSource.PlayOneShot(snd); //plays the note

        ps.Play(); //plays the particle effect
  
        dequeueText.text += cmd + " ";
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
