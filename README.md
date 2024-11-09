# Recorder

A .NET command-line tool that simultaneously records audio from both microphone input and system audio output (speaker).

## Features

- Concurrent recording from microphone and system audio
- Automatic silence detection and handling for speaker output
- Timestamped WAV file output
- Simple command-line interface

## Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Windows OS (required for WASAPI loopback capture)
- Audio input device (microphone)

## Installation

1. Clone this repository:
   ``git clone https://github.com/yourusername/recorder.git``

2. Navigate to the project directory:
   ``cd recorder``

3. Build the project:
   ``dotnet build``

## Usage

Run the application:
``dotnet run``

The program will:
1. Start recording from both microphone and system audio
2. Create two WAV files with timestamps (e.g., `20240315_143022.mic.wav` and `20240315_143022.speaker.wav`)
3. Continue recording until any key is pressed
4. Save both recordings automatically

## Output Files

- `{timestamp}.mic.wav` - Recording from microphone input
- `{timestamp}.speaker.wav` - Recording from system audio output

## Dependencies

- [NAudio (2.2.1)](https://github.com/naudio/NAudio) - .NET audio library

## Technical Notes

- Uses WASAPI loopback capture for system audio recording
- Implements silence detection and handling for speaker output
- Utilizes async/await pattern for non-blocking I/O operations

## License

[MIT](LICENSE)
