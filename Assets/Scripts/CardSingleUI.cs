using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardSingleUI : MonoBehaviour
{
    private CardGroup cardGroup;

    [SerializeField] private Button cardBackButton;
    [SerializeField] private Image cardBackBackground;
    [SerializeField] private Image cardFrontBackground;
    [SerializeField] private Image cardFrontImage;

    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;

    private bool objectMatch;

    [Header("DoTween Animation")]
    [SerializeField] private Vector3 selectRotation = new Vector3();
    [SerializeField] private Vector3 deselectRotation = new Vector3();
    [SerializeField] private float duration = 0.25f;
    private Tweener[] tweener = new Tweener[3];

    private void Awake()
    {
        cardGroup = transform.parent.GetComponent<CardGroup>();

        if (cardGroup != null)
        {
            cardGroup.Subscribe(this);
        }
        else
        {
            Debug.LogError("CardGroup not found in parent.");
        }
    }

    private void Start()
    {
        if (cardBackButton != null)
        {
            cardBackButton.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("CardBackButton is not assigned.");
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        StartCoroutine(WaitingToHide());
        MemoryGameManagerUI.Instance.Subscribe(this);
    }

    private void OnClick()
    {
        if (cardGroup != null)
        {
            cardGroup.OnCardSelected(this);
        }
    }

    public void Select()
    {
        tweener[0] = transform.DORotate(selectRotation, duration)
            .SetEase(Ease.InOutElastic)
            .OnUpdate(CheckSelectHalfDuration);
    }

    public void Deselect()
    {
        tweener[1] = transform.DORotate(deselectRotation, duration)
            .SetEase(Ease.InOutElastic)
            .OnUpdate(CheckDeselectHalfDuration);
    }

    private IEnumerator WaitingToHide()
    {
        yield return new WaitForSeconds(3f);
        tweener[2] = transform.DORotate(deselectRotation, duration)
            .SetEase(Ease.InOutElastic)
            .OnUpdate(CheckWaitingToHide);
    }

    private void CheckWaitingToHide()
    {
        float elapsed = tweener[2]?.Elapsed() ?? 0;
        float halfDuration = tweener[2]?.Duration() / 2f ?? 0;

        if (elapsed >= halfDuration)
        {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
        }
    }

    private void CheckSelectHalfDuration()
    {
        float elapsed = tweener[0]?.Elapsed() ?? 0;
        float halfDuration = tweener[0]?.Duration() / 2f ?? 0;

        if (elapsed >= halfDuration)
        {
            cardBack.SetActive(false);
            cardFront.SetActive(true);
        }
    }

    private void CheckDeselectHalfDuration()
    {
        float elapsed = tweener[1]?.Elapsed() ?? 0;
        float halfDuration = tweener[1]?.Duration() / 2f ?? 0;

        if (elapsed >= halfDuration)
        {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
        }
    }

    public Image GetCardBackBackground() => cardBackBackground;
    public Image GetCardFrontBackground() => cardFrontBackground;

    public void SetObjectMatch()
    {
        objectMatch = true;
    }

    public void SetCardImage(Sprite sprite)
    {
        if (cardFrontImage != null)
        {
            cardFrontImage.sprite = sprite;
        }
    }

    public bool GetObjectMatch() => objectMatch;

    public void DisableCardBackButton()
    {
        if (cardBackButton != null)
        {
            cardBackButton.interactable = false;
        }
    }
}
