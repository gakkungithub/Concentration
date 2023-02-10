using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadMusic());
    }

    //Addressables�Ń��[�h�����{�`
    IEnumerator loadMusic()
    {
        //Addressables.LoadAssetAsync<�~�����A�Z�b�g�̌^(GameObject.Sprite.AudioClip�Ȃ�)>("�A�Z�b�g�̃p�X")
        var handle = Addressables.LoadAssetAsync<AudioClip>("Assets/Sounds/BGM/�g�����v�E�Q�[��.mp3");

        yield return handle;
        if(handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = handle.Result;
            audioSource.Play();
        }
    }
}
