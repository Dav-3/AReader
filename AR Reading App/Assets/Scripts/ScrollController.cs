using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public float speed = 1f;

    public bool automaticScroll;

    ScrollRect scrollRect;
    RectTransform contentTransform;
    float height;

    // Use this for initialization
    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentTransform = transform.Find("Viewport/Content").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (automaticScroll)
        {
            Scroll(speed);
        }
    }

    public void ToggleAutomaticScrolling()
    {
        automaticScroll = !automaticScroll;
    }

    public void Scroll(float speed)
    {
        height = contentTransform.rect.height;

        if (height != 0)
        {
            scrollRect.verticalNormalizedPosition -= speed * Time.deltaTime / height;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }
}
