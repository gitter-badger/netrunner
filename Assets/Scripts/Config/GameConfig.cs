﻿using UnityEngine;
using model;
using view.gui;
using model.ai;
using view.log;
using model.player;

public class GameConfig : MonoBehaviour
{
    public GameObject board;
    private Game game;

    void Awake()
    {
        var corpPlayer = new Player(
            deck: new Decks().DemoCorp(),
            pilot: new CorpAi(new System.Random(1234))
        );
        var runnerPlayer = new Player(
            deck: new Decks().DemoRunner(),
            pilot: new NoPilot()
        );
        game = new Game(corpPlayer, runnerPlayer);
    }

    void Start()
    {
        var flowView = new GameFlowView();
        var flowLog = new GameFlowLog();
        flowView.Display(board, game);
        flowLog.Display(game);
        var corpView = new CorpViewConfig().Display(game);
        new RunnerViewConfig().Display(game, corpView);
        game.Start();
    }
}
