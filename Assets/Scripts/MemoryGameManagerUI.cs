using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryGameManagerUI : MonoBehaviour
{
    public static MemoryGameManagerUI Instance { get; private set; }

    [SerializeField] private CardGroup cardGroup;
    [SerializeField] private List<CardSingleUI> cardSingleUIList = new List<CardSingleUI>();

    [SerializeField] private GameObject gameArea;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (cardGroup != null)
        {
            cardGroup.OnCardMatch += CardGroup_OnCardMatch;
        }
        else
        {
            Debug.LogError("CardGroup is not assigned in the inspector.");
        }
    }

    public void Subscribe(CardSingleUI cardSingleUI)
    {
        if (cardSingleUIList == null)
        {
            cardSingleUIList = new List<CardSingleUI>();
        }

        if (!cardSingleUIList.Contains(cardSingleUI))
        {
            cardSingleUIList.Add(cardSingleUI);
        }
    }

    private void CardGroup_OnCardMatch(object sender, System.EventArgs e)
    {
        if (cardSingleUIList.All(x => x.GetObjectMatch()))
        {
            StartCoroutine(OnCompleteGame());
        }
    }

    private IEnumerator OnCompleteGame()
    {
        yield return new WaitForSeconds(0.75f);
        PlayerPrefs.SetString("CablePuzzleComplete", "true");

        Debug.Log("Has ganado");
    }

    public void Restart()
    {
        cardSingleUIList.Clear();
    }

    private void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    private void ToggleGameArea(bool toggle)
    {
        gameArea.SetActive(toggle);
    }
}
