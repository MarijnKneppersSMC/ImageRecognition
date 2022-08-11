using static ImageModel.ImageModel;

namespace ImageModel
{
    public partial class ImageModelHandler
    {

        public static ModelOutput PredictFile(string path)
        {

            string fullPath = Path.GetFullPath(path);

            if (!File.Exists(fullPath))
            {
                return new ModelOutput()
                {
                    PredictedLabel = "failure",
                };
            }

            byte[] imageData = File.ReadAllBytes(fullPath);

            ModelInput modelInput = new ModelInput()
            {
                ImageSource = imageData
            };

            ModelOutput output = Predict(modelInput);

            return output;
        }

        public static List<ValueTuple<string, ModelOutput>> PredictDirectory(string path)
        {
            List<ValueTuple<string, ModelOutput>> results = new();

            string fullPath = Path.GetFullPath(path);

            if (!Directory.Exists(fullPath))
            {
                results.Add((fullPath, new ModelOutput()
                {
                    PredictedLabel = "failure",
                }));
                return results;
            }

            string[] files = Directory.GetFiles(fullPath);

            for (int i = 0; i < files.Length; i++)
            {
                results.Add((files[i], PredictFile(files[i])));
            }

            return results;
        }
    }
}
