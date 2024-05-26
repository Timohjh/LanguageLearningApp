using System;
using System.Collections.Generic;
using UnityEngine;

public class Json_Verbs : Json_ParserBase<DataCollection>
{
    protected override void Start()
    {
        base.Start();
        FindFiles("Verb");

        if (collection != null)
        {
            Debug.Log("Title: " + collection.title);
            Debug.Log("Type: " + collection.type);
            Debug.Log("Conjugations: " + collection.conjugations);
            Debug.Log("Pronouns: " + string.Join(", ", collection.pronouns));

            var data = GetVerbById(1);
            if (data != null)
            {
                Debug.Log("Verb ID: " + data.id);
                Debug.Log("English Verb: " + data.e);
                Debug.Log("Infinitive: " + data.i);
            }
        }
    }

    public VerbData GetVerbById(int id)
    {
        if (collection != null && collection.verbs != null)
            foreach (var verb in collection.verbs)
                if (verb.id == id)
                    return verb;

        Debug.LogWarning("Verb with ID " + id + " not found.");
        return null;
    }

    public string[] GetPronouns()
    {
        return collection?.pronouns;
    }

    public string GetTitle()
    {
        return collection?.title;
    }

    public string GetTypeOfVerb()
    {
        return collection?.type;
    }

    public int GetConjugations()
    {
        return collection?.conjugations ?? 0;
    }
}

[Serializable]
public class DataCollection
{
    public string title;
    public string type;
    public int conjugations;
    public string[] pronouns;
    public VerbData[] verbs;
}

[Serializable]
public class VerbData
{
    public int id;
    public string e; // English verb
    public string i; // Infinitive
    public VerbConjugation f; // Conjugations
}

[Serializable]
public class VerbConjugation
{
    public List<string> p; // Present tense conjugations
    public List<string> m; // Imperfect tense conjugations
    public List<string> f; // Future tense conjugations
    public List<string> c; // Conditional tense conjugations
    public List<string> s; // Subjunctive mood conjugations
    public List<string> e; // Imperative mood conjugations
}