using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private bool sync;

    [HideInInspector]
    public string nickname;
    [HideInInspector]
    public string id;
    public Text nicknameComponent;
    public float velocity = 10f;

    private float lastSyncReceived;
    private float lastSyncDelta;
    private Vector2 lastSyncPosition;
    private Vector2 syncPosition;

    public void Setup(string nickname, string id, float x, float y, bool sync)
    {
        this.id = id;
        this.sync = sync;
        this.nickname = nickname;
        transform.position = new Vector2(x, y);
        nicknameComponent.text = nickname;
        syncPosition = new Vector2(x, y);
        lastSyncPosition = syncPosition;
    }

    private void Update()
    {
        if (sync)
        {
            transform.position = Vector2.Lerp(
                lastSyncPosition,
                syncPosition,
                (Time.time - lastSyncReceived) / lastSyncDelta
            );
        }
    }

    public void SetSyncPosition(Vector2 position) 
    {
        lastSyncDelta = Time.time - lastSyncReceived;
        lastSyncPosition = syncPosition;
        syncPosition = position;
        lastSyncReceived = Time.time;
    }
}
