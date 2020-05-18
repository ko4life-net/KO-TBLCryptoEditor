using System;

namespace KO.TBLCryptoEditor.Core
{
    public class InitializationFailure
    {
        public string Reason { get; }

        public InitializationFailure(string reason)
        {
            Reason = reason;
        }
    }
}
