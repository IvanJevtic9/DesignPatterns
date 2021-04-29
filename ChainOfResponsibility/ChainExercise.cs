using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility.Exercise
{
    public abstract class CreatureModifier
    {
        public abstract void Handle(object sender, EmitArgs obj);
    }

    public class GobilinKingModifier : CreatureModifier
    {
        public override void Handle(object sender, EmitArgs obj)
        {
            if (obj.CreatureType == Game.CreatureType.Goblin) obj.ModifiedDefense += 1;
        }
    }
    public class GobilinModifier : CreatureModifier
    {
        public override void Handle(object sender, EmitArgs obj)
        {
            if (obj.CreatureType == Game.CreatureType.GoblinKing) obj.ModifiedAttack += 1;
        }
    }

    public abstract class Creature : IDisposable
    {
        private Game game;
        private int attack;
        private int defense;

        private CreatureModifier modifier;

        public Creature(Game game, CreatureModifier modifier, int defAttack, int defDefense)
        {
            this.game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            this.modifier = modifier ?? throw new ArgumentNullException(paramName: nameof(modifier));
            attack = defAttack;
            defense = defDefense;

            game.EmitEvent += modifier.Handle;
        }

        public int Attack
        {
            get
            {
                var creatureType = this.GetType().Name == nameof(GoblinKing) ? Game.CreatureType.GoblinKing : Game.CreatureType.Goblin;

                var emitCallback = new EmitArgs(creatureType, attack, defense);
                game.EvaluateEvent(this, emitCallback);
                return emitCallback.ModifiedAttack;
            }
        }
        public int Defense
        {
            get
            {
                var creatureType = this.GetType().Name == nameof(GoblinKing) ? Game.CreatureType.GoblinKing : Game.CreatureType.Goblin;

                var emitCallback = new EmitArgs(creatureType, attack, defense);
                game.EvaluateEvent(this, emitCallback);
                return emitCallback.ModifiedDefense;
            }
        }

        public void Dispose()
        {
            game.EmitEvent -= modifier.Handle;
        }

        public override string ToString()
        {
            return $"Creature type: {this.GetType().Name}, {nameof(this.attack)}: {this.Attack} and {nameof(this.defense)}: {this.Defense}";
        }
    }

    public class Goblin : Creature
    {
        protected Goblin(Game game, CreatureModifier modifier, int att, int def) : base(game, modifier, att, def)
        { }
        public Goblin(Game game) : this(game, new GobilinModifier(), 1, 1)
        { }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game, new GobilinKingModifier(), 3, 3)
        { }
    }

    public class EmitArgs
    {
        public EmitArgs(Game.CreatureType creatureType, int att, int def)
        {
            ModifiedAttack = att;
            ModifiedDefense = def;
            CreatureType = creatureType;
        }
        public Game.CreatureType CreatureType { get; set; }
        public int ModifiedAttack { get; set; }
        public int ModifiedDefense { get; set; }
    }

    public class Game
    {
        public enum CreatureType { Goblin, GoblinKing };

        private IList<Creature> creatures;

        public event EventHandler<EmitArgs> EmitEvent;

        public void EvaluateEvent(object sender, EmitArgs obj)
        {
            EmitEvent?.Invoke(sender, obj);
        }

        public Game()
        {
            creatures = new List<Creature>();
        }

        public void AddCreature(CreatureType cr)
        {
            if (cr == CreatureType.Goblin)
            {
                creatures.Add(new Goblin(this));
            }
            else
            {
                creatures.Add(new GoblinKing(this));
            }
        }

        public void RemoveCreature(Creature creature)
        {
            if(creature != null)
            {
                creatures.Remove(creature);
                creature.Dispose();
            }
        }

        public Creature GetCreature(int index)
        {
            try
            {
                return creatures[index];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid index specified.");
                return null;
            }
        }

        public string GetGameCharacters()
        {
            var str = new StringBuilder();

            str.AppendLine("##################################");
            str.AppendLine("         Game characters:         ");
            foreach(var creature in creatures)
            {
                str.AppendLine(creature.ToString());
            }
            str.AppendLine("##################################");

            return str.ToString();
        }
    }

    public static class ChainExercise
    {
        public static void MainFunc(string[] args)
        {
            var game = new Game();

            game.AddCreature(Game.CreatureType.GoblinKing);
            game.AddCreature(Game.CreatureType.GoblinKing);
            game.AddCreature(Game.CreatureType.Goblin);
            game.AddCreature(Game.CreatureType.Goblin);

            Console.WriteLine(game.GetGameCharacters());

            game.AddCreature(Game.CreatureType.Goblin);
            game.AddCreature(Game.CreatureType.Goblin);
            game.AddCreature(Game.CreatureType.Goblin);
            game.AddCreature(Game.CreatureType.Goblin);

            Console.WriteLine(game.GetGameCharacters());

            game.RemoveCreature(game.GetCreature(7));
            game.RemoveCreature(game.GetCreature(1));

            Console.WriteLine(game.GetGameCharacters());
        }
    }
}
