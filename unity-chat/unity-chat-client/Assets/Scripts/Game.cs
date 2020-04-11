using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Scene
{
    Menu = 0,
    Chat = 1,
}

public class Game : MonoBehaviour
{
    public Sand.Client client;
    public UnityEvent onConnected;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        client.Connect();
        client.Emitter.On("connected", OnConnected);
    }

    private void OnConnected(string data)
    {
        onConnected?.Invoke();
    }

    public void ChangeScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
