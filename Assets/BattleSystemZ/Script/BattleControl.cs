using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    [SerializeField]
    PlayerController player01ConScript;
    [SerializeField]
    PlayerController player02ConScript;
    void Start()
    {

    }

    void Update()
    {

    }
    public static IEnumerator TimeControl(float time_speed, float duration)
    {
        Time.timeScale = time_speed;
        yield return new WaitForSecondsRealtime(duration);
        //duration = 10;
        //for(int times = 0;times<duration;times++)
        //{
        //    yield return null;
        //}
        Time.timeScale = 1;
    }
    public static IEnumerator CheckAttackEffect(PlayerController playerConScript, GameObject effect_obj)
    {
        yield return new WaitUntil(() => playerConScript.isHitting);
        Instantiate(effect_obj, playerConScript.attack_body_part.transform);
    }
    public static  void InstantiateEffect(GameObject target_obj, GameObject effetc_prefab)
    {
        GameObject effects_obj = Instantiate(effetc_prefab, target_obj.transform);
    }
}