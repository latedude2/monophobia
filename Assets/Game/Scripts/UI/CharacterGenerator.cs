using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> FemaleNames = new List<string>(){ "Olivia", "Emma", "Ava", "Charlotte", "Sophia", "Amelia", "Isabella", "Mia", "Evelyn", "Harper", "Camila", "Gianna", "Abigail", "Luna", "Ella", "Elizabeth", "Sofia", "Emily", "Avery", "Mila", "Scarlett", "Eleanor", "Madison", "Layla", "Penelope", "Aria", "Chloe", "Grace", "Ellie", "Nora", "Hazel", "Zoey", "Riley", "Victoria", "Lily", "Aurora", "Violet", "Nova", "Hannah", "Emilia", "Zoe", "Stella", "Everly", "Isla", "Leah", "Lillian", "Addison", "Willow", "Lucy", "Paisley", "Natalie", "Naomi", "Eliana", "Brooklyn", "Elena", "Aubrey", "Claire", "Ivy", "Kinsley", "Audrey", "Maya", "Genesis", "Skylar", "Bella", "Aaliyah", "Madelyn", "Savannah", "Anna", "Delilah", "Serenity", "Caroline", "Kennedy", "Valentina", "Sophie", "Alice", "Gabriella", "Sadie", "Ariana", "Allison", "Hailey", "Autumn", "Nevaeh", "Natalia", "Quinn", "Josephine", "Sarah", "Cora", "Emery", "Samantha", "Piper", "Leilani", "Eva", "Everleigh", "Madeline", "Lydia", "Jade", "Kasia", "Anastazja", "Lina", "Dovile" };
    public List<string> MaleNames = new List<string>(){ "Liam", "Noah", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander", "Mason", "Michael", "Ethan", "Daniel", "Jacob", "Logan", "Jackson", "Levi", "Sebastian", "Mateo", "Jack", "Owen", "Theodore", "Aiden", "Samuel", "Joseph", "John", "David", "Wyatt", "Matthew", "Luke", "Asher", "Carter", "Julian", "Grayson", "Leo", "Jayden", "Gabriel", "Isaac", "Lincoln", "Anthony", "Hudson", "Dylan", "Ezra", "Thomas", "Charles", "Christopher", "Jaxon", "Maverick", "Josiah", "Isaiah", "Andrew", "Elias", "Joshua", "Nathan", "Caleb", "Ryan", "Adrian", "Miles", "Eli", "Nolan", "Christian", "Aaron", "Cameron", "Ezekiel", "Colton", "Luca", "Landon", "Hunter", "Jonathan", "Santiago", "Axel", "Easton", "Cooper", "Jeremiah", "Angel", "Roman", "Connor", "Jameson", "Robert", "Greyson", "Jordan", "Ian", "Carson", "Jaxson", "Leonardo", "Nicholas", "Dominic", "Austin", "Everett", "Brooks", "Xavier", "Kai", "Jose", "Parker", "Adam", "Jace", "Wesley", "Kayden", "Mikkel", "Abderrahman", "Simonas", "David", "Kristinn" };
    public List<string> Professions = new List<string>(){ "accountant", "actor", "animator", "architect", "baker", "biologist", "builder", "butcher", "career counselor", "nursing home caretaker", "comic book writer", "ceo of a big company", "programmer", "chef", "decorator", "dentist", "designer","director", "doctor", "economist", "editor", "electrician", "engineer", "executive", "farmer", "film director", "fisherman", "flight attendant", "garbage collector", "geologist", "hairdresser", "head teacher", "jeweler", "journalist", "judge", "lawyer", "lecturer", "library assistant", "makeup artist", "manager", "miner", "musician", "nurse", "optician", "painter", "personal assistant", "photographer", "pilot", "plumber", "police officer", "politician", "porter", "printer", "prison officer", "professional gambler", "receptionist", "sailor", "stay at home parent", "salesperson", "scientist", "secretary", "shop assistant","sign language Interpreter", "singer", "soldier", "solicitor", "surgeon", "tailor", "teacher", "translator", "travel agent", "trucker", "TV cameraman", "TV presenter", "vet", "waiter", "web designer", "writer", "game developer" };
    public List<string> ChildActivities = new List<string>(){ "accountant for teddy bears", "actor in school play", "dollhouse architect", "sand cake baker", "little biologist", "lego builder", "spiderman fan", "teddybear doctor", "cartoon binge-watcher", "goldfish owner", "mommy's hairdresser", "teddybear teacher", "juggler", "daddy's assistant", "little wizard", "ukulele player", "nurse for dolls", "finger-painter", "frog photographer", "pretend police officer", "little chemist", "mommy's assistant", "nursery rhymes singer", "tinsoldier owner", "teddybear surgeon", "doll's tailor", "translator from imaginary language", "batman fan", "hamsters owner", "fairytales writer", "imaginary friend's caretaker", "video game player" };
    public List<string> AdultLastWords = new List<string>(){ "Please don't", "I'm not ready", "I'm too young to die", "I'm so cold", "Tell my mom I love her", "Don't let me go like this", "Why didn't you save me?", "I will never forgive you", "I did not deserve this!", "No, not like this", "Dad, I'm coming", "I'm gonna see Jesus", "Goodnight", "I don't want to die", "Don't let them take me", "Where are they taking me", "Oh my God it hurts so much", "They took my legs! Where are my legs?!", "I can't breathe", "Who turned the lights off?", "God help me!", "Goodbye honey, I'll see you later", "I can hear the rain", "Don't leave me alone", "I was supposed to go skiing today" };
    public List<string> ChildLastWords = new List<string>(){ "Please help me", "Where is my mommy?", "Auuuuchie, it hurts!", "It's very cold", "I want my daddy", "Aaaaaaaaaaaaa!", "Save me!", "Nooooooooo", "Mommy where are you?", "Mommy, I peed my pants!", "Is this mister Jesus?", "I don't want to die", "Don't let them take me", "What is happening?", "Dear God please help!", "They took my teddybear!", "Whyyyyy?", "I'm so sleepy...", "Mommy why are you crying?", "Take me, not my sister!", "I'm scared", "Don't leave me here", "Is it because I didn't go to the church?", "" };

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDeadCharacter() {
        Object[] RandomImages = Resources.LoadAll("Sprites/character_faces", typeof(Sprite));
        Sprite RandomImage = (Sprite)RandomImages[Random.Range(0, 68)];

        string CharacterName = "";
        string Age = "";
        string Occupation = "";
        string LastWords = "";

        if (RandomImage.name.Contains("f")) {
            CharacterName = FemaleNames[Random.Range(0, FemaleNames.Count - 1)];
        } else if (RandomImage.name.Contains("m")) {
            CharacterName = MaleNames[Random.Range(0, MaleNames.Count - 1)];
        }

        if (RandomImage.name.Contains("a")) {
            if (RandomImage.name.Contains("y")) {
                Age = Random.Range(21, 39).ToString();
            } else if (RandomImage.name.Contains("o")) {
                Age = Random.Range(40, 60).ToString();
            }
           Occupation = Professions[Random.Range(0, Professions.Count - 1)];
           LastWords = AdultLastWords[Random.Range(0, AdultLastWords.Count - 1)];
        } else if (RandomImage.name.Contains("c")) {
            if (RandomImage.name.Contains("y")) {
                Age = Random.Range(3, 5).ToString();
            } else if (RandomImage.name.Contains("o")) {
                Age = Random.Range(6, 11).ToString();
            }
           Occupation = ChildActivities[Random.Range(0, ChildActivities.Count - 1)];
           LastWords = ChildLastWords[Random.Range(0, ChildLastWords.Count - 1)];
        }

        gameObject.transform.Find("Bio").GetComponent<Text>().text = CharacterName + ", " + Age + ", " + Occupation;
        gameObject.transform.Find("LastWords").GetComponent<Text>().text = '"' + LastWords + '"';
        gameObject.transform.Find("Image").GetComponent<Image>().sprite = RandomImage;

    }
}
