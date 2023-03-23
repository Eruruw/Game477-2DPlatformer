using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float currTime;
    float startTime = 0f;

    [SerializeField] TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        currTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += 1 * Time.deltaTime;
        timerText.text = "Time: " + currTime.ToString("0.00");

    }
}
