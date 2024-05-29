using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject hero;
    private GameObject enemy;
    private GameController gameController;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject healPrefab;

    void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameController = FindObjectOfType<GameController>();
    }

    public void SelectAttack(string btn)
    {
        if (btn == "melee")
        {
            meleePrefab.GetComponent<ActionScript>().Attack(enemy);
            meleePrefab.GetComponent<ActionScript>().Attack(hero);
        }
        else if (btn == "heal")
        {
            healPrefab.GetComponent<ActionScript>().Heal(hero);
            healPrefab.GetComponent<ActionScript>().Heal(enemy);
        }
        // Notify GameController that the hero's turn has ended
        gameController.EndHeroTurn();
    }
}
