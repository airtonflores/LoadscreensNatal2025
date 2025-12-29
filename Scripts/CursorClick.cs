using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CursorClick : MonoBehaviour
{
    public InputActionAsset acoes;
    public RectTransform cursorVisual;

    /*Adicionou clique ao cursor virtual
    */

    private InputAction clickAction;

    void Awake()
    {
        if (acoes == null)
        {
            Debug.LogError("O arquivo de Input Actions não foi arrastado para o script!");
            return;
        }

        // Tenta encontrar a ação "Click"
        clickAction = acoes.FindAction("Interagir");

        if (clickAction == null)
        {
            Debug.LogError("Não foi possível encontrar a ação 'Click'. Verifique o nome no Editor.");
        }
    }

    void OnEnable() => clickAction?.Enable();
    void OnDisable() => clickAction?.Disable();

    void Update()
    {
        if (clickAction != null && clickAction.triggered)
        {
            SimularClique();
        }
    }

    void SimularClique()
    {
        if (EventSystem.current == null) return;

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = cursorVisual.position;

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultados);

        foreach (RaycastResult hit in resultados)
        {
            ExecuteEvents.Execute(hit.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            Debug.Log("Clicou em: " + hit.gameObject.name);
        }
    }
}