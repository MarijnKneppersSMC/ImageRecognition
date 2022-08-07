using static ImageModel.ImageModelHandler;
using ImageModel;
using System.Collections.Generic;
using System.IO;

namespace ImagerecognitionConsole
{
    public class Program
    {

        public static void Main(string[] args)
        {
            List<ValueTuple<string, PredictionResult>> results = new();

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
                    List<ValueTuple<string, PredictionResult>> output = Predict(args[i]);

                    for(int j = 0; j < output.Count; j++)
                    {
                        results.Add(output[j]);
                    }
                }
            }

            Console.Clear();

            Console.WriteLine(results.Count);

            for(int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{results[i].Item1}: {((int)results[i].Item2)}");
            }

        }

        static List<ValueTuple<string, PredictionResult>> Predict(string path)
        {
            List<ValueTuple<string, PredictionResult>> output = new();

            string fullPath = Path.GetFullPath(path);

            if (File.Exists(fullPath))
            {
                PredictionResult result = PredictFile(fullPath);
                output.Add((fullPath, result));
            }
            else if (Directory.Exists(fullPath))
            {
                List<ValueTuple<string, PredictionResult>> results = PredictDirectory(fullPath);

                for (int i = 0; i < results.Count; i++)
                {
                    output.Add(results[i]);
                }
            }
            else
            {
                output.Add((fullPath, PredictionResult.FAILURE));
            }

            return output;
        }

    }
}