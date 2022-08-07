using static ImageModel.ImageModel;

namespace ImageModel
{
    public partial class ImageModelHandler
    {

        public static PredictionResult PredictFile(string path)
        {

            string fullPath = Path.GetFullPath(path);

            if(!File.Exists(fullPath))
            {
                return PredictionResult.FAILURE;
            }

            byte[] imageData = File.ReadAllBytes(fullPath);

            ModelInput modelInput = new ModelInput()
            {
                ImageSource = imageData
            };

            ModelOutput output = Predict(modelInput);

            return (PredictionResult)((output.PredictedLabel == "wolves")? PredictionResult.WOLF : PredictionResult.CAR);
        }

        public static List<ValueTuple<string, PredictionResult>> PredictDirectory(string path)
        {
            List<ValueTuple<string, PredictionResult>> results = new();

            string fullPath = Path.GetFullPath(path);

            if(!Directory.Exists(fullPath))
            {
                results.Add((fullPath, PredictionResult.FAILURE));
                return results;
            }

            string[] files = Directory.GetFiles(fullPath);

            for(int i = 0; i < files.Length; i++)
            {
                results.Add((files[i], PredictFile(files[i])));
            }

            return results;
        }
    }

    public enum PredictionResult : int
    {
        FAILURE = -1,
        CAR = 0,
        WOLF = 1
        
    }
}
