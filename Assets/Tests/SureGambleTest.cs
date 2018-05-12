﻿using NUnit.Framework;
using model;
using view;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using System.Linq;

public class SureGambleTest
{
    [Test]
    public void ShouldPlay()
    {
        var cards = new List<ICard>();
        for (int i = 0; i < 5; i++)
        {
            cards.Add(new SureGamble());
        }
        var sureGamble = cards.First();
        var deck = new Deck(cards);
        var game = new Game(deck);
        game.Start();
        var balance = new LastBalanceObserver();
        var clicks = new SpentClicksObserver();
        var grip = new GripObserver();
        var heap = new HeapObserver();
        game.runner.credits.Observe(balance);
        game.runner.clicks.Observe(clicks);
        game.runner.grip.ObserveRemovals(grip);
        game.runner.heap.Observe(heap);
        var play = game.runner.actionCard.Play(sureGamble);

        play.Trigger(game);

        Assert.AreEqual(9, balance.LastBalance);
        Assert.AreEqual(1, clicks.Spent);
        Assert.AreEqual(sureGamble, grip.LastRemoved);
        Assert.AreEqual(sureGamble, heap.LastAdded);
    }

    private class LastBalanceObserver : IBalanceObserver
    {
        public int LastBalance { get; private set; }

        void IBalanceObserver.NotifyBalance(int balance)
        {
            LastBalance = balance;
        }
    }

    private class SpentClicksObserver : IClickObserver
    {
        public int Spent { get; private set; }

        void IClickObserver.NotifyClicks(int spent, int unspent)
        {
            Spent = spent;
        }
    }

    private class GripObserver : IGripRemovalObserver
    {
        public ICard LastRemoved { get; private set; }

        void IGripRemovalObserver.NotifyCardRemoved(ICard card)
        {
            LastRemoved = card;
        }
    }

    private class HeapObserver : IHeapObserver
    {
        public ICard LastAdded { get; private set; }

        void IHeapObserver.NotifyCardAdded(ICard card)
        {
            LastAdded = card;
        }
    }
}