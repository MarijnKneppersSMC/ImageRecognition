namespace ImageModel
{

    public enum PredictedLabel: sbyte
    {
        FAILURE = -1,
        WOLF = 0,
        CAR = 1
    }

    public struct PredictionResult
    {
        public string path;

        public PredictedLabel predictedLabel;

        public float[] scores;

        public string error;
    }
}
