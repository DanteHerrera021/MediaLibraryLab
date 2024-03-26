using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

string userInput = "";

do
{

    Console.WriteLine("Welcome to the Media Library Lab!");
    Console.WriteLine("Please type 1 to view all available movies");
    Console.WriteLine("Please type 2 to add a new movie");
    Console.WriteLine("Press enter to exit the program.");

    userInput = Console.ReadLine();

    if (userInput == "1")
    {

        logger.Info($"Displaying {movieFile.Movies.Count} movies");

        for (int i = 0; i < movieFile.Movies.Count; i++)
        {
            Console.WriteLine(movieFile.Movies[i].Display());
        }
    }
    else if (userInput == "2")
    {
        // TODO: ALLOW FOR MOVIE ADDITION

        while (true)
        {
            Console.WriteLine("Add a new movie (Y/N)?");
            string resp = Console.ReadLine().ToUpper();
            if (resp != "Y") { break; }

            Movie movie = new Movie();

            movie.mediaId = movieFile.Movies[^1].mediaId + 1;

            Console.WriteLine("Enter movie title");
            movie.title = Console.ReadLine();

            Console.WriteLine("Enter the movie director");
            movie.director = Console.ReadLine();

            int hours;
            int mins;
            int secs;
            Console.WriteLine("Enter the movie's run time in hours");
            hours = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the movie's run time in the remaining minutes");
            mins = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the movie's run time in the remaining seconds");
            secs = int.Parse(Console.ReadLine());

            movie.runningTime = new TimeSpan(hours, mins, secs);

            string input;
            do
            {
                Console.WriteLine("Enter genre (or done to quit)");

                input = Console.ReadLine();

                if (input != "done" && input.Length > 0)
                {
                    movie.genres.Add(input);
                }
            } while (input != "done");

            if (movie.genres.Count == 0)
            {
                movie.genres.Add("(no genres listed)");
            }

        }
    }

} while (userInput == "1" || userInput == "2");

logger.Info("Program ended");
