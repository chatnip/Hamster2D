using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;


public class DialogManager : MonoBehaviour
{
    [Header("*Dialog")]
    [SerializeField] GameObject dialog;
    [SerializeField] DialogData DialogData;
    [SerializeField] public bool isScripting;

    private void DialogSetup(Fragment Scenario_Fragment)
    {
        // 다이얼로그 켜기
        dialog.SetActive(true);
        DialogData.nameText.text = Scenario_Fragment.Speaker;
        isScripting = true;
    }

    public IEnumerator DialogTexting(ScenarioBase scenarioBase)
    {
        DialogData.dialogText.text = null;
        DialogSetup(scenarioBase.Fragments[0]);

        for (int i = 0; i < scenarioBase.Fragments.Count; i++)
        {
            int temp = i;
            var sequence = DOTween.Sequence();
            DialogData.dialogText.text = null;
            DialogSetup(scenarioBase.Fragments[temp]);
            Fragment newFragment = scenarioBase.Fragments[temp];

            sequence.Append(DialogData.dialogText.DOText(newFragment.Script, newFragment.Script.Length / 10)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            sequence.Complete();
                        }
                    }));

            yield return new WaitUntil(() =>
            {
                if (DialogData.dialogText.text == newFragment.Script)
                {
                    return true;
                }
                return false;
            });

            yield return new WaitForSeconds(0.2f);

            yield return new WaitUntil(() =>
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    return true;
                }
                return false;
            });
            continue;
        }
        dialog.SetActive(false);
        isScripting = false;
    }
}

[System.Serializable]
public struct DialogData
{
    public Text nameText;
    public Text dialogText;
}