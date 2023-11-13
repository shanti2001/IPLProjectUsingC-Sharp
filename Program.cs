// See https://aka.ms/new-console-template for more information
// using System;

class Bowler{
    public String bowlerName;
    public double economic;
    public Bowler(String bowlerName,double economic){
        this.bowlerName = bowlerName;
        this.economic = economic;
    }
}
public class IPL{
    
    const int ID=0;
    const int SEASON=1;
    const int CITY=2;
    const int DATE=3;
    const int TEAM1=4;
    const int TEAM2=5;
    const int TOSS_WINNER=6;
    const int TOSS_DECISION=7;
    const int RESULT=8;
    const int DL_APPLIED=9;
    const int WINNER=10;
    const int WIN_BY_RUNS=11;
    const int WIN_BY_WICKETS=12;
    const int PLAYER_OF_MATCH=13;
    const int VENUE=14;
    const int UMPIRE1=15;
    const int UMPIRE2=16;
    const int UMPIRE3=17;
    const int MATCH_ID = 0;
    const int INNING = 1;
    const int BATTING_TEAM = 2;
    const int BOWLING_TEAM = 3;
    const int OVER = 4;
    const int BALL = 5;
    const int BATSMAN = 6;
    const int NON_STRIKER = 7;
    const int BOWLER = 8;
    const int IS_SUPER_OVER = 9;
    const int WIDE_RUNS = 10;
    const int BYE_RUNS = 11;
    const int LEG_BYE_RUNS = 12;
    const int NOBALL_RUNS = 13;
    const int PENALTY_RUNS = 14;
    const int BATSMAN_RUNS = 15;
    const int EXTRA_RUNS = 16;
    const int TOTAL_RUNS = 17;
    const int PLAYER_DISMISSED = 18;
    const int DISMISSAL_KIND = 19;
    const int FIELDER = 20;
    static string pathDeliveries = "C:/Users/SHANTINATH SAMANTA/OneDrive/Desktop/ProjectUsingDotnet/deliveries.csv";
    static string pathMatches = "C:/Users/SHANTINATH SAMANTA/OneDrive/Desktop/ProjectUsingDotnet/matches.csv";

    private static void GetNumberOfMatchesPerYear(List<Match>matches){
        Dictionary<string,int> numberOfMatchesPerYear = new Dictionary<string, int>();
        foreach(Match match in matches){
            numberOfMatchesPerYear[match.getSeason()] = numberOfMatchesPerYear.TryGetValue(match.getSeason(),out int value)?value+1:1;
            // numberOfMatchesPerYear.Add(match.getSeason(),GetOrDefault(numberOfMatchesPerYear,match.getSeason(),defaultValue: 0)+1);
        }
        foreach(string season in numberOfMatchesPerYear.Keys){
            Console.WriteLine(season+" : "+numberOfMatchesPerYear[season]);
        }
       
       
    }
    private static void GetNumberOfMatchesWonOfAllTeamsOverAllYears(List<Match> matches){
        Dictionary<string,int> numberOfMatchesWonOfAllTeamsOverAllYears = new Dictionary<string, int>();
        foreach(Match match in matches){
            if(match.getWinner() != ""){
            numberOfMatchesWonOfAllTeamsOverAllYears[match.getWinner()] = numberOfMatchesWonOfAllTeamsOverAllYears.TryGetValue(match.getWinner(),out int value)?value+1:1;
            }
        }
        foreach(string team in numberOfMatchesWonOfAllTeamsOverAllYears.Keys){
            Console.WriteLine(team+" : "+numberOfMatchesWonOfAllTeamsOverAllYears[team]);
        }
       

    }
    private static void GetExtraRunsConcededPerTeamIn2016(List<Match>matches,List<Delivery>deliveries){
        List<String> matchId = new List<string>();
        Dictionary<string,int> extraRunsConcededPerTeam = new Dictionary<string, int>();
        foreach(Match match in matches){
            if(match.getSeason().Equals("2016")){
                matchId.Add(match.getId());
            }
        }
        foreach(Delivery delivery in deliveries){
            if(matchId.Contains(delivery.getMatch_id())){
                int extra_runs = int.Parse(delivery.getExtra_runs());
                extraRunsConcededPerTeam[delivery.getBatting_team()] = extraRunsConcededPerTeam.TryGetValue(delivery.getBatting_team(),out int value)?value+extra_runs:extra_runs;
            }
        }
        foreach(string team in extraRunsConcededPerTeam.Keys){
            Console.WriteLine(team + " : "+ extraRunsConcededPerTeam[team]);
        }
        
    }
    private static void GetTopEconomicalBowlersOn2015(List<Match>matches,List<Delivery>deliveries){
        List<string> matchId = new List<string>();
        Dictionary<string,int> runs = new Dictionary<string, int>();
        Dictionary<string,int> balls = new Dictionary<string,int>();
        List<Bowler> ecoBowler = new List<Bowler>();
        foreach(Match match in matches){
            if(match.getSeason().Equals("2015")){
                matchId.Add(match.getId());
            }
        }
        foreach(Delivery delivery in deliveries){
            if(matchId.Contains(delivery.getMatch_id())){
                int totalRuns=0;
                int totalBalls=0;
                if(!delivery.getWide_runs().Equals("0") || !delivery.getNoball_runs().Equals("0")){
                    totalRuns+=int.Parse(delivery.getWide_runs())+int.Parse(delivery.getNoball_runs());
                }
                else if(!delivery.getBye_runs().Equals("0") || !delivery.getLeg_by_run().Equals("0")){
                    totalBalls+=1;
                }
                else{
                    totalRuns+=int.Parse(delivery.getBatsman_run());
                    totalBalls+=1;
                }
                runs[delivery.getBowler()] = runs.TryGetValue(delivery.getBowler(),out int run)?run+totalRuns:totalRuns;
                balls[delivery.getBowler()] = balls.TryGetValue(delivery.getBowler(),out int ball)?ball+totalBalls:totalBalls;
            }
        }
        foreach(string bowler in runs.Keys){
            if(balls.ContainsKey(bowler)){
                double over = balls[bowler]/(double)6;
                double economic = runs[bowler]/over;
                ecoBowler.Add(new Bowler(bowler,economic));
            }
        }
        ecoBowler.Sort((ele1,ele2)=>{
            if(ele1.economic==ele2.economic){
                return 0;
            }
            else if(ele1.economic>ele2.economic){
                return 1;
            }
            else{
                return -1;
            }
        });
        foreach(Bowler bowler in ecoBowler){
            Console.WriteLine(bowler.bowlerName+" : "+bowler.economic);
        }

    }
    public static void Main(string []args){
        
        List<Delivery> deliveries = GetDeliveriesData(pathDeliveries);
        List<Match> matches = GetMatchesData(pathMatches);
        
        // GetNumberOfMatchesPerYear(matches);
        // GetNumberOfMatchesWonOfAllTeamsOverAllYears(matches);
        // GetExtraRunsConcededPerTeamIn2016(matches,deliveries);
        GetTopEconomicalBowlersOn2015(matches,deliveries);
    }

