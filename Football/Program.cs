using Football.Management;
using Football.Models;
using System.Transactions;
using System.Xml.Serialization;

class Program
{
    static TeamManagement teamManagement = new TeamManagement();
    static FootballPlayerManagement playerManagement = new FootballPlayerManagement();
    static  GameManagement gameManagement = new GameManagement();
    static void Main(string[] args)
    {
        CollectInformation();
        Menu();
    }
    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("Football Management Systems:");
            Console.WriteLine("1.Enter the results of the finished game");
            Console.WriteLine("2.List current status and players of a team");
            Console.WriteLine("3.Rank score table");
            Console.WriteLine("4.Rank teams with the most goal scored and the most goal conceded");
            Console.WriteLine("5.Rank players who scored the most goals");
            Console.WriteLine("0.Exit!");
            Console.WriteLine("Select your choice:");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    EnterGameResults();
                    break;
                case "2":
                    ListTeamStatus();
                    break;
                case "3":
                    RankScoreTable();
                    break;
                case "4":
                    RankTeamsGoals();
                    break;
                case "5":
                    RankPlayersGoals();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Sorry,but you entered incorrect choice, try again.");
                    break;
            }
        }
    }
    static void EnterGameResults()
    {
        try
        {
            Console.WriteLine("Enter week number:");
            int weekNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter home team code:");
            int homeTeamCode = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter guest team code:");
            int guestHomeCode = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter home team's goals:");
            int homeTeamGoals = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter guest team's goals:");
            int guestTeamGoals = int.Parse(Console.ReadLine());

            Games game = new Games()
            {
                WeekNumber = weekNumber,
                HomeTeamCode = homeTeamCode,
                GuestTeamCode = guestHomeCode,
                HomeGoals = homeTeamGoals,
                GuestGoals = guestTeamGoals
            };
            gameManagement.Add(game);
            UpdateTeam(game);
            UpdatePlayer(homeTeamCode, guestHomeCode);
            UpdatePlayer(guestHomeCode, guestTeamGoals);
        }
        catch(FormatException ex)
        {
            Console.WriteLine($"There is an error due ti fomat of input:{ex.Message}");
        }
        catch(Exception ex) 
        {
            Console.WriteLine($"An error happened:{ex.Message}");
        }
    }
    static void UpdateTeam(Games game )
    {
        try
        {
            Team hometeam = teamManagement.GetTeam(game.HomeTeamCode);
            Team guestTeam = teamManagement.GetTeam(game.GuestTeamCode);
            if (hometeam == null || guestTeam == null)
            {
                throw new Exception("one of the teams or both of them is not found.");
            }
            hometeam.GoalsScored += game.HomeGoals;
            hometeam.GoalsConceded += game.GuestGoals;
            guestTeam.GoalsScored += game.GuestGoals;
            guestTeam.GoalsConceded += game.HomeGoals;


            if (game.HomeGoals == game.GuestGoals)
            {
                hometeam.Equality++;
                guestTeam.Equality++;
            }
            else if (game.HomeGoals > game.GuestGoals)
            {
                hometeam.Wins++;
                guestTeam.Losses++;
            }
            else
            {
                guestTeam.Wins++;
                hometeam.Losses++;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error happened during updating teams:{ex.Message}");
        }
        
    }
    static void UpdatePlayer(int teamCode, int teamGoals)
    {
        try
        {
            for (int i = 0; i < teamGoals; i++)
            {
                Console.WriteLine($"Enter form number of player who scored goal {i + 1}:");
                int formNumber = int.Parse(Console.ReadLine());
                FootballPlayers player = playerManagement.GetPlayers(formNumber);
                if (player != null)
                {
                    player.GoalsScored++;
                    playerManagement.Update(player);
                }
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Input is not in correct format:{ex.Message}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An error happened while update team's player:{ex.Message}");
        }
    }
    static void ListTeamStatus()
    {
        Console.WriteLine("Enter name of team:");
        string teamName = Console.ReadLine();
        Team team = teamManagement.GetTeamByName(teamName);

        if(team != null )
        {
            Console.WriteLine($"Team name: {team.TeamName}, Wins:{team.Wins}, Losses: {team.Losses}, Equality: {team.Equality}, Goals Scored; {team.GoalsScored}, Goals Conceded:{team.GoalsConceded}, Points:{team.Points}, Goals Difference: {team.GoalDifference}");
            var players = playerManagement.GetPlayersByTeam(team.TeamCode).OrderBy(p => p.FormNumber);
            Console.WriteLine("All information about players:");
            foreach( var player in players )
            {
                Console.WriteLine($"Form number: {player.FormNumber}, Name:{player.FullName}, Goals Scored: { player.GoalsScored}");
            }
        }
        else
        {
            Console.WriteLine("Sorry, but there is no team.");
        }
    }
    static void RankScoreTable()
    {
        var teams = teamManagement.GetAll().OrderByDescending(t=>t.Points).ThenByDescending(t=>t.GoalDifference);
        Console.WriteLine("Team\tWins\tEquality\tLosses\tGoals Scored\tGoals Conceded\tGoal Difference\tPoints");
        foreach( var team in teams )
        {
            Console.WriteLine($"{team.TeamName}\t{team.Wins}\t{team.Losses}\t{team.Equality}\t{team.GoalDifference}\t{team.Points}\t{team.GoalsConceded}\t{team.GoalsScored}");
        }
    }
    static void RankTeamsGoals()
    {
        var teamGoalsScored=teamManagement.GetAll().OrderByDescending(t=>t.GoalsScored).ToList();
        var teamGoalsConceded=teamManagement.GetAll().OrderByDescending(t=>t.GoalsConceded).ToList();
        Console.WriteLine("Teams which have the most goals scored:");
        foreach ( var team in teamGoalsScored)
        {
            Console.WriteLine($"{team.TeamName}\t{team.GoalsScored}");
        }
        Console.WriteLine("Teams which have the most goals conceded:");
        foreach (var team in teamGoalsConceded)
        {
            Console.WriteLine($"{team.TeamName}\t{team.GoalsConceded}");
        }
    }
    static void RankPlayersGoals()
    {
        var players = playerManagement.GetAll().OrderByDescending(p=>p.GoalsScored);
        Console.WriteLine("Player\tForm number\tGoals Scored");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.FullName}\t{player.FormNumber}\t{player.GoalsScored}");
        }
    }
    static void CollectInformation()
    {
        try
        {
            teamManagement.Add(new Team { TeamCode = 1, TeamName = "Qarabag" });
            teamManagement.Add(new Team { TeamCode = 2, TeamName = "Sumqayit" });
            teamManagement.Add(new Team { TeamCode = 3, TeamName = "Neftchi" });

            playerManagement.Add(new FootballPlayers { FormNumber = 18, FullName = " Olavio Juninyo", GoalsScored = 17, TeamCode=1});
            playerManagement.Add(new FootballPlayers { FormNumber = 9, FullName = "  Aleksandr Ramalinqom", GoalsScored = 13,TeamCode = 1 });
            playerManagement.Add(new FootballPlayers { FormNumber = 7, FullName = "Yassin Benzia  ", GoalsScored = 12,TeamCode=2 });
            playerManagement.Add(new FootballPlayers { FormNumber = 5, FullName = "Farid Guliyev ", GoalsScored = 11, TeamCode = 2 });
            playerManagement.Add(new FootballPlayers { FormNumber = 90, FullName = "Neriman Axundzade ", GoalsScored = 10,TeamCode=3 });
            playerManagement.Add(new FootballPlayers { FormNumber = 3, FullName = "Hajiyev Rahman", GoalsScored = 5, TeamCode = 3 });
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An error happened:{ex.Message}");
        }
    }
}