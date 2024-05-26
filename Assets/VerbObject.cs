using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VerbObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private string answer;

    public UnityEvent OnRightAnswer;

    public void CheckAnswer(string inAnswer)
    {
        if (inAnswer.Equals(answer)) OnRightAnswer.Invoke();
    }

    public void SetTitle(string inText)
    {
        title.text = inText;
    }

    public void SetAnswer(string inAnswer)
    {
        answer = inAnswer;
    }
}