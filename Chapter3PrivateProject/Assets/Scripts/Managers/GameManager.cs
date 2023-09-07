using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject grid;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AnimationManager animatinManager;

    private void Awake()
    {
        Camera.main.GetComponent<FollowPlayer>().CatchPlayer(player);
        uiManager.CatchPlayer(player);
        animatinManager.CatchPlayer(player);
        player.SetActive(false);
        grid.SetActive(false);
    }

    public void OnPlay()
    {
        if (uiManager.Play)
        {
            player.SetActive(true);
            grid.SetActive(true);
        }
    }
}
