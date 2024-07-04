using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelType
{
    ShopPanel = 0,
    EquipmentPanel,
    MainPanel,
    CollectionPanel,
    BattlePanel
}


[ExecuteInEditMode]
public class PanelController : MonoBehaviour
{
    [SerializeField] private List<RectTransform> panels;
    [SerializeField] private List<Button> tabButtons; // Canvas - Tab - Buttons
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private RectTransform content; // Canvas - Scroll View
    [SerializeField] private RectTransform canvasRect;

    private float tabWidth = 1080f; // 1080 x 1920 Default 

    public PanelType panelType;  // Set Start Panel in editor 

    private void Awake()
    {
        Init();
        TabButtonsAddListenr();
    }

    private void Init()
    {
        SetPanelWidth();
        ShowSelectedPanel();
    }



    private void SetPanelWidth()
    {
        tabWidth = canvasRect.rect.width;
        // Debug.Log(tabWidth);

        foreach (RectTransform panel in panels)
        {
            Vector2 size = panel.sizeDelta;
            size.x = tabWidth;
            panel.sizeDelta = size;
            Debug.Log(panel.rect.width);
        }
    }

    private void ShowSelectedPanel()
    {
        Vector2 newPosition = content.anchoredPosition;
        newPosition.x = -((int)panelType) * tabWidth;
        content.anchoredPosition = newPosition;
    }

    private void TabButtonsAddListenr()
    {
        int buttonCount = tabButtons.Count;
        for (int i = 0; i < buttonCount; i++)
        {
            int index = i;
            tabButtons[i].onClick.AddListener(() => SetContentPosition(index));
        }
    }

    private void SetContentPosition(int tabIndex)
    {
        Vector2 newPosition = content.anchoredPosition;
        newPosition.x = -tabIndex * tabWidth;
        content.anchoredPosition = newPosition;
    }

}