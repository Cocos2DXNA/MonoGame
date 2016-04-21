// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

#if MONOMAC && PLATFORM_MACOS_LEGACY
using MonoMac.OpenAL;
#else
using OpenTK.Audio.OpenAL;
#endif

namespace Microsoft.Xna.Framework.Audio
{
    internal class OALSoundBuffer : IDisposable
    {
        int openALDataBuffer;
        ALFormat openALFormat;
        int dataSize;
        int sampleRate;
        bool _isDisposed;
        public int SourceId;

        public OALSoundBuffer()
        {
            try
            {
                AL.GenBuffers(1, out openALDataBuffer);
                ALHelper.CheckError("Failed to generate OpenAL data buffer.");
            }
            catch (DllNotFoundException e)
            {
                throw new NoAudioHardwareException("OpenAL drivers could not be found.", e);
            }
        }
        public OALSoundBuffer(int bufferId, int sourceId)
        {
            openALDataBuffer = bufferId;
            SourceId = sourceId;
        }
        public OALSoundBuffer(int bufferId)
        {
            openALDataBuffer = bufferId;
        }

        ~OALSoundBuffer()
        {
            Dispose(false);
        }

        public int OpenALDataBuffer
        {
            get
            {
                return openALDataBuffer;
            }
        }

        public double Duration
        {
            get;
            set;
        }

        public void BindDataBuffer(byte[] dataBuffer, ALFormat format, int size, int sampleRate)
        {
            openALFormat = format;
            dataSize = size;
            Duration = -1;
            this.sampleRate = sampleRate;
            AL.BufferData(openALDataBuffer, openALFormat, dataBuffer, dataSize, this.sampleRate);
            ALHelper.CheckError("Failed to fill buffer.");

            int bits, channels;

            AL.GetBuffer(openALDataBuffer, ALGetBufferi.Bits, out bits);
            ALHelper.CheckError(string.Format("Failed to get buffer bits: format={0}, size={1}, sampleRate={2}", format, size, sampleRate));

            AL.GetBuffer(openALDataBuffer, ALGetBufferi.Channels, out channels);

            ALHelper.CheckError(string.Format("Failed to get buffer channels: format={0}, size={1}, sampleRate={2}", format, size, sampleRate));
            Duration = (float)(size / ((bits / 8) * channels)) / (float)sampleRate;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Clean up managed objects
                }
                // Release unmanaged resources
                if (AL.IsBuffer(openALDataBuffer))
                {
                    ALHelper.CheckError("Failed to fetch buffer state.");
                    AL.DeleteBuffers(1, ref openALDataBuffer);
                    ALHelper.CheckError("Failed to delete buffer.");
                }

                _isDisposed = true;
            }
        }
    }
}
