using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // create instance of canvas

    [Header ("Panel Properties")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI UpgradeScoreText;
    [SerializeField] private GameObject _upgradeMenu;

    [Header ("Score Values")]
    private int score;
    [SerializeField] private int _upgradeScore; // holds score until next upgrade
    [SerializeField] private int _distanceForNextUpgrade; // when an upgrade is reached, how much does it increase for next one?
    private int _upgradeTotalHolder; // holds the last previous max upgrade score

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _upgradeTotalHolder = _upgradeScore;
        ScoreText.text = "SCORE: " + score.ToString();
        UpgradeScoreText.text = "SCORE UNTIL NEXT UPGRADE: " + _upgradeScore.ToString();
    }

    public void AddPoint()
    {
        score+=1;
        _upgradeScore-=1;
        if(_upgradeScore == 0)
        {
            _upgradeScore = _upgradeTotalHolder + _distanceForNextUpgrade;
            _upgradeTotalHolder = _upgradeScore;
            _upgradeMenu.GetComponent<UpgradeMenu>().activateMenu();
        }
        ScoreText.text = "SCORE: " + score.ToString();
        UpgradeScoreText.text = "SCORE UNTIL NEXT UPGRADE: " + _upgradeScore.ToString();
    }
}
