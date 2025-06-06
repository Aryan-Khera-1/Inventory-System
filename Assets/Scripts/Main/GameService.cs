using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("Services")]
    [SerializeField] private UIService uiService;

    public UIService UIService { get; private set; }
    public EventService EventService { get; private set; }
    public SoundService SoundService { get; private set; }
    public GameplayService GameplayService { get; private set; }

    protected override void Awake()
    {
        base.Awake();   

        // Assign UI Service (MonoBehaviour)
        UIService = uiService;

        // Instantiate Non-Mono services
        EventService = new EventService();
        SoundService = new SoundService();
        GameplayService = new GameplayService(EventService);
        
        UIService.Initialize(EventService, GameplayService);
    }
}
