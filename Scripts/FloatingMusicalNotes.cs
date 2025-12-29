

/*Este é o script que faz com que as notas musicais
subam pelo ar utilizando string.
Ele possui um bug pois está enviando um character vazio,
Aquele quadradinho é entrada anormal no sistema,
pois mesmo com fontes configuradas com todos os characteres
o erro persiste. 
SourceHansSerif-Regular SDF
contendo todos os caractéries para as linguagens jp, kr e cn (faltando 1)

*/
using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingMusicalNotes : MonoBehaviour
{
    [Header("Notas musicais (Unicode)")]
    [SerializeField] private string[] notes = new string[] { "♪", "♫", "♯" };

    [Header("Configuração UI (Ajuste conforme o tamanho do Canvas)")]
    [SerializeField] private Vector2 spawnArea = new Vector2(1f, 1f); // Reduzido para Canvas pequenos
    [SerializeField] private float riseSpeed = 0.5f;                // Velocidade menor para escala reduzida
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int fontSize = 24;                     // Fonte menor

    [Header("Movimento em Espiral")]
    [SerializeField] private float radius = 0.2f;                   // Raio menor
    [SerializeField] private float angularSpeed = 2f;

    [Header("Prefab (TextMeshPro - Text UI)")]
    [SerializeField] private TextMeshProUGUI notePrefab;

    void Start()
    {
        if (notePrefab != null)
            StartCoroutine(SpawnNotesLoop());
    }

    IEnumerator SpawnNotesLoop()
    {
        while (true)
        {
            SpawnNote();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnNote()
    {
        if (notePrefab == null) return;

        string symbol = notes[Random.Range(0, notes.Length)];
        TextMeshProUGUI note = Instantiate(notePrefab, transform);

        note.text = symbol;
        note.fontSize = fontSize;
        note.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);

        RectTransform rt = note.rectTransform;

        // Resetar escala para garantir que não venha gigante do prefab
        rt.localScale = Vector3.one;

        // Posicionamento inicial relativo ao centro do objeto pai
        rt.localPosition = new Vector3(
            Random.Range(-spawnArea.x * 0.5f, spawnArea.x * 0.5f),
            Random.Range(-spawnArea.y * 0.5f, spawnArea.y * 0.5f),
            0f
        );

        StartCoroutine(AnimateNoteSpiral(note, lifetime, radius, angularSpeed));
    }

    IEnumerator AnimateNoteSpiral(TextMeshProUGUI note, float lifetime, float r, float speed)
    {
        float timer = 0f;
        Vector3 startPos = note.rectTransform.localPosition;
        float startAngle = Random.Range(0f, Mathf.PI * 2f);
        Color baseColor = note.color;

        while (timer < lifetime)
        {
            if (note == null) yield break;
            timer += Time.deltaTime;
            float t = timer / lifetime;

            // Movimento
            float yOffset = riseSpeed * timer;
            float angle = startAngle + speed * timer;
            float xOffset = Mathf.Cos(angle) * r;

            note.rectTransform.localPosition = startPos + new Vector3(xOffset, yOffset, 0f);

            // Fade out
            note.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f - t);

            yield return null;
        }

        if (note != null) Destroy(note.gameObject);
    }
}