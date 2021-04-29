using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ChainOfResponsibility.Br
{
    public class Query
    {
        public enum Argument { Attack, Defense }

        public string CreatureName;
        public Argument ArgumentQuery;

        public int Value { get; set; }

        public Query(string creatureName, Argument argumentQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(paramName: nameof(creatureName));
            ArgumentQuery = argumentQuery;
            Value = value;
        }
    }

    public class Game
    {
        public event EventHandler<Query> Queries;
        public void EvaluateQuery(object sender, Query query)
        {
            Queries?.Invoke(sender, query);
        }
    }

    public class Creature
    {
        private Game game;
        private int attack;
        private int defense;

        public string Name { get; set; }

        public Creature(Game game, string name, int att, int def)
        {
            this.game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            attack = att;
            defense = def;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.EvaluateQuery(this, q);
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.EvaluateQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(attack)}: {Attack}, {nameof(defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game game;
        protected Creature creature;

        public CreatureModifier(Game game, Creature creature)
        {
            this.game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
            game.Queries += Handle;
        }
        protected abstract void Handle(object sender, Query q);
        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {}

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name && q.ArgumentQuery == Query.Argument.Attack) q.Value *= 2;
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {}
        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name && q.ArgumentQuery == Query.Argument.Defense) q.Value += 3;
        }
    }

    public static class BrokerChain
    {
        public static void MainFunc(string[] args)
        {
            var gameManager = new Game();

            var dragonCreature = new Creature(gameManager, "Red Dragon", 20, 11);

            Console.WriteLine(dragonCreature.ToString());
            using (var buffAtt = new DoubleAttackModifier(gameManager, dragonCreature))
            {
                Console.WriteLine(dragonCreature.ToString());

                var buffDef = new IncreaseDefenseModifier(gameManager, dragonCreature);
                
                Console.WriteLine(dragonCreature.ToString());

                buffDef.Dispose();
            }
            Console.WriteLine(dragonCreature.ToString());
        }
    }
}
