using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class ConcentrationStartScene : MonoBehaviour
{
    [SerializeField]
    Button OnePlayerStartButton;
    [SerializeField]
    Button TwoPlayerStartButton;

    private bool resoucesLoadComplete = false;
    void Start()
    {
        GameSoundManager.Instance.Initialize();
        OnePlayerStartButton.onClick.AddListener(() =>
        {
            if (!resoucesLoadComplete)
            {
                return;
            }
            GameSceneUtil.Instance.SingleSceneTransration(ConcentrationGameStringResource.CONCENTRATION_GAME_MAIN_SCENE,
                () => OnePlayerStartButtonAction());

        });

        TwoPlayerStartButton.onClick.AddListener(() =>
        {
            if (!resoucesLoadComplete)
            {
                return;
            }
            GameSceneUtil.Instance.SingleSceneTransration(ConcentrationGameStringResource.CONCENTRATION_GAME_MAIN_SCENE);

        });

        // BGM�Ȃǂ̃T�E���h�t�@�C�������[�h����
        StartCoroutine(soundResourcesLoad());
        // �G�t�F�N�g�����[�h����
        //StartCoroutine(effectResourcesLoad());

        StartCoroutine(startBGM());
    }

    IEnumerator soundResourcesLoad()
    {
        //����́A���y��炷���߂̋@�\����������ɍ���Ă��銴���B����͎Q�Ƃ��Ȃ��āAgarbage collector�Ɉ���������Ȃ��悤�ɂȂ��Ă���A����ɏ�������邱�Ƃ��Ȃ��B
        var bgmLoadhandle = Addressables.LoadAssetsAsync<AudioClip>("BGM", null);
        yield return bgmLoadhandle;�@//����yield return �͑S�Ă̌��ʂ�Ԃ�����B(�r���ŏI������肵�Ȃ����������B)
        for (int i = 0; i < bgmLoadhandle.Result.Count; i++)
        {
            GameSoundManager.Instance.SetBGMAudioClips(bgmLoadhandle.Result[i]);
        }
        //�������A�܂�A�������ɉ������Ȃ���΃������ォ������Ă���Ȃ��̂ŁA���������������������B���Ȃ킿�A��������B
        Addressables.Release(bgmLoadhandle);

        var seLoadhandle = Addressables.LoadAssetsAsync<AudioClip>("SE", null);
        yield return seLoadhandle;
        for (int i = 0; i < seLoadhandle.Result.Count; i++)
        {
            GameSoundManager.Instance.SetSEAudioClips(seLoadhandle.Result[i]);
        }
        Addressables.Release(seLoadhandle);
    }

    IEnumerator effectResourcesLoad()
    {
        var effectLoadhandle = Addressables.LoadAssetsAsync<GameObject>("GameMainFX", null);
        yield return effectLoadhandle;
        for (int i = 0; i < effectLoadhandle.Result.Count; i++)
        {
            GameVisualEffectManager.Instance.SetGameMainResultParticleSystems(effectLoadhandle.Result[i]);
        }
        Addressables.Release(effectLoadhandle);

        resoucesLoadComplete = true;
    }

    IEnumerator startBGM()
    {
        Debug.Log("BGM���X�^�[�g���܂�");
        yield return new WaitUntil(() => resoucesLoadComplete);

        GameSoundManager.Instance.PlayBGM(GameSoundManager.BGMTypes.GameStart);
    }
    /// <summary>
    /// �Ăяo�����ConcentrationGameProgressionManager��CPUCard��Computer���I�����ĂƓ`����
    /// </summary>
    public void OnePlayerStartButtonAction()
    {
        var gameProgressionManagerGameObject = GameSceneUtil.Instance.
            NextSceneRootGetGameObjects.Where(x => x.GetComponentInChildren<ConcentrationGameProgressionManager>()).FirstOrDefault();
        if (gameProgressionManagerGameObject != null)
        {

            var gameProgressionManager = gameProgressionManagerGameObject.GetComponent<ConcentrationGameProgressionManager>();
            gameProgressionManager.GameMode = ConcentrationGameProgressionManager.GameModes.CPUCardIsComputersChoice;
        }

    }
}
