using System.Collections;
using UnityEngine;

public class HolyLight : Skill
{
    [SerializeField, Range(10, 200)] private int extendSpeed = 1; // Tốc độ kéo dài
    [SerializeField, Range(10, 100)] private int maxSize = 10;
    [SerializeField, Range(1f, 100f)] private float maxManaNeed;
    [SerializeField, Range(0f, 10f)] private float timeHoldSkill;
    public Transform auraCircle;

    private int currentSize = 0;
    Vector3 position, scale;

    private bool inSkill;
    private float timeToExhaust, elapsedTime, decreaseInterval, staminaDecreaseRate;
    private int x=0,y=0,z=0;

    public override void HoldKeySkill()
    {
        //Debug.Log("Mana: "+Player.Instance.CurrentInfo.mana);



        if (cancelSkill)
        {
            Settings.isCatingSkill = false;
            inSkill = false;
            Debug.Log("cancel");
            Player.Instance.spriteRendererPlayer.color = Color.white;
            Player.Instance.UseMana(-manaNeed);
            manaNeed = 0;

            this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
            auraCircle.gameObject.SetActive(false);
            return;
        }

        if (!canUseSkill)
            return;

        if (Settings.isCatingSkill && !inSkill )
            return;

        if (base.canUseSkill && base.Unlocked && Player.Instance.CurrentInfo.mana>0)
        {

            Vector3 playerPosition = Player.Instance.transform.position;
            if (!Settings.isCatingSkill && canUseSkill)
            {
                manaNeed = 0f;
                elapsedTime = 0f;
                decreaseInterval = timeHoldSkill / 100f;
                timeToExhaust = timeHoldSkill;

                //Debug.Log("timeToExhaust: " + timeToExhaust);

                cancelSkill = false;
                position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

                auraCircle.gameObject.SetActive(true);

                auraCircle.position = position;
                transform.localScale = new Vector2(0, transform.localScale.y);
                //transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().enabled = true;
                auraCircle.localScale = new Vector2(0, 0);
                Player.Instance.spriteRendererPlayer.color = Color.yellow;

            }
            Settings.isCatingSkill = true;
            inSkill = true;

            if (manaNeed >= maxManaNeed)
                return;

            //manaNeed++;
            if (timeToExhaust > 0)//&& manaNeed<= maxManaNeed
            {
                elapsedTime += Time.deltaTime;
                timeToExhaust -= Time.deltaTime;

                if (elapsedTime > decreaseInterval)
                {
                    elapsedTime = 0f;
                    //x++;
                    if (timeToExhaust > (timeHoldSkill * (2f / 3f)) && timeToExhaust < timeHoldSkill)
                    {
                        // Trong 1/3 thời gian đầu, stamina mặc định sẽ trừ bằng 20% stamina max
                        staminaDecreaseRate = ((maxManaNeed * (1f / 5f)) * (1f / 33.3f));

                        Player.Instance.UseMana(staminaDecreaseRate);

                        Player.Instance.DamageAttack += 0.1f;

                        //Debug.Log("staminaDecreaseRate: "+ staminaDecreaseRate);
                        x++;
                        //Debug.Log("x: "+x);
                    }
                    else if (timeToExhaust > (timeHoldSkill * (1f / 3f)) && timeToExhaust < (timeHoldSkill * (2f / 3f)))
                    {
                        // Trong khoảng 1/3 - 2/3 thời gian, stamina mặc định sẽ trừ bằng 35% stamina max
                        staminaDecreaseRate = ((maxManaNeed * (35f / 100f)) * (1f / 33.3f));

                        Player.Instance.UseMana(staminaDecreaseRate);
                        Player.Instance.DamageAttack += 0.25f;
                        //Debug.Log("staminaDecreaseRate: " + staminaDecreaseRate);
                        y++;
                        //Debug.Log("y: " + y);
                    }
                    else if (timeToExhaust < (timeHoldSkill * (1f / 3f)))
                    {
                        // Trong khoảng <1/3 thời gian, stamina mặc định sẽ trừ bằng 35% stamina max
                        staminaDecreaseRate = ((maxManaNeed * (45f / 100f)) * (1f / 33.3f));

                        Player.Instance.UseMana(staminaDecreaseRate);

                        Player.Instance.DamageAttack += 0.45f;
                        //Debug.Log("staminaDecreaseRate: " + staminaDecreaseRate);
                        z++;
                        //Debug.Log("z: " + z);
                    }
                    manaNeed += staminaDecreaseRate;

                    

                    Player.Instance.UpdatePlayerUI();

                    //Debug.Log("Toi o day");

                    transform.localScale = new Vector2(transform.localScale.x+0.05f, transform.localScale.y);


                    auraCircle.localScale = new Vector2(transform.localScale.x*5, transform.localScale.x * 5);

                    //GameObject limitSkill = Player.Instance.gameObject.transform.Find("HolyLight").gameObject;
                }
            }
            else
            {
                auraCircle.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    public override void ActivateSkill()
    {
        if (!cancelSkill )
        {
            if (base.canUseSkill && base.Unlocked)
            {
                //Debug.Log("Push1");
                Vector3 playerPosition = Player.Instance.transform.position;
                position = new Vector3(playerPosition.x, playerPosition.y + 60, playerPosition.z);
                transform.position = position;

                //scale = transform.localScale;

                gameObject.SetActive(true);

                auraCircle.gameObject.SetActive(false);
                //transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().enabled = false;

                this.gameObject.SetActive(true);
                //Debug.Log("Push2");
                StartCoroutine(HolyLightStart());
            }
        }

       //GameController.Instance.Player.UseMana(base.manaNeed);
    }

    public override void ActivateSkill(int amount, float scale)
    {
        return;
    }

    private IEnumerator HolyLightStart()
    {
        // niệm phép 
        Settings.isAttacking = true;
        //base.isCastingSkill = true;
        Player.Instance.spriteRendererPlayer.color = Color.grey;
        yield return new WaitForSeconds(base.timeCastSkill);
        // Bắt đầu cast phép
        base.canUseSkill = false;

        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Player.Instance.spriteRendererPlayer.color = Color.red;

        while (maxSize > currentSize)
        {
            float newScaleY = transform.localScale.y + Time.deltaTime * extendSpeed;
            float deltaScaleY = newScaleY - transform.localScale.y;
            float newPosY = transform.position.y - deltaScaleY * 0.5f; // Dịch chuyển vị trí theo chiều y để giữ cho object ở vị trí trung tâm

            transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            currentSize++;
            yield return null; // Chờ một frame
        }
        Settings.isAttacking = false;
        Settings.isCatingSkill = false;

        Debug.Log("Player.Instance.DamageAttack: " + Player.Instance.DamageAttack);

        inSkill = true;
        yield return new WaitForSeconds(base.timeLifeSkill); // vòng đời hào quang ánh sáng 

        Player.Instance.spriteRendererPlayer.color = Settings.playerColor;

        // Hoàn thành phép được tự do di chuyển
        transform.localPosition = position;
        transform.localScale = Vector3.zero;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.transform.Find("HolyLighAura").gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // Thời gian hồi phép 
        yield return new WaitForSeconds(base.skillCoolDown);
        auraCircle.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        currentSize = 0;
        base.canUseSkill = true;
        this.gameObject.SetActive(false);
    }

    public float GetMaxManaNeed()
    {
        return maxManaNeed;
    }
}

