using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProgress : MonoBehaviour
{
    [SerializeField] private Image progress;
    [SerializeField] private TextMeshProUGUI text;
    private GameModel _gameModel;

    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    private void Update()
    {
        if (_gameModel==null || _gameModel.GameState != GameState.Playing)
        {
            return;
        }
        var model = _gameModel.CharacterModel;
        progress.fillAmount = Mathf.Lerp(progress.fillAmount,1 - (float)model.CurrentAmount / model.Amount,Time.deltaTime * 5 );
        text.text = $"{model.CurrentAmount}/{model.Amount}";
    }
}