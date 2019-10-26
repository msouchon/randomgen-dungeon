using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public enum SpellsEnum
    {
        DASH,
        SHOOT,
        SHOOTLIGHTNING,
	LIGHTNINGBALL
    }

    [System.Serializable]
    public struct Spell
    {
        public SpellsEnum spell;
        public GameObject icon;
        public ISpell script;
        public GameObject drop;
    }

    public List<Spell> spells;
    public Dictionary<SpellsEnum, Spell> spellDict = new Dictionary<SpellsEnum, Spell>();

    public List<SpellsEnum> playerSpells;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spells.Count; i++)
        {
            Spell spell = spells[i];
            switch (spell.spell)
            {
                case SpellsEnum.SHOOT:
                    spell.script = this.GetComponentInChildren<Shoot>();
                    break;
                case SpellsEnum.SHOOTLIGHTNING:
                    spell.script = this.GetComponentInChildren<ShootLightning>();
                    break;
                case SpellsEnum.DASH:
                    spell.script = this.GetComponent<Dash>();
                    break;
		case SpellsEnum.LIGHTNINGBALL:
		    spell.script = this.GetComponent<lightningBallSummon>();
		    break;
            }
            spellDict.Add(spell.spell, spell);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                if (playerSpells.Count >= i)
                {
                    spellDict[playerSpells[i - 1]].script.doAction();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        DroppedSpell droppedSpell = other.gameObject.GetComponent<DroppedSpell>();
        if(droppedSpell) {
            SpellsEnum type = droppedSpell.spellType;
            if(!playerSpells.Contains(type)) {
                playerSpells.Add(type);
                Destroy(other.gameObject);
            }
        }
    }
}
