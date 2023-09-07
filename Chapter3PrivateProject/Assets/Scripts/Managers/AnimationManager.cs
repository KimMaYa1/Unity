using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Image characterSelectRenderer;

    private GameObject _player;
    private Animator _anim;
    private bool isCharacter = true;
    public Sprite elf;
    public Sprite knight;

    public void CatchPlayer(GameObject player)
    {
        _player = player;
        _anim = _player.GetComponent<Animator>();
    }

    public void IsKnight()
    {
        characterSelectRenderer.sprite = knight;
        characterRenderer.sprite = knight;
        isCharacter = true;
    }
    
    public void IsElf()
    {
        characterSelectRenderer.sprite = elf;
        characterRenderer.sprite = elf;
        isCharacter = false ;
    }

    public void OnPlay()
    {
        _anim.SetBool("IsCharacter", isCharacter);
    }
}
