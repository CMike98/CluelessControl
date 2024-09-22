namespace CluelessControl.EnvelopeColorStates
{
    public record EnvelopeColorCollection
    {
        public required Color BackgroundColor { get; init; }

        public required Color LineColor { get; init; }

        public required Color NumberFontColor { get; init; }

        public required Color ChequeFontColor { get; init; }
    }
}
