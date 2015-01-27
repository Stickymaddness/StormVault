# StormVault
A player statistics tracking tool for Heroes Of The Storm

StormVault is a Windows application for tracking your statistics for the online game Heroes Of The Storm.

Current features include:

* Match tracking - wins and losses account wide and per hero
* Playtime tracking - total playtime account wide and per hero
* Map distribution and how often you've played each map 
* Automatic monitoring for new replays - StormVault will automatically update when a match ends (can be disabled)
* Graphical representation of statistic data

![StormVault1](http://i.imgur.com/Y3lKuOy.png)
![StormVault2](http://i.imgur.com/Rbrcfjg.png)

# Requirements

StormVault requires .NET 4.5 or above, which subsequently requires Windows 7 or above.

# How does it work?

When you first run StormVault you are prompted to enter your player name and directory to where your replay files are stored. 
StormVault will then load and parse all of your replay files, extracting the stats it needs, this is where StormVault gets all its information from. 
Once StormVault has parsed a replay, it saves the info it needs so that it does not need to parse the same replays every time on startup.

# Acknowledgment

Special thanks to Barrett77 for his <a href="https://github.com/barrett777/Heroes.ReplayParser">replay parser</a> and nickaceves for his <a href="https://github.com/nickaceves/nmpq">in memory mpq parserr</a> !
