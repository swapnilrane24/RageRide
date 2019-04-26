using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This script hanle the sliding menu of ship
/// </summary>
[RequireComponent(typeof(Mask))]
public class ShipSelector : MonoBehaviour {

    public static ShipSelector instance;

    [Tooltip("Button to go to the previous button (optional)")]
    public GameObject prevButton;
    [Tooltip("Button to go to the next button (optional)")]
    public GameObject nextButton;
    //private RectTransform _scrollRectRect;
    [SerializeField]
    public RectTransform _container;
    [SerializeField]
    private int _buttonCount;
    [SerializeField]
    private int _currentButton;
    private int limit;

    [SerializeField]
    private float buttonSize;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Vector2 tempPos = _container.anchoredPosition;

        tempPos.x = GameController.instance.anchorPosX;
        tempPos.y = GameController.instance.anchorPosY;

        _container.anchoredPosition = tempPos;

        // prev and next buttons
        if (nextButton)
            nextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

        if (prevButton)
            prevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
    }

    void Update()
    {
        _buttonCount = _container.transform.childCount;
    }

    //------------------------------------------------------------------------
    private void NextScreen()
    {
        if (limit > -(GameController.instance.selectedPlayer) )
        {
            LerpToButton(_currentButton + 1);
            limit--;
        }
    }

    //------------------------------------------------------------------------
    private void PreviousScreen()
    {
        if (limit < (_buttonCount - (1 + GameController.instance.selectedPlayer)))
        {
            LerpToButton(_currentButton - 1);
            limit++;
        }
    }

    private void LerpToButton(int aButtonIndex)
    {
        Vector2 tempPos = _container.anchoredPosition;
        tempPos.x = _container.anchoredPosition.x + aButtonIndex * buttonSize;
        if(aButtonIndex >=0 || aButtonIndex <= _buttonCount)
        _container.anchoredPosition = tempPos;
    }
}
