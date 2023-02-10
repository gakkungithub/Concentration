using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVisualEffectManager : SingletonMonoBehaviour<GameVisualEffectManager>
{

    private List<GameObject> GameMainResultParticleSystems = new List<GameObject>();

    public enum GameMainResult
    {
        Invalide = -1,
        GameWin,
        GameLose
    }

    public void SetGameMainResultParticleSystems(GameObject particleSystem)
    {
        GameMainResultParticleSystems.Add(particleSystem);
    }

    public void GameResultParticlePlay(GameMainResult gameMainResult)
    {
        Instantiate(GameMainResultParticleSystems[(int)gameMainResult]);
    }
}
