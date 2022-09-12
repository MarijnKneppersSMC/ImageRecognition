namespace ImageModel
{

    /// <summary>
    /// All possibilities that can result out of a pridiction
    /// </summary>
    public enum PredictedLabel: sbyte
    {
        /// <summary>
        /// The program encountered a problem while trying to predict the contents of an image
        /// </summary>
        FAILURE = -1,

        /// <summary>
        /// The program decided that the subject of the provided image is most likely a wolf
        /// </summary>
        WOLF = 0,

        /// <summary>
        /// The program decided that the subject of the provided image is most likely a car
        /// </summary>
        CAR = 1
    }

    public struct PredictionResult
    {

        /// <summary>
        /// The full file path to the provided image
        /// </summary>
        public string path;

        /// <summary>
        /// The label that was predicted to correspond with the provided image
        /// </summary>
        public PredictedLabel predictedLabel;

        /// <summary>
        /// How certain the model was of the possible outcomes. Only exists when predictedLabel != FAILURE.
        /// [0]: wolf
        /// [1]: car
        /// </summary>
        public float[] scores;

        /// <summary>
        /// A description of the encountered error. Only exists when predictedLabel == FAILURE.
        /// </summary>
        public string error;
    }
}
