using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseGM : MonoBehaviour
{
    // Start is called before the first frame update

    public Text winLoseText;
    
    public Text respectTotalText;
    public Text xpTotalText;

    private int respectValue;
    private int xpValue;

    private float respectDisplayed;
    private float xpDisplayed;

    private string respectOrigText;
    private string xpOrigText;

    private GameOverData _gameOverData;
    private bool scoreTransition;
    public int animationTime = 1;
    private bool lastFrame;

    private void Start()
    {
        _gameOverData = FindObjectOfType<GameOverData>();
        scoreTransition = false;
        respectOrigText = respectTotalText.text;
        xpOrigText = xpTotalText.text;
        respectValue = _gameOverData.totalRespect;
        respectDisplayed = _gameOverData.totalRespect;
        xpDisplayed = 0;

        if (_gameOverData.wonOrLost == GameOverData.GameOverCondition.Win)
        {
            winLoseText.text = "CONGRATULATIONS";
        }
        else
        {
            winLoseText.text = "DEFEATED";
        }

        respectTotalText.text = respectOrigText + respectDisplayed;
        xpTotalText.text = xpOrigText + xpDisplayed;

        StartCoroutine(PauseBeforeAnim());
    }

    private void Update()
    {
        xpTotalText.text = xpOrigText + ((int)xpDisplayed);
        respectTotalText.text = respectOrigText + ((int)respectDisplayed);
        Debug.Log("xpDisplayed value: "+(int)xpDisplayed);
        if (scoreTransition && !lastFrame)
        {Debug.Log("Animating");
            if ((int)xpDisplayed != respectValue)
            {Debug.Log("Animating2");
                xpDisplayed += (animationTime * Time.deltaTime) * (respectValue - xpDisplayed);
                respectDisplayed -=(animationTime * Time.deltaTime) * (respectDisplayed - 1);

                if (xpDisplayed >= respectValue - 1)
                {
                    xpDisplayed = respectValue-1;
                    respectDisplayed = 1;
                    lastFrame = true;
                }
            }
        }

        if (lastFrame)
        {
                xpDisplayed = respectValue;
                respectDisplayed = 0;
        }
    }

    private IEnumerator PauseBeforeAnim()
    {
        yield return new WaitForSeconds(2);
        scoreTransition = true;
    }
}
