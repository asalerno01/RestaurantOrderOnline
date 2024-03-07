namespace Server.Logger
{
	public static class Logger
	{
		public static void Log(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.BackgroundColor = ConsoleColor.Black;

			Console.WriteLine();
			Console.WriteLine(message);
			Console.WriteLine();

			Console.ResetColor();
		}
	}
}
