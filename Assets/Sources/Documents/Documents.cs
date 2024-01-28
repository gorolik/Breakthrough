using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class Documents
{
    private static string _filePath = Application.persistentDataPath + "/Documents.operation";


    public static void TryCollectDocument(int id)
    {
        DocumentBase documentBase;

        if ((documentBase = ReadDocumentBase()) != null)
        {
            foreach (var document in documentBase.Documents)
            {
                if (document.Id == id)
                    return;
            }
        }
        else
        {
            documentBase = new DocumentBase();
        }

        documentBase.AddDocument(id);

        WriteFile(documentBase);
    }

    public static int GetDocumentsCount()
    {
        return ReadDocumentBase().Documents.Count;
    }

    private static DocumentBase ReadDocumentBase()
    {
        if(File.Exists(_filePath) == false)
        {
            InitFile();
        }

        var readJson = File.ReadAllText(_filePath);

        try
        {
            return JsonUtility.FromJson<DocumentBase>(readJson);
        }
        catch
        {
            return null;
        }
    }

    private static void WriteFile(DocumentBase documentBase)
    {
        var writeJson = JsonUtility.ToJson(documentBase);
        File.WriteAllText(_filePath, writeJson);
    }

    private static void InitFile()
    {
        if (File.Exists(_filePath) == true)
            return;

        File.Create(_filePath).Dispose();
        DocumentBase documentBase = new DocumentBase();
        WriteFile(documentBase);
    }

    [Serializable]
    public class DocumentBase
    {
        public List<DocumentData> Documents = new List<DocumentData>();


        public void AddDocument(int id)
        {
            Documents.Add(new DocumentData(id));
        }
    }

    [Serializable]
    public struct DocumentData
    {
        public int Id;
        public bool IsCollected;


        public DocumentData(int id)
        {
            Id = id;
            IsCollected = true;
        }
    }
}