
public class Character {
    int _ID;
    string _firstName;
    string _lastName;
    string _status; //dead - wounded/50% or less health - [type of sickness] - healthy
    int _gender; //0 male, 1 female
    int _age; //8 - 12, 13 - 18, 19 - 30, 31 - 50, 50+
    int _health;
    string _greeting; //this characters default greeting
    int _importance; //if this character is influential to the main story
    string _faction;

    //lists of things they hate, dislike, are okay with, like and love
    CharacterManager.loves[] _loves;
    CharacterManager.likes[] _likes;
    CharacterManager.okays[] _okays;
    CharacterManager.dislikes[] _dislikes;
    CharacterManager.hates[] _hates;
    
    int[] _traits; //things that make them unique
    int[] _goals; //things they want to accomplish
    int[] _skills; //things they are good at
    string _relationship; //relationship with the player
    int _relationshipLvl; //how they feel about you

    int[] _convoList;

    int _roomLocation;
    int[] _missions; //missions this character offers
    int[] _involved; //missions this character is involved in - mission stops if provoked by player
    int[][] _missionTimes; //times the missions are offered

    int[] _trainings;
    int[][] _trainingHours;
    int _provoked;
    int[] _behaviors;

    public Character(int ID, string firstName, string lastName, int gender, int age, int health, int[] traits, int[] goals, int[] skills) {
        _ID = ID;
        _firstName = firstName;
        _lastName = lastName;
        _status = "Alive";
        _gender = gender;
        _age = age;
        _health = health;
        _greeting = "";
        _importance = 0;

        _traits = traits;
        _goals = goals;
        _skills = skills;
        _relationshipLvl = 50;

        _convoList = new int[3] { 0, -1, -1 };

        _roomLocation = -1;
        _missions = new int[3] {0,0,0 };
        _missionTimes = new int[3][];

        _trainings = new int[3] { 0, 0, 0 };
        _trainingHours = new int[2][];
        _provoked = 0;
        _behaviors = new int[2]{0,0};

    }

    public Character() {
        _status = "Alive";
        _roomLocation = -1;
        _health = 100;
        _greeting = "";
        _importance = 0;

        _relationshipLvl = 50;

        _convoList = new int[3] { 0, -1, -1 };

        _missions = new int[3] { 0, 0, 0 };
        _trainings = new int[3] { 0, 0, 0 };
        _behaviors = new int[2] { 0, 0 };
    }

    
    
    public void SetID(int ID) {
        _ID = ID;
    }
    public void SetFirstName(string firstName) {
        _firstName = firstName;
    }
    public void SetLastName(string lastName) {
        _lastName = lastName;
    }
    public void SetStatus(string status) {
        _status = status;
    }
    public void SetGender(int gender) {
        _gender = gender;
    }
    public void SetAge(int age) {
        _age = age;
    }
    public void SubtractHealth(int health) {
        _health -= health;
        if (_health < 0) {
            _health = 0;
            _status = "Dead";
        }
    }

    public void SetGreeting(string greeting) {
        _greeting = greeting;
    }
    public void SetImportance(int importance) {
        _importance = importance;
    }
    public void SetFaction(string faction) {
        _faction = faction;
    }

    public void SetPreferences(CharacterManager.loves[] loves) {
        _loves = loves;
    }
    public void SetPreferences(CharacterManager.likes[] likes) {
        _likes = likes;
    }
    public void SetPreferences(CharacterManager.okays[] okays) {
        _okays = okays;
    }
    public void SetPreferences(CharacterManager.dislikes[] dislikes) {
        _dislikes = dislikes;
    }
    public void SetPreferences(CharacterManager.hates[] hates) {
        _hates = hates;
    }
    

    public void SetTraits(int[] traits) {
        _traits = traits;
    }
    public void SetGoals(int[] goals) {
        _goals = goals;
    }
    public void SetSkills(int[] skills) {
        _skills = skills;
    }

    public void AddConvo(int convoID, int index) {
        _convoList[index] = convoID;
    }

    public void SetLocation(int location){
        _roomLocation = location;
    }
    public void SetMissions(int[] missions) {
        _missions = missions;
    }
    public void SetInvoled(int[] involved) {
        _involved = involved;
    }
    public void SetMissionTimes(int[][] missionTimes) {
        _missionTimes = missionTimes;
    }

    
    public void SetRelationshipLvl(int relationship){
        _relationshipLvl = relationship;
    }
    public void AddRelationshipLvl(int rel) {
        _relationshipLvl += rel;
    }
    public void SubtractRelationshipLvl(int rel) {
        _relationshipLvl -= rel;
    }
    public void SetRelationship(string relationship) {
        _relationship = relationship;
    }
    

    public void SetTrainings(int[] trainings) {
        _trainings = trainings;
    }
    public void SetTrainingHours(int[][] hours) {
        _trainingHours = hours;
    }
    public void SetWarned(int warned) {
        _provoked = warned;
    }
    public void AddBehavior(int behavior, int index) {
        _behaviors[index] = behavior;
    }

    public int GetID() {
        return _ID;
    }
    public string GetFirstName() {
        return _firstName;
    }
    public string GetLastName() {
        return _lastName;
    }
    public string GetStatus() {
        return _status;
    }
    public int GetGender() {
        return _gender;
    }
    public int GetAge() {
        return _age;
    }
    public int GetHealth() {
        return _health;
    }
    public string GetGreeting() {
        return _greeting;
    }
    public int GetImportance() {
        return _importance;
    }
    public string GetFaction() {
        return _faction;
    }

    public CharacterManager.likes[] GetLikes() {
        return _likes;
    }
    public CharacterManager.dislikes[] GetDislikes() {
        return _dislikes;
    }
    public CharacterManager.hates[] GetHates() {
        return _hates;
    }
    public CharacterManager.loves[] GetLoves() {
        return _loves;
    }
    public CharacterManager.okays[] GetOkays() {
        return _okays;
    }

    public int[] GetTraits() {
        return _traits;
    }
    public int[] GetGoals() {
        return _goals;
    }
    public int[] GetSkills() {
        return _skills;
    }

    public int[] GetConvoList() {
        return _convoList;
    }


    public int GetRelationshipLvl() {
        return _relationshipLvl;
    }
    public int GetLocation() {
        return _roomLocation;
    }
    public int[] GetMissions() {
        return _missions;
    }
    public int[] GetInvolved() {
        return _involved;
    }
    public int[][] GetMissionTimes() {
        return _missionTimes;
    }
    public string GetRelationship() {
        return _relationship;
    }

    public int[] GetTrainings() {
        return _trainings;
    }
    public int[][] GetTrainingHours() {
        return _trainingHours;
    }
    public int GetWarned() {
        return _provoked;
    }
    public int GetBehavior(int index) {
        return _behaviors[index];
    }
}
