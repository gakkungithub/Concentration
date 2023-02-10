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

    //Addressablesでロードする基本形
    IEnumerator loadMusic()
    {
        //Addressables.LoadAssetAsync<欲しいアセットの型(GameObject.Sprite.AudioClipなど)>("アセットのパス")
        var handle = Addressables.LoadAssetAsync<AudioClip>("Assets/Sounds/BGM/トランプ・ゲーム.mp3");

        yield return handle;
        if(handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = handle.Result;
            audioSource.Play();
        }
    }
}
