using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Json_ParserBase<T> : MonoBehaviour
{
    protected T collection;
    protected List<T> parsedDataList = new();

    protected virtual void Start()
    {
        // This can be overridden by derived classes to perform additional initialization
    }

    protected void FindFiles(string keyword)
    {
        if (Directory.Exists(Application.streamingAssetsPath))
        {
            var files = Directory.GetFiles(Application.streamingAssetsPath, $"*{keyword}*.json");

            foreach (var file in files)
            {
                var jsonContent = File.ReadAllText(file);
                Debug.Log($"Loaded JSON file: {file}");
                Debug.Log($"JSON content: {jsonContent}");

                try
                {
                    var parsedData = JsonUtility.FromJson<T>(jsonContent);
                    parsedDataList.Add(parsedData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to parse JSON file: {file}. Error: {e.Message}");
                }
            }

            // Assuming we want to use the first parsed data collection
            if (parsedDataList.Count > 0)
                collection = parsedDataList[0];
            else
                Debug.LogError("No JSON files found with the specified keyword.");
        }
        else
        {
            Debug.LogError("StreamingAssets folder not found.");
        }
    }
}