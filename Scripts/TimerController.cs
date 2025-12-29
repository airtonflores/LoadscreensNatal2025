using UnityEngine;
using TMPro;

/*É o script do timer e a configuração das fontes
*/

public class TimerController : MonoBehaviour
{
    public float timeInitial = 26f; // Valor de reinicializa��o
    public float timeRemaining = 26f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText; // Refer�ncia que voc� arrasta no Inspector

    void Start()
    {
        timeRemaining = timeInitial;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // --- LOGICA DE REINICIAR ---
                Debug.Log("Tempo esgotado! Reiniciando...");
                timeRemaining = timeInitial; // Volta para o tempo inicial (ex: 30)
                // O timerIsRunning continua true, entaoo ele recomeca imediatamente
            }
        }
    }

    public void ResetTimer()
    {
        timeRemaining = timeInitial;
        timerIsRunning = true; // Garante que ele comece a rodar ao resetar
        DisplayTime(timeRemaining); // Atualiza o visual na hora
        Debug.Log("Timer Resetado para: " + timeInitial);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeText == null)
        {
            timeText = GetComponent<TextMeshProUGUI>();
            if (timeText == null) return;
        }

        float tempoSeguro = Mathf.Max(0, timeToDisplay);
        int segundos = Mathf.FloorToInt(tempoSeguro);
        int milissegundos = Mathf.FloorToInt((tempoSeguro % 1f) * 100);

        string s = segundos.ToString("00");
        string m = milissegundos.ToString("00");

        // Monta a string usando as tags de Sprite do TextMesh Pro
        //timeText.text = $"<sprite name=\"{s[0]}\"><sprite name=\"{s[1]}\"><sprite name=\":\"><sprite name=\"{m[0]}\"><sprite name=\"{m[1]}\">";
        timeText.text = $"<sprite name=\"{s[0]}\"><sprite name=\"{s[1]}\"><sprite name=\"s\">";
    }
}