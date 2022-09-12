using static ImageModel.ImageModel;

namespace ImageModel
{
    public partial class ImageModelHandler
    {

        public static PredictionResult PredictFile(string path)
        {

            string fullPath = Path.GetFullPath(path);

            if (!File.Exists(fullPath))
            {
                return new PredictionResult()
                {
                    predictedLabel = PredictedLabel.FAILURE,
                    path = fullPath,
                    error = "File not found"
                };
            }

            byte[] imageData = File.ReadAllBytes(fullPath);

            ModelInput modelInput = new ModelInput()
            {
                ImageSource = imageData
            };

            ModelOutput output = Predict(modelInput);

            Enum.TryParse(typeof(PredictedLabel), output.PredictedLabel, true, out object predictedLabel);

            PredictionResult result = new PredictionResult()
            {
                path = fullPath,
                scores = output.Score,
                predictedLabel = (PredictedLabel)predictedLabel
            };

            return result;
        }

        public static List<PredictionResult> PredictDirectory(string path)
        {
            List<PredictionResult> results = new();

            string fullPath = Path.GetFullPath(path);

            if (!Directory.Exists(fullPath))
            {
                results.Add(new PredictionResult()
                {
                    predictedLabel = PredictedLabel.FAILURE,
                    error = "Directory not found",
                    path = fullPath
                });
                return results;
            }

            string[] files = Directory.GetFiles(fullPath);

            for (int i = 0; i < files.Length; i++)
            {
                results.Add(PredictFile(files[i]));
            }

            return results;
        }
    }
}
