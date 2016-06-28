using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal class DmtxChannel
    {
        DmtxScheme _encScheme;     /* current encodation scheme */
        DmtxChannelStatus _invalid;
        int _inputIndex;      /* pointer to current input character */
        int _encodedLength; /* encoded length (units of 2/3 bits) */
        int _currentLength; /* current length (units of 2/3 bits) */
        int _firstCodeWord; /* */
        byte[] _encodedWords;
        byte[] _input;

        internal byte[] Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
            }
        }

        internal DmtxScheme EncScheme
        {
            get { return _encScheme; }
            set { _encScheme = value; }
        }

        internal DmtxChannelStatus Invalid
        {
            get { return _invalid; }
            set { _invalid = value; }
        }

        internal int InputIndex
        {
            get { return _inputIndex; }
            set { _inputIndex = value; }
        }

        internal int EncodedLength
        {
            get { return _encodedLength; }
            set { _encodedLength = value; }
        }

        internal int CurrentLength
        {
            get { return _currentLength; }
            set { _currentLength = value; }
        }

        internal int FirstCodeWord
        {
            get { return _firstCodeWord; }
            set { _firstCodeWord = value; }
        }


        internal byte[] EncodedWords
        {
            get
            {
                if (_encodedWords == null)
                {
                    _encodedWords = new byte[1558];
                }
                return _encodedWords;
            }
        }
    }

    internal class DmtxChannelGroup
    {
        DmtxChannel[] _channels;

        internal DmtxChannel[] Channels
        {
            get
            {
                if (_channels == null)
                {
                    _channels = new DmtxChannel[6];
                    for (int i = 0; i < 6; i++)
                    {
                        _channels[i] = new DmtxChannel();
                    }
                }
                return _channels;
            }
        }
    }
}
