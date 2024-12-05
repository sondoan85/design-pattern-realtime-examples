namespace GameSaveMechanism
{
    //GameMemento - Captures the current state of the game.
    public class GameMemento
    {
        public int Level { get; }

        public int Points { get; }

        public List<string> PowerUps { get; }

        public GameMemento(int level, int points, List<string> powerUps)
        {
            Level = level;
            Points = points;
            PowerUps = new List<string>(powerUps);
        }
    }

    //GameSession - Represents the current game state.
    public class GameSession
    {
        public int Level { get; set; }

        public int Points { get; set; }

        public List<string> PowerUps { get; } = new List<string>();

        public GameMemento Save()
        {
            return new GameMemento(Level, Points, PowerUps);
        }

        public void Restore(GameMemento memento)
        {
            Level = memento.Level;
            Points = memento.Points;
            PowerUps.Clear();
            PowerUps.AddRange(memento.PowerUps);
        }
    }

    //GameHistory - Keeps track of saved game states.
    public class GameHistory
    {
        private Stack<GameMemento> _savedStates = new Stack<GameMemento>();

        public void SaveState(GameSession game)
        {
            _savedStates.Push(game.Save());
        }

        public GameMemento LoadLastSavedState()
        {
            return _savedStates.Pop();
        }
    }

    // Testing the Memento Design Pattern
    // Client Code
    public class Program
    {
        public static void Main()
        {
            GameSession game = new GameSession();
            GameHistory gameHistory = new GameHistory();
            game.Level = 5;
            game.Points = 3000;
            game.PowerUps.Add("Shield");
            game.PowerUps.Add("Speed boost");

            // Player saves the game state
            gameHistory.SaveState(game);

            // Player progresses but then finds themselves in trouble
            game.Level = 6;
            game.Points = 3200;
            game.PowerUps.Remove("Shield");

            Console.WriteLine($"Current Game State: Level={game.Level}, Points={game.Points}, PowerUps={string.Join(", ", game.PowerUps)}");
            // Outputs: Current Game State: Level=6, Points=3200, PowerUps=Speed boost
            
            // Player decides to load the previously saved state
            game.Restore(gameHistory.LoadLastSavedState());

            Console.WriteLine($"Restored Game State: Level={game.Level}, Points={game.Points}, PowerUps={string.Join(", ", game.PowerUps)}");
            // Outputs: Restored Game State: Level=5, Points=3000, PowerUps=Shield, Speed boost

            Console.ReadKey();
        }
    }
}
