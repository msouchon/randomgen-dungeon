using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsUI : MonoBehaviour
{
    public GameObject player;
    public Spells spellsScript;

    private List<Spells.SpellsEnum> playerSpells = new List<Spells.SpellsEnum>();

    void Start()
    {
        spellsScript = player.GetComponent<Spells>();
    }

    void Update()
    {
        if (!checkLists(playerSpells, spellsScript.playerSpells))
        {
            playerSpells = new List<Spells.SpellsEnum>(spellsScript.playerSpells);

            foreach (Transform child in transform.parent.transform)
            {
                if (child != this.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            for (int i = 0; i < playerSpells.Count; i++)
            {
                Spells.Spell spell = spellsScript.spellDict[playerSpells[i]];
                Vector3 position = new Vector3(60 * i, -1, 0) - new Vector3(120, 0, 0);
                GameObject obj = Instantiate(spell.icon);
                obj.transform.SetParent(this.transform);
                obj.transform.localPosition = position;
                obj.transform.SetParent(this.transform.parent);
            }
            this.transform.SetSiblingIndex(10);
        }
    }

    bool checkLists(List<Spells.SpellsEnum> l1, List<Spells.SpellsEnum> l2)
    {
        if (l1.Count != l2.Count)
        {
            return false;
        }

        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
            {
                return false;
            }
        }

        return true;
    }
}
