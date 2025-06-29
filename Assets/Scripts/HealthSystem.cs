using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance;

    public Image currentHealthBar;  // 当前血量条
    public Text healthText;  // 显示血量的文本
    public float hitPoint = 100f;  // 当前血量
    public float maxHitPoint = 100f;  // 最大血量

    public Image currentManaBar;  // 当前法力条
    public Text manaText;  // 显示法力的文本
    public float manaPoint = 100f;  // 当前法力
    public float maxManaPoint = 100f;  // 最大法力

    // 自动恢复血量和法力的设置
    public bool Regenerate = true;  // 是否启用自动恢复
    public float regen = 0.1f;  // 每次恢复的量
    private float timeleft = 0.0f;  // 剩余时间
    public float regenUpdateInterval = 1f;  // 恢复间隔

    public bool GodMode;  // 是否开启上帝模式，开启时会自动满血满法力

    //==============================================================
    // Awake 方法：脚本初始化
    //==============================================================
    void Awake()
    {
        Instance = this;  // 设置单例
    }

    //==============================================================
    // Start 方法：游戏开始时调用
    //==============================================================
    void Start()
    {
        UpdateGraphics();  // 初始化图形界面
        timeleft = regenUpdateInterval;  // 设置恢复时间间隔
    }

    //==============================================================
    // Update 方法：每帧更新
    //==============================================================
    void Update()
    {
        if (Regenerate)
            Regen();  // 自动恢复血量和法力
        UpdateHealthBar();  // 更新血条
        UpdateManaBar();  // 更新法力条
    }

    //==============================================================
    // 自动恢复血量和法力
    //==============================================================
    private void Regen()
    {
        timeleft -= Time.deltaTime;  // 减少时间间隔

        if (timeleft <= 0.0)  // 如果间隔时间结束
        {
            // 如果是上帝模式，血量和法力恢复到最大值
            if (GodMode)
            {
                HealDamage(maxHitPoint);  // 恢复满血
                RestoreMana(maxManaPoint);  // 恢复满法力
            }
            else
            {
                HealDamage(regen);  // 恢复一定量的血量
                RestoreMana(regen);  // 恢复一定量的法力
            }

            UpdateGraphics();  // 更新UI图形

            timeleft = regenUpdateInterval;  // 重置时间
        }
    }

    //==============================================================
    // 更新血量条
    //==============================================================
    private void UpdateHealthBar()
    {
        float ratio = hitPoint / maxHitPoint;  // 计算当前血量占最大血量的比例
        currentHealthBar.rectTransform.localPosition = new Vector3(currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width, 0, 0);  // 更新血条的填充
        healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");  // 更新血量文本
    }

    //==============================================================
    // 玩家受到伤害
    //==============================================================
    public void TakeDamage(float Damage)
    {
        hitPoint -= Damage;  // 减少血量
        if (hitPoint < 1)  // 血量不能小于0
            hitPoint = 0;

        UpdateGraphics();  // 更新图形界面

        //StartCoroutine(PlayerHurts());  // 播放伤害效果
    }

    //==============================================================
    // 恢复血量
    //==============================================================
    public void HealDamage(float Heal)
    {
        hitPoint += Heal;  // 恢复血量
        if (hitPoint > maxHitPoint)  // 血量不能超过最大血量
            hitPoint = maxHitPoint;

        UpdateGraphics();  // 更新图形界面
    }

    //==============================================================
    // 设置最大血量
    //==============================================================
    public void SetMaxHealth(float max)
    {
        maxHitPoint += (int)(maxHitPoint * max / 100);  // 增加最大血量

        UpdateGraphics();  // 更新图形界面
    }

    //==============================================================
    // 更新法力条
    //==============================================================
    private void UpdateManaBar()
    {
        float ratio = manaPoint / maxManaPoint;  // 计算当前法力占最大法力的比例
        currentManaBar.rectTransform.localPosition = new Vector3(-(currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width), 0, 0);  // 更新法力条的填充
        manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");  // 更新法力文本
    }

    //==============================================================
    // 使用法力
    //==============================================================
    public void UseMana(float Mana)
    {
        manaPoint -= Mana;  // 减少法力
        if (manaPoint < 1)  // 法力不能小于0
            manaPoint = 0;

        UpdateGraphics();  // 更新图形界面
    }

    //==============================================================
    // 恢复法力
    //==============================================================
    public void RestoreMana(float Mana)
    {
        manaPoint += Mana;  // 恢复法力
        if (manaPoint > maxManaPoint)  // 法力不能超过最大法力
            manaPoint = maxManaPoint;

        UpdateGraphics();  // 更新图形界面
    }

    //==============================================================
    // 设置最大法力
    //==============================================================
    public void SetMaxMana(float max)
    {
        maxManaPoint += (int)(maxManaPoint * max / 100);  // 增加最大法力

        UpdateGraphics();  // 更新图形界面
    }

    //==============================================================
    // 更新所有UI图形
    //==============================================================
    private void UpdateGraphics()
    {
        UpdateHealthBar();  // 更新血条
        UpdateManaBar();  // 更新法力条
    }

    //==============================================================
    // 玩家受到伤害时的效果
    //==============================================================
    IEnumerator PlayerHurts()
    {
        // 播放伤害动画或音效
        PopupText.Instance.Popup("Ouch!", 1f, 1f);  // 示例弹窗

        if (hitPoint < 1)  // 如果血量为0，玩家死亡
        {
            yield return StartCoroutine(PlayerDied());  // 处理玩家死亡
        }
        else
            yield return null;
    }

    //==============================================================
    // 玩家死亡时的效果
    //==============================================================
    IEnumerator PlayerDied()
    {
        // 玩家死亡，播放动画或音效
        PopupText.Instance.Popup("You have died!", 1f, 1f);  // 示例弹窗

        yield return null;
    }
}
