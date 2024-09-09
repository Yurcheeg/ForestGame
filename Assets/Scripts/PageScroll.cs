using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageScroll : MonoBehaviour
{
    [SerializeField] private List<Image> pages = new();
    private int currentPage;
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].gameObject.SetActive(false);
            currentPage--;
        }
    }
    public void NextPage()
    {
        currentPage++;
        pages[currentPage].gameObject.SetActive(true);
    }
    public void RemovePage()
    {
        for (int i = currentPage; i >= 0; i--)
        {
            pages[i].gameObject.SetActive(false);
        }
        currentPage = 0;
    }
}
