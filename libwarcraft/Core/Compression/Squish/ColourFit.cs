// TODO: Break this out into a real library
// <auto-generated>
// ReSharper disable All
// Note: this file is not actually auto-generated, but rather a straight source import from an external library.

namespace Warcraft.Core.Compression.Squish
{
    internal abstract class ColourFit
    {
        #region Fields

        protected ColourSet _Colours;
        private SquishOptions _Flags;

        #endregion

        #region Constructor

        protected ColourFit(ColourSet colours, SquishOptions flags)
        {
            _Colours = colours;
            _Flags = flags;
        }

        #endregion

        #region Public

        public void Compress(ref byte[] block)
        {
            if (_Flags.HasFlag(SquishOptions.DXT1))
            {
                Compress3(block);
                if (!_Colours.IsTransparent)
                {
                    Compress4(block);
                }
            }
            else
            {
                Compress4(block);
            }
        }

        #endregion

        #region Protected

        protected abstract void Compress3(byte[] block);

        protected abstract void Compress4(byte[] block);

        #endregion
    }
}
