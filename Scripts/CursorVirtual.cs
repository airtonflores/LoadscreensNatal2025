using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*Adicionou o cursor virtual
*/

public class CursorVirtual : MonoBehaviour
{
    [Header("Componentes Necessários")]
    public RectTransform cursorVisual; // A imagem da setinha
    public Canvas canvasDoCursor;      // O Canvas para o cursor

    [Header("Configurações de Velocidade")]
    public float velocidadeControle = 1000f;

    [Header("Input Actions")]
    public InputActionAsset acoes;
    private InputAction moverAction;

    private Vector2 posicaoVirtual;

    void Awake()
    {
        // Oculta o cursor padrão do sistema
        Cursor.visible = false;

        // Trava o cursor dentro da janela do jogo (Opcional, mas recomendado)
        Cursor.lockState = CursorLockMode.Confined;

        moverAction = acoes.FindAction("Mover");
        posicaoVirtual = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void OnEnable() => moverAction.Enable();
    void OnDisable() => moverAction.Disable();

    void Update()
    {
        Vector2 delta = moverAction.ReadValue<Vector2>();

        // 1. Se houver movimento de controle ou teclado (WASD/Setas)
        if (delta.magnitude > 0.01f)
        {
            posicaoVirtual += delta * velocidadeControle * Time.deltaTime;
        }
        // 2. Se não, mas o mouse se mover, o cursor virtual "pula" para o mouse
        else if (Mouse.current.delta.ReadValue().magnitude > 0.01f)
        {
            posicaoVirtual = Mouse.current.position.ReadValue();
        }

        // Limita o cursor para não sair da tela
        posicaoVirtual.x = Mathf.Clamp(posicaoVirtual.x, 0, Screen.width);
        posicaoVirtual.y = Mathf.Clamp(posicaoVirtual.y, 0, Screen.height);

        // Converte a posição da tela para a posição dentro do seu Canvas
        Vector2 posicaoNoCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasDoCursor.transform as RectTransform,
            posicaoVirtual,
            canvasDoCursor.worldCamera,
            out posicaoNoCanvas
        );

        cursorVisual.localPosition = posicaoNoCanvas;
    }
}