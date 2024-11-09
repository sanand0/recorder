using NAudio.Wave;

class Program
{
    static async Task Main()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var (micPath, speakerPath) = ($"{timestamp}.mic.wav", $"{timestamp}.speaker.wav");
        Console.WriteLine($"Recording to {micPath} and {speakerPath}");

        try
        {
            await RecordAudioAsync(micPath, speakerPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task RecordAudioAsync(string micPath, string speakerPath)
    {
        using var mic = new WaveInEvent();
        using var speaker = new WasapiLoopbackCapture();
        using var micWriter = new WaveFileWriter(micPath, mic.WaveFormat);
        using var speakerWriter = new WaveFileWriter(speakerPath, speaker.WaveFormat);

        // Configure recording handlers
        mic.DataAvailable += (_, e) => micWriter.Write(e.Buffer, 0, e.BytesRecorded);
        ConfigureSpeakerCapture(speaker, speakerWriter);

        // Start recording
        mic.StartRecording();
        speaker.StartRecording();

        Console.WriteLine("Recording... Press any key to stop");
        await Task.Run(Console.ReadKey);

        mic.StopRecording();
        speaker.StopRecording();
        Console.WriteLine($"Recording saved to {micPath} and {speakerPath}");
    }

    static void ConfigureSpeakerCapture(WasapiLoopbackCapture speaker, WaveFileWriter writer)
    {
        var silence = new byte[speaker.WaveFormat.AverageBytesPerSecond / 4];
        new SilenceProvider(speaker.WaveFormat).Read(silence, 0, silence.Length);

        speaker.DataAvailable += (_, e) => writer.Write(
            e.Buffer.All(b => b == 0) ? silence : e.Buffer,
            0,
            e.Buffer.All(b => b == 0) ? silence.Length : e.BytesRecorded
        );
    }
}
