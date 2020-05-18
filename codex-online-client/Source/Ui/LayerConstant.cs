namespace codex_online
{
    public static class LayerConstant
    {
        public static int PickedUpRenderLayer { get; } = -2;
        public static int CardListWindowRenderLayer { get; } = -1;
        public static int DefaultRenderLayer { get; } = 0;
        public static int BoardRenderLayer { get; } = 1;

        public static int DefaultLayerDepth { get; } = 0;
        public static float LowestLayerDepth { get; } = 2;
        public static float LayerDepthIncriment { get; } = .0001f;
    }
}
