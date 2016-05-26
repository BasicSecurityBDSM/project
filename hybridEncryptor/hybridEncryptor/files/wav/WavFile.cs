using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    class WavFile : File
    {
        Chuncks.WaveHeader header;
        Chuncks.WaveFormatChunk format;
        Chuncks.WaveDataChunk data;
        public enum WavType
        {
            SineWave = 0,
            BlockWave = 1,
            RandomWave = 2
        }
        public override void Generate(byte[] dataToInsert)
        {
            Generate(dataToInsert, WavType.SineWave);
        }
        public void Generate(byte[] dataToInsert, WavType type)
        {
            //split the bytes into bits
            byte[] bitsToInsert = new byte[(dataToInsert.Length) * 8];
            for (int i = 0; i < dataToInsert.Length; i += 1)
            {
                for (int j = 0; j < 8; j++)
                {
                    byte holder = new byte();
                    holder = (byte)((dataToInsert[i] & (0 ^ (byte)Math.Pow(2, j))) / Math.Pow(2, j));
                    bitsToInsert[i * 8 + j] = holder;
                }
            }
            // Init chunks
            header = new Chuncks.WaveHeader();
            format = new Chuncks.WaveFormatChunk();
            data = new Chuncks.WaveDataChunk();
            int amount = Convert.ToInt32(bitsToInsert.Length);
            int numSamples = amount * 2;
            data.shortArray = new short[numSamples];
            short amplitude = 32760 / 15;
            // Fill the data array with sample data
            double freq = 440.0f;   // Concert A: 440Hz
            double t = (Math.PI * 2 * freq) / (format.dwSamplesPerSec * format.wChannels);
            for (int i = 0; i < numSamples - 1; i += 2)
            {
                for (int channel = 0; channel < format.wChannels; channel++)
                {
                    switch (type)
                    {
                        case WavType.SineWave:
                            {
                                short oldInt = Convert.ToInt16(amplitude * Math.Sin(t * i));
                                byte[] bytes = BitConverter.GetBytes(oldInt);
                                if (channel == 0)
                                {
                                    //put data only on the left channel
                                    bytes[0] = (byte)(bytes[0] ^ bitsToInsert[i / 2]);
                                }
                                short newInt = BitConverter.ToInt16(bytes, 0);
                                data.shortArray[i + channel] = newInt;
                                break;
                            }
                        case WavType.BlockWave:
                            {
                                short oldInt;
                                if (Convert.ToInt16(amplitude * Math.Sign(Math.Sin(t * i)))>0)
                                {
                                    oldInt = amplitude;
                                }
                                else
                                {
                                    oldInt = (short)(-amplitude); 
                                }
                                byte[] bytes = BitConverter.GetBytes(oldInt);
                                if (channel == 0)
                                {
                                    //put data only on the left channel
                                    bytes[0] = (byte)(bytes[0] ^ bitsToInsert[i / 2]);
                                }
                                short newInt = BitConverter.ToInt16(bytes, 0);
                                data.shortArray[i + channel] = newInt;
                                break;
                            }
                        case WavType.RandomWave:
                            {
                                Random rnd = new Random();
                                short randomValue = 0;
                                randomValue = Convert.ToInt16(rnd.Next(-amplitude, amplitude));
                                data.shortArray[i + channel] = randomValue;
                                break;
                            }
                    }
                }
            }
            data.dwChunkSize = (int)(data.shortArray.Length * (format.wBitsPerSample / 8));
        }
        public override byte[] GetData()
        {
            int byteCount = (data.shortArray.Length) / 16;
            byte[] bytesInserted = new byte[byteCount];
            for (int i = 0; i < byteCount; i += 1)
            {
                byte holder = new byte();
                for (int j = 0; j < 8; j++)
                {
                    holder ^= (byte)((data.shortArray[i * 16 + j * 2] ^ data.shortArray[i * 16 + 1 + j * 2]) * Math.Pow(2, j));
                }
                bytesInserted[i] = holder;
            }
            return bytesInserted;
        }
        public override void Load(string filePath)
        {
            header = new Chuncks.WaveHeader();
            format = new Chuncks.WaveFormatChunk();
            data = new Chuncks.WaveDataChunk();
            byte[] wav = System.IO.File.ReadAllBytes(filePath);

            format.wChannels = wav[22];
            byte[] samples = new byte[2];
            samples[0] = wav[24];
            samples[1] = wav[25];
            format.dwSamplesPerSec = BitConverter.ToUInt16(samples, 0);

            int sampleCount = (wav.Length - 44) / 2;
            data.shortArray = new short[sampleCount];
            int pos = 44;
            for (int i = 0; i < sampleCount; i += 2)
            {
                byte[] left = new byte[2];
                left[0] = wav[pos];
                left[1] = wav[pos + 1];
                data.shortArray[i] = BitConverter.ToInt16(left, 0);

                byte[] right = new byte[2];
                right[0] = wav[pos + 2];
                right[1] = wav[pos + 3];
                data.shortArray[i + 1] = BitConverter.ToInt16(right, 0);

                pos += 4;
            }
            data.dwChunkSize = (int)(data.shortArray.Length * (format.wBitsPerSample / 8));

        }


        public override void Save(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            BinaryWriter writer = new BinaryWriter(fileStream);

            writer.Write(header.sGroupID.ToCharArray());
            writer.Write(header.dwFileLength);
            writer.Write(header.sRiffType.ToCharArray());

            writer.Write(format.sChunkID.ToCharArray());
            writer.Write(format.dwChunkSize);
            writer.Write(format.wFormatTag);
            writer.Write(format.wChannels);
            writer.Write(format.dwSamplesPerSec);
            writer.Write(format.dwAvgBytesPerSec);
            writer.Write(format.wBlockAlign);
            writer.Write(format.wBitsPerSample);

            writer.Write(data.sChunkID.ToCharArray());
            writer.Write(data.dwChunkSize);
            foreach (short dataPoint in data.shortArray)
            {
                writer.Write(dataPoint);
            }

            writer.Seek(4, SeekOrigin.Begin);
            int filesize = (int)writer.BaseStream.Length;
            writer.Write(filesize - 8);

            writer.Close();
            fileStream.Close();
        }

        class Chuncks
        {
            public class WaveHeader
            {
                public string sGroupID; // RIFF
                public int dwFileLength; // total file length minus 8, which is taken up by RIFF
                public string sRiffType; // always WAVE

                /// <summary>
                /// Initializes a WaveHeader object with the default values.
                /// </summary>
                public WaveHeader()
                {
                    dwFileLength = 0;
                    sGroupID = "RIFF";
                    sRiffType = "WAVE";
                }
            }
            public class WaveFormatChunk
            {
                public string sChunkID;         // Four bytes: "fmt "
                public int dwChunkSize;        // Length of header in bytes
                public ushort wFormatTag;       // 1 (MS PCM)
                public ushort wChannels;        // Number of channels
                public int dwSamplesPerSec;    // Frequency of the audio in Hz... 44100
                public int dwAvgBytesPerSec;   // for estimating RAM allocation
                public ushort wBlockAlign;      // sample frame size, in bytes
                public ushort wBitsPerSample;    // bits per sample

                /// <summary>
                /// Initializes a format chunk with the following properties:
                /// Sample rate: 44100 Hz
                /// Channels: Stereo
                /// Bit depth: 16-bit
                /// </summary>
                public WaveFormatChunk()
                {
                    sChunkID = "fmt ";
                    dwChunkSize = 16;
                    wFormatTag = 1;
                    wChannels = 2;
                    dwSamplesPerSec = 44100;
                    wBitsPerSample = 16;
                    wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
                    dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;
                }
            }
            public class WaveDataChunk
            {
                public string sChunkID;     // "data"
                public int dwChunkSize;    // Length of header in bytes
                public short[] shortArray;  // 8-bit audio

                /// <summary>
                /// Initializes a new data chunk with default values.
                /// </summary>
                public WaveDataChunk()
                {
                    shortArray = new short[0];
                    dwChunkSize = 0;
                    sChunkID = "data";
                }
            }
        }

    }
}
