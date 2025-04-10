// Relevant libraries and classes. UPD-M04/D10: Removed libraries for every team excluding RED and BLU.
using mercenarySerialIdentificationSystem;
using index-reliableexcavationdemolition;
using index-buildersleagueunited;

/****************************************************************************************************

This is important, but depending on the amount of people added to either list and their position, it may take as long as 0.5 seconds for a Sentry to start attacking.
Usually, it takes around 0.02 to 0.05~ seconds longer between locking onto a target and attacking, but you might find a half second delay unattractive.
Granted, the half-second was attained from loading both the Whitelist and Blacklist with 100 Mercenaries each,  and having our Subject be the very last entry in the Blacklist, but still.
Disable Whitelists and Blacklists if you don't want to be put at a disadvantage, no matter how small it may seem.

The most seemingly insignificant shortcomings could be your downfall, do not undermine them.

****************************************************************************************************/
class sentrySelectiveFire
{
  /* Creates whitelist and blacklist.
  M04-D10: I forgot I added a Blacklist as well, so if you want your sentries to deliberately cause a friendly fire incident, then you have that option. */
  private List<int> whitelist = new List<int>();
  private List<int> blacklist = new List<int>();

  // Do NOT disable Local Line 19, anything with a 'system.' prefix you may disable or erase as they are for debugging purposes.
  public static void Main()
  {
    sentrySelectiveFire system = new sentrySelectiveFire();
    sentry.BindToPDA();

    // Call these functions individually to add mercenaries from the console/terminal rather than a PDA or ConTracker.
    system.ManageWhitelistBlacklist(__MERCENARY-ID1-GOES-HERE-MANUALLY__, "whitelist");  // Adds a mercenary to the whitelist. They will never be targetted by binded Sentries until removed.
    system.ManageWhitelistBlacklist(__MERCENARY-ID2-GOES-HERE-MANUALLY__, "blacklist");  // Inverse of the previous. A mercenary added to the blacklist will always be targetted.
    system.CheckTargetStatus(__MERCENARY-ID1or2-GOES-HERE-MANUALLY__);  // Check if a mercenary is whitelisted, blacklisted or is not listed.

    // Call these functions to simulate binding to a PDA or a ConTracker.
    system.BindToPDA(); 
    system.BindToConTracker();
  }
  
  /* Method to manage the whitelist and blacklist

  Worth noting that there is no confirmation prompt for whitelisting or blacklisting.
  Issued commands are final. If a user is already in either list, attempting to add them to the opposite list will immediately add them and remove them from the previous.
  Programmed this way in case an indidivual believed to be trustworthy ended up being a threat, and target priority must be changed immediately.
  You are able to add yourself to the Blacklist. Pray you aren't self-destructive. */
    public void BindToPDA()
    {
        if (PDA.Detected())
        {
            Console.WriteLine("[INFO] PDA Found. Attempting to bind.");
            PDA.addressables(whitelist);
            PDA.addressables(blacklist);
            Console.WriteLine("[SUCCESS] Connected to PDA.");
            /* A PDA is only useful for removing individuals from either list on the fly, or adding mercenaries already detected.
            I hope you're fast. */
            BindToConTracker();
        }
        else
        {
            Console.WriteLine("[ALERT] PDA not found. Attempting to bind to ConTracker. You should look for your PDA in the meantime.")
            BindToContracker();
            /* If it doesn't work the first time, then you probably lost your PDA.
            Such a vulnerability will leave you open, it will try again later, hopefully when it's safe.
            But for the timebeing, it wont bother. Do not let this happen, be vigilant with your equipment. */
        }
    }
    public void BindToConTracker()
    {
        if (ConTracker.Detected())
        {
            Console.WriteLine("[INFO] ConTracker Found. Attempting to bind.");
            PDA.addressables(whitelist);
            PDA.addressables(blacklist);
            Console.WriteLine("[SUCCESS] Connected to ConTracker.");
            /* Controlled and pre-emptive listing. A connection isn't always necessary, as your ConTracker will upload a localized list to your PDA every time a change is made.
            This is so a Sentry reacts faster, because once a connection with a PDA is established, it will pull from the Whitelist and Blacklist immediately.
            Ideally, you set this up before a skirmish, but you may not have a choice in that regard. */
        }
        else
        {
            Console.WriteLine("[ALERT] PDA and ConTracker not found. Sentry Target Acquisition unchanged.")
            // I don't like the fact I have to write code for this. How do you even lose both your PDA *and* your ConTracker? Are you stupid?
            // M04/D10: I wouldn't say that nowadays. I would say far worse.
        }
    }
    public void ManageWhitelistBlacklist(int mercenaryID, string command)
    {
        if (command.ToLower() == "whitelist")
            {
                if (!whitelist.Contains(mercenaryID))
                {
                    whitelist.Add(mercenaryID);  // Whitelists a chosen mercenary.
                    Console.WriteLine("[SUCCESS] " + mercenaryID + " added to whitelist. They will no longer be attacked by connected Sentry Guns.");
                }
            else
                {
                    Console.WriteLine("[WARNING] " + mercenaryID + " is already whitelisted.");
                }
            }
        else if (command.ToLower() == "blacklist")
            {
                if (!blacklist.Contains(mercenaryID))
                {
                    blacklist.Add(mercenaryID);  // Blacklists a chosen mercenary.
                    Console.WriteLine("[SUCCESS] " mercenaryID + " added to blacklist. They will always be attacked by connected Sentry Guns.");
                }
            else
            {
                Console.WriteLine("[WARNING] "  + mercenaryID + " is already blacklisted..");
            }
        }
        else
        {
            Console.WriteLine("[ERROR] Invalid command. Use 'whitelist' or 'blacklist' followed by the ID of a desired Mercenary. Don't forget to add a space.\nExample: whitelist 1");
        }
    }

    // Self-explanatory.
    // M04/D10: Everything here is self-explanatory, why did I write this?
    public void CheckTargetStatus(int mercenaryID)
    {
        // Check if mercenary is in the whitelist
        if (whitelist.Contains(mercenaryID))
        {
            Console.WriteLine("Mercenary ID " + mercenaryID + " is whitelisted. They will not be attacked by connected Sentry Guns.");
        }
        // Check if mercenary is in the blacklist
        else if (blacklist.Contains(mercenaryID))
        {
            Console.WriteLine("Mercenary ID " + mercenaryID + " is blacklisted. They will always be attacked by connected Sentry Guns.");
        }
        else
        {
            Console.WriteLine("Mercenary ID " + mercenaryID + " is either whitelisted nor blacklisted. Connected Sentry Guns will assess target using default behaviour.");
        }
    }
}    
    void AcquireTarget(int mercenaryID)
    {
        if (target(mercenaryID) == whitelisted)
        {
            Console.WriteLine("Target Identified as: " + mercenaryID + ", who is [WHITELISTED]. Systems disengaged.")
        }
        else
        {
            Console.WriteLine("Target Identified as: " + mercenaryID + ", who is [BLACKLISTED]. Systems engaged.")
        }
    }
