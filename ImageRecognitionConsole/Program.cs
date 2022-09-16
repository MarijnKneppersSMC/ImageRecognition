using static ImageModel.ImageModelHandler;
using System.Text;
using ImageModel;

namespace ImagerecognitionConsole
{
    public class Program
    {

        public static int Main(string[] args)
        {
            List<PredictionResult> results = new();

            if (args.Length == 0)
            {
#if DEBUG
                Console.WriteLine("No command line arguments given. Please provide a file or folder path.");
                string path = Console.ReadLine();

                results = Predict(path);
#elif RELEASE
                return 1;
#endif
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    List<PredictionResult> output = Predict(args[i]);

                    for (int j = 0; j < output.Count; j++)
                    {
                        results.Add(output[j]);
                    }
                }
            }

            Console.Clear();

            Console.WriteLine(results.Count);

            for (int i = 0; i < results.Count; i++)
            {
                StringBuilder sb = new StringBuilder($"{results[i].path}, {results[i].predictedLabel}");

                if (results[i].predictedLabel == PredictedLabel.FAILURE)
                {
                    sb.Append($", {results[i].error}");
                }
                else
                {
                    for (int j = 0; j < results[i].scores.Length; j++)
                    {
                        sb.Append($", {results[i].scores[j].ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                    }
                }

                Console.WriteLine(sb.ToString());
            }

            return 0;

        }

        static List<PredictionResult> Predict(string path)
        {
            List<PredictionResult> output = new();

            string fullPath = Path.GetFullPath(path);

            if (File.Exists(fullPath))
            {
                PredictionResult result = PredictFile(fullPath);
                output.Add(result);
            }
            else if (Directory.Exists(fullPath))
            {
                List<PredictionResult> results = PredictDirectory(fullPath);

                for (int i = 0; i < results.Count; i++)
                {
                    output.Add(results[i]);
                }
            }
            else
            {
                output.Add(new PredictionResult()
                {
                    predictedLabel = PredictedLabel.FAILURE,
                    path = fullPath,
                    error = "File not found"
                });
            }

            return output;
        }

    }
}