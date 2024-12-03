using System.Xml.Linq;

namespace SocialMediaNotificationSystem
{
    public class Program
    {
        public interface IFollower
        {
            void Update(User user, string message);
        }

        public interface IUser
        {
            void Follow(IFollower follower);
            void Unfollow(IFollower follower);
            void NotifyFollowers(string message);
        }

        public class Follower : IFollower
        {
            public string Name { get; private set; }

            public Follower(string name)
            {
                Name = name;
            }

            public void Update(User user, string message)
            {
                Console.WriteLine($"{Name} received a new post from {user.Name}: {message}");
            }
        }

        public class User : IUser
        {
            public string Name { get; private set; }
            private List<IFollower> _followers = new List<IFollower>();
            public User(string name)
            {
                Name = name;
            }

            public void Follow(IFollower follower)
            {
                _followers.Add(follower);
            }

            public void Unfollow(IFollower follower)
            {
                _followers.Remove(follower);
            }

            public void PostMessage(string message)
            {
                Console.WriteLine($"{Name} posted: {message}");
                NotifyFollowers(message);
            }

            public void NotifyFollowers(string message)
            {
                foreach (var follower in _followers)
                {
                    follower.Update(this, message);
                }
            }
        }

        public static void Main(string[] args)
        {
            User Pranaya = new User("Pranaya");
            User Rout = new User("Rout");

            Follower Anurag = new Follower("Anurag");
            Follower Mohanty = new Follower("Mohanty");

            Pranaya.Follow(Anurag); // Anurag follows Pranaya
            Rout.Follow(Anurag);   // Anurag follows Rout
            Pranaya.Follow(Mohanty);   // Mohanty follows Pranaya

            Pranaya.PostMessage("Hello from Pranaya!");
            Rout.PostMessage("Rout's first post.");

            Console.ReadKey();
        }
    }
}
