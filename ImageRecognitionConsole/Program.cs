using static ImageModel.ImageModelHandler;
using static ImageModel.ImageModel;
using System.Text;

namespace ImagerecognitionConsole
{
    public class Program
    {

        public static void Main(string[] args)
        {
            List<ValueTuple<string, ModelOutput>> results = new();

            if (args.Length == 0)
            {
                Console.WriteLine("No command line arguments given. Please provide a file or folder path.");
                string path = Console.ReadLine();
                Predict(path);

                results = Predict(path);
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    List<ValueTuple<string, ModelOutput>> output = Predict(args[i]);

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
                StringBuilder sb = new StringBuilder($"{results[i].Item1}: {results[i].Item2.PredictedLabel}");

                if (results[i].Item2.Score != null)
                {
                    for (int j = 0; j < results[i].Item2.Score.Length; j++)
                    {
                        sb.Append($", {results[i].Item2.Score[j]}");
                    }
                }

                Console.WriteLine(sb.ToString());
            }

        }

        static List<ValueTuple<string, ModelOutput>> Predict(string path)
        {
            List<ValueTuple<string, ModelOutput>> output = new();

            string fullPath = Path.GetFullPath(path);

            if (File.Exists(fullPath))
            {
                ModelOutput result = PredictFile(fullPath);
                output.Add((fullPath, result));
            }
            else if (Directory.Exists(fullPath))
            {
                List<ValueTuple<string, ModelOutput>> results = PredictDirectory(fullPath);

                for (int i = 0; i < results.Count; i++)
                {
                    output.Add(results[i]);
                }
            }
            else
            {
                output.Add((fullPath, new ModelOutput()
                {
                    PredictedLabel = "failure",
                }));
            }

            return output;
        }

    }
}