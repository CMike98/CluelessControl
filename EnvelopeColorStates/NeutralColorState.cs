﻿using CluelessControl.Cheques;
using CluelessControl.Envelopes;

namespace CluelessControl.EnvelopeColorStates
{
    public class NeutralColorState : ColorState
    {
        public NeutralColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.White,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetDefaultTextColor()
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.White,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetDefaultTextColor()
            };
        }
    }
}
