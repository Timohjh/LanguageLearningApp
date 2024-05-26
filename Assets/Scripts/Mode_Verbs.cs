using System.Collections.Generic;
using UnityEngine;

public class Mode_Verbs : MonoBehaviour
{
    [SerializeField] private GameObject inputPrefab;
    [SerializeField] private Transform inputHolder;

    private readonly List<VerbObject> inputFields = new();
    private VerbData currentData;

    private Json_Verbs json;

    private void Awake()
    {
        json = GetComponent<Json_Verbs>();
    }

    private void Start()
    {
        GenerateRandomVerb();
    }

    public void GenerateRandomVerb()
    {
        var randomVerbId = Random.Range(1, 16); // Assuming IDs are 1 to 15
        Debug.Log($"Generated random verb ID: {randomVerbId}");
        SetNewVerb(randomVerbId);
    }

    private void SetPronouns()
    {
        // Clear any existing input fields
        foreach (Transform child in inputHolder)
            Destroy(child.gameObject);

        inputFields.Clear();

        var pronouns = json.GetPronouns();
        foreach (var pronoun in pronouns)
        {
            var spawnedInput = Instantiate(inputPrefab, inputHolder).GetComponent<VerbObject>();
            spawnedInput.SetTitle(pronoun);
            inputFields.Add(spawnedInput);
        }
    }

    public void SetNewVerb(int id)
    {
        currentData = json.GetVerbById(id);

        if (currentData != null)
        {
            SetPronouns();

            // Setting the English verb title and expected French conjugations for each pronoun
            for (var i = 0; i < inputFields.Count; i++)
            {
                var spawnedInput = inputFields[i];

                // Set the answer for each pronoun (French conjugations)
                var correctAnswer = currentData.f.p[i];
                spawnedInput.SetAnswer(correctAnswer);

                // Set the title to the English verb
                spawnedInput.SetTitle($"{json.GetPronouns()[i]} {currentData.e}");
            }

            Debug.Log($"English Verb: {currentData.e}");
            Debug.Log($"French Infinitive: {currentData.i}");
            Debug.Log($"French Present Conjugations: {string.Join(", ", currentData.f.p)}");
        }
        else
        {
            Debug.LogWarning($"Verb with ID {id} not found.");
        }
    }
}