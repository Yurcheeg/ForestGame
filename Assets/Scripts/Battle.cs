using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    EndTurn,
    Win,
    Loss
}
public class Battle : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    public BattleState state;

    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject BattleUI;
    public TMP_Text rollText;
    public Image winScreen;
    public Image lossScreen;

    public TMP_Text enemyIntentionText;
    public Image enemyIntentionIcon;

    public List<Button> actionButtons;

    public List<int> dice = new();
    private int rollResult;

    public event EventHandler OnPlayerAction;
    public event EventHandler<Unit> OnTurnEnd;

    EnemyAttacks enemyIntention;

    public StatusEffects statusEffect;
    public int turnCount;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();

        playerAnimator = player.GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();

        PlayerPrefs.SetInt("playerHp", player.maxHealth);//for test
        player.currentHealth = PlayerPrefs.GetInt("playerHp", player.maxHealth);
        enemy.currentHealth = enemy.maxHealth;

        BattleUI.SetActive(true);
        for (int i = 0; i < actionButtons.Count; i++)
        {
            if (actionButtons[i].GetComponentInChildren<TMP_Text>().text == "Defend")
            {
                actionButtons[i].onClick.AddListener(() => Defend());
            }
            if (actionButtons[i].GetComponentInChildren<TMP_Text>().text == "Evade")
            {
                actionButtons[i].onClick.AddListener(() => Evade());
            }
            //item will work differently 
        }

        OnPlayerAction += EnemyTurn;

        OnTurnEnd += statusEffect.ApplyStatusEffects;

        //on evade and on defend to affect enemy damage
        turnCount = 0;
    }
    public void Start()
    {
        TurnStart();

    }
    private void SetBattleState(BattleState newState)
    {
        if (state == BattleState.Win || state == BattleState.Loss)
        {
            SwitchButtons(false);
            OnPlayerAction -= EnemyTurn;
            return;
        }
        state = newState;
    }
    public void TurnStart()
    {
        SetBattleState(BattleState.Start);
        turnCount++;
        //dice roll
        rollResult = 0;
        for (int i = 0; i < dice.Count; i++)
        {
            int die = UnityEngine.Random.Range(1, 7);
            Debug.Log($"Die rolled {die}");
            rollText.text = $"Dice roll: {rollResult += die}";
        }

        //show enemy intentions
        enemyIntention = EnemyIntention();
        ShowEnemyIntention();
        Debug.Log($"enemy wants to use {enemyIntention}");
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        //List of buttons. when one is triggered the attack plays and the turn goes to the enemy
        SetBattleState(BattleState.PlayerTurn);
        if (state == BattleState.PlayerTurn)
        {
            SwitchButtons(true);
            Debug.Log("it's player's turn");
        }
    }
    EnemyAttacks EnemyIntention()
    {
        for (int i = 0; i < enemy.enemyAttacks.Count; i++)
        {
            if (enemy.enemyAttackId[i].appliesStatus && player.affectedByStatusEffect == null)
            {
                var enemyAttack = enemy.enemyAttackId[i];
                return enemyAttack;
            }
        }
        return EnemyIntentionReroll();
    }
    EnemyAttacks EnemyIntentionReroll()
    {
        for (int i = 0; i < 1000; i++)
        {
            int randomAttackId = UnityEngine.Random.Range(0, enemy.enemyAttacks.Count);
            if (!enemy.enemyAttackId[randomAttackId].appliesStatus)
            {
                return enemy.enemyAttackId[randomAttackId];
            }
        }
        return enemy.enemyAttackId[0];
    }
    void ShowEnemyIntention()
    {
        if (enemyIntention.appliesStatus && enemyIntention.statusEffect.icon != null)
        {
            enemyIntentionIcon.sprite = enemyIntention.statusEffect.icon;
        }
        else enemyIntentionIcon.sprite = enemyIntention.attackSprite;
        enemyIntentionText.text = $"{enemyIntention.damage}x{enemyIntention.attackCount}";
    }

    void EnemyTurn(object sender, EventArgs e)
    {
        SetBattleState(BattleState.EnemyTurn);
        if (state == BattleState.EnemyTurn)
        {
            Debug.Log("enemy turn");

            
            StartCoroutine(EnemyPlayAttack(sender));
        }
    }
    IEnumerator EnemyPlayAttack(object sender)
    {
        yield return StartCoroutine(PlayAnimation($"{enemyIntention.animationTriggerName}", enemy));
        int dmg = 0;
        switch ((string)sender)
        {
            case "Defend":
                dmg = enemyIntention.damage / 2;//cut in half
                for(int i = 0; i<enemyIntention.attackCount;i++)
                {
                    player.currentHealth -= dmg;
                    Debug.Log("Damage was cut in half");
                    Debug.Log($"{enemyIntention.damage}[{enemyIntention.damage / 2}]");
                }
                break;

            case "Evade" when rollResult > enemyIntention.damage*enemyIntention.attackCount:
                dmg = enemyIntention.damage * 0;// 0 damage
                for(int i=0;i<enemyIntention.attackCount;i++)
                {
                    player.currentHealth -= dmg;
                    Debug.Log("Dodged the attack");
                    Debug.Log($"{enemyIntention.damage}[{enemyIntention.damage * 0}]");
                }
                break;

            default:
                dmg = enemyIntention.damage;
                for(int i=0;i<enemyIntention.attackCount; i++)
                {
                    player.currentHealth -= dmg;
                    Debug.Log($"player got hit by {enemyIntention}");
                }
                break;
        }
        Debug.Log($"dmg{dmg}");
        if(dmg > 0)
        {
            yield return StartCoroutine(HitFlash(player));
        }
        if (enemyIntention.appliesStatus)
        {
            statusEffect.statusCall[enemyIntention.statusEffect.name](player);//get status effect and apply to player
            Debug.Log("status effect applied");
        }
        DeathCheck();
        TurnEnd();
    }
    void DeathCheck()
    {
        if (player.currentHealth <= 0)
        {
            BattleLoss();
            StartCoroutine(PlayAnimation("Death", player));
        }
        else if (enemy.currentHealth <= 0)
        {
            BattleWin();
            StartCoroutine(PlayAnimation("Death",enemy));
        }
    }
    public void BattleWin()
    {

        SetBattleState(BattleState.Win);
        Debug.Log("battle won");
        PlayerPrefs.SetInt("playerHp", player.currentHealth);
        PlayerPrefs.Save();
        winScreen.gameObject.SetActive(true);
        //show stuff when won. Buttons are turned off
    }
    public void BattleLoss()
    {
        SetBattleState(BattleState.Loss);
        Debug.Log("battle lost");
        PlayerPrefs.SetInt("playerHp", player.maxHealth);
        PlayerPrefs.Save();
        lossScreen.gameObject.SetActive(true);
        //game over screen, buttons to start new game or come back to menu. Buttons are turned off
    }
    public void TurnEnd()
    {
        SetBattleState(BattleState.EndTurn);
        //check for status conditions and return turn to the player
        List<Unit> targets = new List<Unit>();
        targets.Add(enemy);
        targets.Add(player);
        for (int i = 0; i < targets.Count; i++)
            if (targets[i].affectedByStatusEffect != null)
            {
                OnTurnEnd.Invoke(this, targets[i]);
            }
        //death check :)
        DeathCheck();
        TurnStart();

    }
    public IEnumerator Attack(Attack attack)
    {
        SwitchButtons(false);
        //List of buttons. when one is triggered the attack plays and the turn goes to the enemy
        int damage = 0;
        
        if (attack.appliesStatusEffect != null && enemy.affectedByStatusEffect == null)
        {
            statusEffect.statusCall[attack.appliesStatusEffect.name](enemy); //get status effect and apply to enemy

        }
        for (int i = 0; i < attack.attackCount; i++)
        {
            //attack animations
            yield return StartCoroutine(PlayAnimation("PlayerAttack", player));
            Debug.Log("attack animation played");

            damage = rollResult * attack.damageMultiplier;
            enemy.currentHealth -= damage;
            Debug.Log($"Dealt {damage} damage to {enemy.name}");
            Debug.Log($"enemy hp: {enemy.currentHealth}");

            yield return StartCoroutine(HitFlash(enemy));
            //death check :)
            DeathCheck();


        }
        object sender = "Attack";
        OnPlayerAction?.Invoke(sender, EventArgs.Empty);

    }
    void Defend()
    {
        SwitchButtons(false);
       // object sender = "Defend";
        //enemy dmg / 2;
        OnPlayerAction?.Invoke("Defend", EventArgs.Empty);
        Debug.Log("player used defend. The incoming damage is cut in half");
    }
    void Evade()
    {
        SwitchButtons(false);
        object sender = "Evade";
        //if(rollResult>enemyDamage) { do stuff }
        OnPlayerAction?.Invoke("Evade", EventArgs.Empty);
        Debug.Log("player used evade. If the roll is bigger than enemy damage, they will dodge the attack");
    }
    void Item()
    {
        SwitchButtons(false);
        //no idea rn
        OnPlayerAction?.Invoke(this, EventArgs.Empty);
    }
    private IEnumerator PlayAnimation(string triggerName, Unit unit)
    {
        Animator animator = unit.GetComponent<Animator>();
        animator.SetTrigger(triggerName);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
    }
    void SwitchButtons(bool state)
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].interactable = state;
        }
    }
    public IEnumerator HitFlash(Unit unit)
    {
        SpriteRenderer sprite = unit.GetComponent<SpriteRenderer>();
        Animator animator = unit.GetComponent<Animator>();

        sprite.color = new Color(1f, 0.3f, 0);
        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(255, 255, 255);
        yield return null;
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator HitFlash(Unit unit,Color color)
    {
        SpriteRenderer sprite = unit.GetComponent<SpriteRenderer>();
        Animator animator = unit.GetComponent<Animator>();

        sprite.color = color;
        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(255, 255, 255);
        yield return null;
        yield return new WaitForSeconds(1f);
    }
}
