using RaidSetupManager.Domain;
using Blizzard.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Character = RaidSetupManager.Domain.Character;

namespace RaidSetupManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Character> GuildRoster;
        public MainWindow()
        {
            GuildRoster = new List<Character>();
            InitializeComponent();
            BlizzardAPI.SetupAccess();
            BlizzardGuild guild = BlizzardAPI.GetGuild("kazzak", "Just Kill The Boss");
            foreach(var m in guild.roster.roster.members)
            {
                if(m.rank < 5 && m.rank != 2)
                {
                    Character c = BlizzardAPI.GetCharacter(m.character.realm.slug, m.character.name);
                    if (c != null)
                    {
                        c.GuildRank = guild.roster.roster.members.First(p => p.character.name.ToLower() == c.Name.ToLower()).rank;
                        GuildRoster.Add(c);
                    }
                }
            }
            GuildRoster = GuildRoster.OrderBy(p => p.GuildRank).ThenBy(p => p.Name).ToList();
            int i = 0;
            foreach(var m in GuildRoster)
            {
                if (m != null)
                {
                    i++;
                    Debug.WriteLine($"Rank: {m.GuildRank} - Name: {m.Name}, a level {m.Level} {m.Race} {m.Class}");
                }
            }
            Debug.WriteLine($"Current roster contains {i} members");
        }
    }
}
