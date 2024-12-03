using System.Linq;

namespace SmartHomeSystem
{
    // Command Interface
    public interface ICommand
    {
        void Execute();
        void NotExecute();
    }

    //Receivers
    //Receiver - Light
    public class Light
    {
        public void TurnOn()
        {
            Console.WriteLine("Light turned ON");
        }
        public void TurnOff()
        {
            Console.WriteLine("Light turned OFF");
        }

        public void CallAlready()
        {
            Console.WriteLine("Light call already");
        }
    }
    //Receiver - Fan
    public class Fan
    {
        public void Start()
        {
            Console.WriteLine("Fan started");
        }
        public void Stop()
        {
            Console.WriteLine("Fan stopped");
        }
        public void CallAlready()
        {
            Console.WriteLine("Fan call already");
        }
    }

    //Concrete Commands
    public class LightOnCommand : ICommand
    {
        private Light _light;
        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOn();
        }

        public void NotExecute()
        {
            _light.CallAlready();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;
        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOff();
        }

        public void NotExecute()
        {
            _light.CallAlready();
        }
    }

    public class FanStartCommand : ICommand
    {
        private Fan _fan;
        public FanStartCommand(Fan fan)
        {
            _fan = fan;
        }

        public void Execute()
        {
            _fan.Start();
        }

        public void NotExecute()
        {
            _fan.CallAlready();
        }
    }

    public class FanStopCommand : ICommand
    {
        private Fan _fan;
        public FanStopCommand(Fan fan)
        {
            _fan = fan;
        }

        public void Execute()
        {
            _fan.Stop();
        }

        public void NotExecute()
        {
            _fan.CallAlready();
        }
    }

    //Invoker - Voice Assistant
    public class VoiceAssistant
    {
        private ICommand _command;
        private List<ICommand> _listCommand = new List<ICommand>();

        public void SetCommand(ICommand command)
        {
            _command = command;
            _listCommand.Add(command);
        }

        public void HearVoiceCommand(ICommand oldCommand)
        {
            if (_listCommand.Count(c => c == oldCommand) is 1)
            {
                _command.Execute();
            }
            else 
            {
                _command.NotExecute();
            }
            
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Light livingRoomLight = new Light();
            Fan bedroomFan = new Fan();
            VoiceAssistant assistant = new VoiceAssistant();

            ICommand turnLightOn = new LightOnCommand(livingRoomLight);
            ICommand turnLightOff = new LightOffCommand(livingRoomLight);
            ICommand startFan = new FanStartCommand(bedroomFan);
            ICommand stopFan = new FanStopCommand(bedroomFan);

            // User gives a voice command to turn on the light
            assistant.SetCommand(turnLightOn);
            assistant.HearVoiceCommand(turnLightOn);

            // User gives a voice command to start the fan
            assistant.SetCommand(startFan);
            assistant.HearVoiceCommand(startFan);

            // User gives a voice command to turn off the light
            assistant.SetCommand(turnLightOff);
            assistant.HearVoiceCommand(turnLightOff);

            // User gives a voice command to start the fan
            assistant.SetCommand(startFan);
            assistant.HearVoiceCommand(startFan);

            // User gives a voice command to turn on the light
            assistant.SetCommand(turnLightOn);
            assistant.HearVoiceCommand(turnLightOn);

            Console.ReadKey();
        }
    }
}
