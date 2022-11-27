using UnityEngine.UI;
using UnityEngine;

public class MenuReferences : MonoBehaviour
{
    public static MenuReferences Instance;

    [Header("Start Game")]
    public ScrollRect MenuViewSR;
    public RectTransform MenuContainerRT;
    public RectTransform PlaymodeContainerRT;

    [Header("Menu Container Default Position")]
    public Transform DefaultMenuPositionT;

    [Header("Canvas Group")]
    public CanvasGroup MenuCanvas;
    public CanvasGroup PlaymodeCanvas;

    private void Awake()
    {
        Instance = this;
    }
}
