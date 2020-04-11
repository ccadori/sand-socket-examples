using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StringUnityEvent : UnityEvent<string> { }

public class SubmitInput : MonoBehaviour
{
    public StringUnityEvent onSubmit;
    public InputField inputField;

    public void Submit()
    {
        if (inputField.text.Trim() != "")
        {
            Debug.Log(inputField.text.Trim());
            onSubmit?.Invoke(inputField.text.Trim());
            inputField.text = "";
        }
    }
}
