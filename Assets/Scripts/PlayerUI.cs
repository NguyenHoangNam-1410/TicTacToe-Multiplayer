using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject crossArrowGameObject;
    [SerializeField] private GameObject circleArrowGameObject;
    [SerializeField] private GameObject crossYouTextGameObject;
    [SerializeField] private GameObject circleYouTextGameObject;
    [SerializeField] private TextMeshProUGUI playerCrossScoreTextMesh;
    [SerializeField] private TextMeshProUGUI playerCircleScoreTextMesh;


    private void Awake()
    {
        crossArrowGameObject.SetActive(false);
        circleArrowGameObject.SetActive(false);
        crossYouTextGameObject.SetActive(false);
        circleYouTextGameObject.SetActive(false);

        playerCrossScoreTextMesh.text = "";
        playerCircleScoreTextMesh.text = "";
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnCurrentPlayablePlayerTypeChanged += GameManager_OnCurrentPlayablePlayerTypeChanged;
        GameManager.Instance.OnScoreChanged += GameManager_OnScoreChanged;
    }

    private void GameManager_OnScoreChanged(object sender, EventArgs e)
    {       
        GameManager.Instance.GetScore(out int playerCrossScore, out int playerCircleScore);
        playerCrossScoreTextMesh.text = playerCrossScore.ToString();
        playerCircleScoreTextMesh.text = playerCircleScore.ToString();
    }


    private void GameManager_OnCurrentPlayablePlayerTypeChanged(object sender, EventArgs e)
    {
        UpdateCurrentPlayerArrow();
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        Debug.Log("Game started");
        if (GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Cross)
        {
            Debug.Log("Cross player");
            crossYouTextGameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Circle player");
            circleYouTextGameObject.SetActive(true);
        }
        playerCrossScoreTextMesh.text = "0";
        playerCircleScoreTextMesh.text = "0";
        UpdateCurrentPlayerArrow();
    }

    private void UpdateCurrentPlayerArrow()
    {
        if(GameManager.Instance.GetCurrentPlayablePlayerType() == GameManager.PlayerType.Cross)
        {
            crossArrowGameObject.SetActive(true);
            circleArrowGameObject.SetActive(false);
        }
        else
        {
            crossArrowGameObject.SetActive(false);
            circleArrowGameObject.SetActive(true);
        }
    }
}
