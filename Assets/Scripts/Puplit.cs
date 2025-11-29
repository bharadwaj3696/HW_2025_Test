using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Pulpit : MonoBehaviour
{
    float lifeTime;
    float timer;
    bool alreadyScored;

    public TextMeshProUGUI timerText;

    public void SetLife(float life)
    {
        lifeTime = life;
        timer = lifeTime;
    }

    void Update()
    {
        if (lifeTime <= 0) return;

        timer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.Max(0, timer).ToString("F2");

        if (timer <= 0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyScored) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            alreadyScored = true;
            ScoreManager.Instance.AddScore();
        }
    }
}
