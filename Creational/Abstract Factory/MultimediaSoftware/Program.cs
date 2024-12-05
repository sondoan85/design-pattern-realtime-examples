namespace MultimediaSoftware
{
    public interface IAudioProcessor
    {
        void ProcessAudio(string file);
    }

    public interface IVideoProcessor
    {
        void ProcessVideo(string file);
    }

    public class MP3Processor : IAudioProcessor
    {
        public void ProcessAudio(string file)
        {
            Console.WriteLine($"Processing MP3 audio file: {file}");
        }
    }

    public class MP4Processor : IVideoProcessor
    {
        public void ProcessVideo(string file)
        {
            Console.WriteLine($"Processing MP4 video file: {file}");
        }
    }

    public class WAVProcessor : IAudioProcessor
    {
        public void ProcessAudio(string file)
        {
            Console.WriteLine($"Processing WAV audio file: {file}");
        }
    }

    public class AVIProcessor : IVideoProcessor
    {
        public void ProcessVideo(string file)
        {
            Console.WriteLine($"Processing AVI video file: {file}");
        }
    }

    public interface IMediaFactory
    {
        IAudioProcessor CreateAudioProcessor();
        IVideoProcessor CreateVideoProcessor();
    }

    public class MP3MP4Factory : IMediaFactory
    {
        public IAudioProcessor CreateAudioProcessor() => new MP3Processor();

        public IVideoProcessor CreateVideoProcessor() => new MP4Processor();
    }

    public class WAVAVIFactory : IMediaFactory
    {
        public IAudioProcessor CreateAudioProcessor() => new WAVProcessor();

        public IVideoProcessor CreateVideoProcessor() => new AVIProcessor();
    }

    // Client Code
    public class MediaApplication
    {
        private readonly IAudioProcessor _audioProcessor;
        private readonly IVideoProcessor _videoProcessor;

        public MediaApplication(IMediaFactory factory)
        {
            _audioProcessor = factory.CreateAudioProcessor();
            _videoProcessor = factory.CreateVideoProcessor();
        }

        public void Run(string audioFile, string videoFile)
        {
            _audioProcessor.ProcessAudio(audioFile);
            _videoProcessor.ProcessVideo(videoFile);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Using MP3 & MP4 formats:");
            var mp3mp4Factory = new MP3MP4Factory();
            var mp3mp4App = new MediaApplication(mp3mp4Factory);
            mp3mp4App.Run("song.mp3", "video.mp4");

            Console.WriteLine("\nUsing WAV & AVI formats:");
            var wavaviFactory = new WAVAVIFactory();
            var wavaviApp = new MediaApplication(wavaviFactory);
            wavaviApp.Run("song.wav", "video.avi");

            Console.ReadKey();
        }
    }
}
