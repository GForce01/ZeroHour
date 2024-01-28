using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;

[Serializable]
public class FileToSpawn
{
    public FileType type;
    public String folderName;
}

public class FolderSpawner : MonoBehaviour
{
    public GameObject prefab; // Assign your prefab in the Unity Editor
    public List<FileToSpawn> files = new List<FileToSpawn>();
    private int sortingOrder = 1;
    public ShredderController game;

    void Start()
    {
        SpawnFolders();
    }

    void SpawnFolders()
    {

        for (int i = 0; i < files.Count; i++)
        {
            // Instantiate prefab
            GameObject folderInstance = Instantiate(prefab, transform);

            // Access SpriteRenderer component
            SpriteRenderer spriteRenderer = folderInstance.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Increment sorting order
                
                sortingOrder++;
                spriteRenderer.sortingOrder = sortingOrder;
            }

            FileInteraction fileTypeComponent = folderInstance.GetComponent<FileInteraction>();
            if (fileTypeComponent != null)
            { 
                fileTypeComponent.SetFileText(files[i].folderName,files[i].type, sortingOrder);
            } 
            game.files.Add(fileTypeComponent);
        }
    }
}