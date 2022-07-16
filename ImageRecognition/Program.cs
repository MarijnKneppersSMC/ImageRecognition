namespace ImageRecognition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to image: ");

            string path = Console.ReadLine();

            var sampleData = new ImageModel.ModelInput()
            {
                ImageSource = path,
            };

            var results = ImageModel.Predict(sampleData);

            Console.WriteLine($"This image contains a {((results.Prediction == "cars") ? "car" : "wolf")}\n");

            Console.WriteLine("Certainty:");

            Console.WriteLine($"\tCar: {results.Score[0] * 100}%");
            Console.WriteLine($"\tWolf: {results.Score[1] * 100}%");
        }
    }
}