﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public enum SpellsEnum
    {
        DASH,
        SHOOT,
        SHOOTLIGHTNING,
        LIGHTNINGBALL,
        INVISIBILITY,
        BOMB,
        LASER
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
                case SpellsEnum.INVISIBILITY:
                    spell.script = this.GetComponent<Invisibility>();
                    break;
                case SpellsEnum.BOMB:
                    spell.script = this.GetComponent<BombPlace>();
                    break;
                case SpellsEnum.LASER:
                    spell.script = this.GetComponent<Laser>();
                    break;
            }
            spellDict.Add(spell.spell, spell);
        }

        playerSpells = Levels.playerSpells;
    }

    void Update()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKey(i.ToString()))
            {
                if (playerSpells.Count >= i)
                {
                    spellDict[playerSpells[i - 1]].script.doAction();
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            spellDict[SpellsEnum.SHOOT].script.doAction();
        }
        if (Input.GetMouseButton(1))
        {
            spellDict[SpellsEnum.DASH].script.doAction();
        }
        if (Input.GetKey("c"))
        {
            playerSpells = new List<SpellsEnum>();
            foreach (Spell spell in spells)
            {
                if (spell.spell != SpellsEnum.DASH && spell.spell != SpellsEnum.SHOOT)
                {
                    playerSpells.Add(spell.spell);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DroppedSpell droppedSpell = other.gameObject.GetComponent<DroppedSpell>();
        if (droppedSpell)
        {
            SpellsEnum type = droppedSpell.spellType;
            if (!playerSpells.Contains(type))
            {
                playerSpells.Add(type);
                Destroy(other.gameObject);
            }
        }
    }
}
