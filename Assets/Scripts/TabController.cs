using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pages;
    private Color unselectedColor = Color.gray;
    private Color selectedColor = Color.white;

    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            SetTabState(i, false);
        }
        pages[tabIndex].SetActive(true);
        SetTabState(tabIndex, true);
    }


    private void SetTabState(int i, bool selected)
    {
        Color c = selected ? selectedColor : unselectedColor;

        tabImages[i].color = c;

        ButtonChangeTint hover = tabImages[i].GetComponent<ButtonChangeTint>();
        if (hover != null)
            hover.SetBaseColors(c);
    }
}
