using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    private const int totalStats = 6;

    private Coroutine runningCoroutine;

    public Button nextButton;
    public TextMeshProUGUI leftStat;
    public TextMeshProUGUI rightStat;
    public TextMeshProUGUI leftScore;
    public TextMeshProUGUI rightScore;

    public TextMeshProUGUI[] scoreLabels;
    public TextMeshProUGUI[] scoreValues;

    public TextMeshProUGUI totalScore;

    private float scoreDelay = 1f;
    private float totalScoreDuration = 1f;
    private int statsPerColumn = 3;

    private int[] scores;
    private int finalScore;

    private void Awake()
    {
        scoreLabels = new[] {leftStat, rightStat}; 
        scoreValues = new[] {leftScore, rightScore};
        nextButton.onClick.AddListener(OnNext);

        scores = new int[] { 10, 20, 30, 40, 50, 60 };
    }


    public override void Open()
    {
        base.Open();

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }

        ResetStats();

        runningCoroutine = StartCoroutine(CoShowScore());
    }

    public override void Close()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        base.Close();
    }

    void ResetStats()
    {
        leftStat.text = string.Empty;
        rightStat.text = string.Empty;
        leftScore.text = string.Empty;
        rightScore.text = string.Empty;
        totalScore.text = "000000000";
        finalScore = 1000000;
    }

    IEnumerator CoShowScore()
    {
        for (int i = 0; i < totalStats; i++)
        {
            yield return new WaitForSeconds(scoreDelay);

            int column = i / statsPerColumn;
            string newLine = i % statsPerColumn == 0 ? string.Empty : "\n";
            scoreLabels[column].text = $"{scoreLabels[column].text}{newLine}Stat {i + 1}";
            scoreValues[column].text = $"{scoreValues[column].text}{newLine}{scores[i]}";
        }

        
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / totalScoreDuration;

            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            totalScore.text = $"{current:D9}";
            yield return null;
        }

        totalScore.text = $"{finalScore:D9}";
        runningCoroutine = null;
    }



    public void OnNext()
    {
        windowManager.Open((int)Window.Title);
    }

}
