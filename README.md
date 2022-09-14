# Image Recognition

This program uses machine learning to say whether an image contains a wolf or a car.

## Arguments
The program can contain an infinite amount of paths to files and folders. These paths can be full paths or relative paths.

When no arguments are provided, the result depends on the executable optimization.

When the executable is built in Debug mode, you will be promted to enter the path to a file or folder.

However, when the executable is built in Release mode, the program will exit with exit code 1.

## Output
After successfully predicting all provided images, the application will output the following:

First, the application will clear the console so the output can be read from the first line of the console.

After that, the first thing that will be logged to the console is an unsigned integer. This depicts the amount of prediction results will be logged to the console.

After that, the program will output the previously mentioned amount of prediction results on just as many lines.

All prediction result variables are seperated by a comma and a space. (", ")

The first variable is the full path to the file. This is to be able to distinguish between files with the same name that are in different folders.

The second variable is a stringified version of the `PredictedLabel` enum. This can be parsed back to an enum instance with the following code:
```cs
Enum.TryParse(typeof(PredictedLabel), <PREDICTED_LABEL>, true, out object predictedLabel);
```
Here, `PREDICTED_LABEL` should be replaced with the stringified enum.

This enum can either be WOLF, CAR or FAILURE. Failure is all encompassing for every possible failure.

The third variable depends on the predicted label. When the predicted label is FAILURE, this will be a string explaining the failure.

Otherwise, the third and fourth variable are the scores for the predictions in the order `wolf-car` where the scores are floating point integers.

So in the end, these are all the possible outputs:

``FULL_PATH, FAILURE, REASON``
``FULL_PATH, PREDICTED_LABEL, WOLF_SCORE, CAR_SCORE``

## Exit codes
There are 2 possible exit codes for this application.

When the application executes normally, the program will exit with exit code 0.

When no arguments are provided, the program will immediately terminate with exit code 1.

## Potential optimizations
One possible optimization is to make the application multi-threaded.

Another possible optimization is to check whether a file is supplied twice. For example, when a file is provided as an argument and it' parent folder is too.