    static List<Match> GetMatchesData(string pathMatches){
         List<Match> matches= new List<Match>();
        string line = "";
        bool isFirstRow = true;

        try
        {
            using (StreamReader reader = new StreamReader(pathMatches))
            {
                while ((line = reader.ReadLine()) != null)
                {
                     if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue; 
                    }
                    string[] data = line.Split(",");
                    Match match= new Match();
                    match.setId(data[ID]);
                    match.setSeason(data[SEASON]);
                    match.setCity(data[CITY]);
                    match.setDate(data[DATE]);
                    match.setTeam1(data[TEAM1]);
                    match.setTeam2(data[TEAM2]);
                    match.setToss_winner(data[TOSS_WINNER]);
                    match.setToss_decision(data[TOSS_DECISION]);
                    match.setResult(data[RESULT]);
                    match.setDl_applied(data[DL_APPLIED]);
                    match.setWinner(data[WINNER]);
                    match.setWin_by_run(data[WIN_BY_RUNS]);
                    match.setWi_by_wicket(data[WIN_BY_WICKETS]);
                    match.setPlayer_of_match(data[PLAYER_OF_MATCH]);
                    match.setVenue(data[VENUE]);
                    if(data.Length>=16) {
                        match.setUmpire1(data[UMPIRE1]);
                    }
                    if(data.Length>=17) {
                        match.setUmpire2(data[UMPIRE2]);
                    }
                    if(data.Length==18) {
                        match.setUmpire3(data[UMPIRE3]);
                    }
                    matches.Add(match);
                }
            }
        } catch (FileNotFoundException e) {
            Console.WriteLine($"File not found: {e.Message}");
        } catch (IOException e) {
            Console.WriteLine($"IO Exception: {e.Message}");
        }
        return matches;

    }
    static List<Delivery> GetDeliveriesData(string pathDeliveries)
    {
        List<Delivery> deliveries = new List<Delivery>();
        string line = "";
        bool isFirstRow = true;

        try
        {
            using (StreamReader reader = new StreamReader(pathDeliveries))
            {
                while ((line = reader.ReadLine()) != null)
                {
                     if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue; 
                    }
                    string[] data = line.Split(",");
                    Delivery delivery= new Delivery();
                    delivery.setMatch_id(data[MATCH_ID]);
                    delivery.setInning(data[INNING]);
                    delivery.setBatting_team(data[BATTING_TEAM]);
                    delivery.setBowling_team(data[BOWLING_TEAM]);
                    delivery.setOver(data[OVER]);
                    delivery.setBall(data[BALL]);
                    delivery.setBatsman(data[BATSMAN]);
                    delivery.setNon_striker(data[NON_STRIKER]);
                    delivery.setBowler(data[BOWLER]);
                    delivery.setIs_supper_over(data[IS_SUPER_OVER]);
                    delivery.setWide_runs(data[WIDE_RUNS]);
                    delivery.setBye_runs(data[BYE_RUNS]);
                    delivery.setLeg_by_run(data[LEG_BYE_RUNS]);
                    delivery.setNoball_runs(data[NOBALL_RUNS]);
                    delivery.setPenalty_runs(data[PENALTY_RUNS]);
                    delivery.setBatsman_run(data[BATSMAN_RUNS]);
                    delivery.setExtra_runs(data[EXTRA_RUNS]);
                    delivery.setTotal_runs(data[TOTAL_RUNS]);
                    if(data.Length>=19){
                        delivery.setPlayer_dismissed(data[PLAYER_DISMISSED]);
                    }
                    if(data.Length>=20){
                        delivery.setDismissed_kind(data[DISMISSAL_KIND]);
                    }
                    if(data.Length>=21){
                        delivery.setFielder(data[FIELDER]);
                    }
                    deliveries.Add(delivery);

                }
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found: {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"IO Exception: {e.Message}");
        }

        return deliveries;
    }
    
}
