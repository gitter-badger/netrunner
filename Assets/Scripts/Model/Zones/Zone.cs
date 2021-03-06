﻿using System.Collections.Generic;
using model.cards;

namespace model.zones
{
    public class Zone
    {
        public readonly string Name;
        public List<Card> Cards = new List<Card>();
        public int Count => Cards.Count;
        private HashSet<IZoneAdditionObserver> additions = new HashSet<IZoneAdditionObserver>();
        private HashSet<IZoneRemovalObserver> removals = new HashSet<IZoneRemovalObserver>();
        private HashSet<IZoneCountObserver> counts = new HashSet<IZoneCountObserver>();
        private HashSet<ICardsObserver> pools = new HashSet<ICardsObserver>();

        public Zone(string name)
        {
            this.Name = name;
        }

        public void Add(Card card)
        {
            Cards.Add(card);
            NotifyChanges();
            foreach (var observer in additions)
            {
                observer.NotifyCardAdded(card);
            }
        }

        public void Remove(Card card)
        {
            if (Cards.Contains(card))
            {
                Cards.Remove(card);
                NotifyChanges();
                foreach (var observer in removals)
                {
                    observer.NotifyCardRemoved(card);
                }
            }
            else
            {
                throw new System.Exception("Trying to remove a " + card.Name + " from " + Name + " but it's not in there");
            }
        }

        private void NotifyChanges()
        {
            NotifyCounts();
            NotifyCards();
        }

        private void NotifyCounts()
        {
            foreach (var observer in counts)
            {
                observer.NotifyCount(Cards.Count);
            }
        }

        private void NotifyCards()
        {
            foreach (var observer in pools)
            {
                observer.NotifyCards(Cards);
            }
        }

        public void ObserveCards(ICardsObserver observer)
        {
            pools.Add(observer);
            NotifyCards();
        }

        public void ObserveAdditions(IZoneAdditionObserver observer)
        {
            additions.Add(observer);
        }

        public void UnobserveAdditions(IZoneAdditionObserver observer)
        {
            additions.Remove(observer);
        }

        public void ObserveRemovals(IZoneRemovalObserver observer)
        {
            removals.Add(observer);
        }

        public void UnobserveRemovals(IZoneRemovalObserver observer)
        {
            removals.Remove(observer);
        }

        public void ObserveCount(IZoneCountObserver observer)
        {
            counts.Add(observer);
            NotifyCounts();
        }
    }

    public interface IZoneCountObserver
    {
        void NotifyCount(int count);
    }

    public interface IZoneAdditionObserver
    {
        void NotifyCardAdded(Card card);
    }

    public interface IZoneRemovalObserver
    {
        void NotifyCardRemoved(Card card);
    }

    public interface ICardsObserver
    {
        void NotifyCards(List<Card> cards);
    }
}