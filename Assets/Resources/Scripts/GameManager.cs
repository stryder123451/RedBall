using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject StartMenu;
    [SerializeField] public GameObject GameMenu;
    [SerializeField] public GameObject GameOverMenu;
    [SerializeField] public GameObject DifficultPanel;
    [SerializeField] private Button PlayButton;
    [SerializeField][Header("Время:")] public TMP_Text Time;
    [SerializeField] [Header("Попытка:")] public TMP_Text Attempt;
    [HideInInspector] public int Attempts;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.interactable = false;
        GameMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void SetDifficult(int index)
    {
        ActivateButton();
        if (TryGetComponent(out BallManager ballManager))
        {
            ballManager.DifficultLevel = index;
        }
    }

    private void ActivateButton()
    {
        if (PlayButton.TryGetComponent(out Image image))
        {
            PlayButton.interactable = true;
            image.color = Color.green;
        }
    }

   


}
