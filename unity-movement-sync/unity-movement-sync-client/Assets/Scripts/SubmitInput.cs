using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class StringUnityEvent : UnityEvent<string> { }

public class SubmitInput : MonoBehaviour
{
    public InputField inputField;
    public Button button;
    public StringUnityEvent onSubmit;

    public void Submit()
    {
        if (inputField.text.Trim() != "")
        {
            onSubmit?.Invoke(inputField.text.Trim());
            inputField.text = "";
        }
    }

    public void SetInteractable(bool interactable)
    {
        inputField.interactable = interactable;
        button.interactable = interactable;
    }
}
