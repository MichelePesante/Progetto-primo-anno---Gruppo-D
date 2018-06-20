using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextController : MonoBehaviour, ISelectHandler, IDeselectHandler {

    private RectTransform myRectTransform;
    private Vector3 SmallScale;
    private Vector3 BigScale;
    private Vector3 CurrentScale;

    private void Start()
    {
        SmallScale = new Vector3(1f, 1f, 1f);
        BigScale = new Vector3(1.3f, 1.3f, 1f);
        CurrentScale = SmallScale;
        myRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (myRectTransform)
            myRectTransform.localScale = CurrentScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        CurrentScale = BigScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        CurrentScale = SmallScale;
    }
}
