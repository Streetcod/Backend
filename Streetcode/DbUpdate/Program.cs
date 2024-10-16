﻿using DbUp;
using Microsoft.Extensions.Configuration;

public class Program
{
    static int Main(string[] args)
    {
        string migrationPath = /*"/Streetcode.DAL/Persistence/ScriptsMigration"*/Path.Combine("/"/*Directory.GetCurrentDirectory()*/,
            "Streetcode.DAL", "Persistence", "ScriptsMigration");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

        var configuration = new ConfigurationBuilder()
            .SetBasePath("/Streetcode/Streetcode.WebApi"/*Path.Combine(Directory.GetCurrentDirectory(), "/Streetcode/Streetcode.WebApi")*/)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables("STREETCODE_")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        string pathToScript = "";

        pathToScript = migrationPath;
        
        var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem(pathToScript)
                .LogToConsole()
                .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}
