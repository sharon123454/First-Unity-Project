using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeText2;

    private void Update()
    {
        timeText.text = gameManager.timer.ToString("F2");
        timeText2.text = gameManager.timer.ToString("F2");
    }
}
