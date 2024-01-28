using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ReleaseFile : MonoBehaviour
{
    public ShredderController shredderController; // Assuming the FileMiniGame script is attached to the same GameObject

    public FileType machineType;

    public Animator _animator;

    public AudioSource shredAudio;
    public AudioSource mailAudio;
    void OnTriggerEnter2D(Collider2D other)
    {
        FileInteraction fileInteraction = other.GetComponent<FileInteraction>();
        
        if(!fileInteraction) return;
        
        _animator.SetTrigger("RunMachine");
        bool rightFile = fileInteraction.fileType == machineType;
        
        if (fileInteraction != null)
        {
            shredderController.HandleFileReleased(rightFile,fileInteraction);
        }

        if (shredAudio && machineType == FileType.Shred)
        {
            shredAudio.PlayOneShot(shredAudio.clip);
        }

        if (mailAudio && machineType == FileType.Mail)
        {
            mailAudio.PlayOneShot(mailAudio.clip);
        }
    }
}