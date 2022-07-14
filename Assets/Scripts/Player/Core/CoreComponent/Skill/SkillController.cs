using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : CoreComponent
{
    public List<SkillSlot> skillSlotList;
    public float baseCoolDownFactor = 1;

    public float finalCoolDownFactor = 1;

    Player player;
    PlayerInputHandler inputHandler;
    [SerializeField] SkillSlot selectedSkillSlot; //todo set private

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        skillSlotList = new List<SkillSlot>(GetComponentsInChildren<SkillSlot>());
    }

    private void Start() {
        foreach(SkillSlot skillSlot in skillSlotList)
        {
            skillSlot.skill.Initialize(player, player.core);
        }
    }

    private void OnEnable() {
        inputHandler.onSkillInputChange += SelectSkillSlot;
    }

    private void OnDisable() {
        inputHandler.onSkillInputChange -= SelectSkillSlot;
    }

    public void SelectSkillSlot(int index)
    {
        selectedSkillSlot = skillSlotList[index];
    }

    void Update() 
    {
        SkillUpdateTime();
    }

    void SkillUpdateTime()
    {
        if (selectedSkillSlot == null) return;

        if (selectedSkillSlot.skill.isPlaying)
        {
            selectedSkillSlot.skill.LogicsUpdate();
        }
    }

    public void ActivateSkill()
    {
        selectedSkillSlot.skill.Activate();
    }

    public string GetSkillAnimName()
    {
        return selectedSkillSlot.skill.animName;
    }

    public void CooldownAllSkills(float delta) 
    {
        foreach (SkillSlot skillSlot in skillSlotList)
        {
            skillSlot.Cooldown(delta, finalCoolDownFactor);
        }
    }

    public bool IsSkillEmergency()
    {
        return selectedSkillSlot.skill.isEmergency;
    }

    public bool DoesSkillNeedTarget()
    {
        return selectedSkillSlot.skill.needTarget;
    }

    public bool DoesSkillNeedCharging()
    {
        return selectedSkillSlot.skill.needCharging;
    }

    public bool IsSkillFinished()
    {
        return !selectedSkillSlot.skill.isPlaying;
    }

    public void ChargeSkill()
    {
        selectedSkillSlot.skill.Charging();
    }

    public void ResetSkillCharging()
    {
        selectedSkillSlot.skill.ResetSkillCharging();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (selectedSkillSlot == null) return;

        selectedSkillSlot.skill.OnTouchPlayer(other);
    }
}
