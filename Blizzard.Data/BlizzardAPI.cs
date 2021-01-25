using Newtonsoft.Json;
using RaidSetupManager.Domain;
using RestSharp;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Blizzard.Data
{
    public static class BlizzardAPI
    {
        private static string AccessToken;
        public static void SetupAccess()
        {
            var client = new RestClient("https://eu.battle.net/oauth/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("clientId", "NTgxNTVlOTgwNzZhNGYxZGFkOTllOTVlY2NkZGE0Yzc6MU8ySXJzcFI0Nk5DQk0xUXRBT0pXdlJ3VzVueDR1UE4=");
            request.AddHeader("clientSecret", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);
            Authorizer JSON = JsonConvert.DeserializeObject<Authorizer>(response.Content);
            AccessToken = JSON.AccesToken;
        }


        public static RaidSetupManager.Domain.Character GetCharacter(string server, string charName)
        {
            server = server.Replace(' ', '-');
            var client = new RestClient($"https://eu.api.blizzard.com/profile/wow/character/{server.ToLower()}/{charName.ToLower()}?namespace=profile-eu");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            IRestResponse response = client.Execute(request);
            try
            {
                if (response.Content == null || response.Content == "") throw new Exception();
                BlizzardCharacter jsonChar = JsonConvert.DeserializeObject<BlizzardCharacter>(response.Content);
                return ConvertBlizzardCharacterToCharacter(jsonChar);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{charName} could not be found");
                return null;
            }
        }

        private static RaidSetupManager.Domain.Character ConvertBlizzardCharacterToCharacter(BlizzardCharacter bs)
        {
            RaidSetupManager.Domain.Character character = new RaidSetupManager.Domain.Character();

            if (bs.name == null || bs.name == "" ||
               bs.realm == null ||
               bs.guild == null ||
               bs.character_class == null ||
               bs.race == null) return null;
             
            character.Name = bs.name;
            character.Realm = bs.realm.name.en_US;
            character.Guild = bs.guild.name;
            character.Class = bs.character_class.name.en_US;
            character.Race = bs.race.name.en_US;
            character.Level = bs.level;
            character.EquppedItemLevel = bs.equipped_item_level;
            character.AverageItemLevel = bs.average_item_level;
            int t = (int)(bs.last_login_timestamp / 1000);
            character.LastLogin = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(t);

            return character;
        }

        public static BlizzardEquipment GetEquipment(string href)
        {
            var client = new RestClient(href);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            IRestResponse response = client.Execute(request);
            try
            {
                BlizzardEquipment jsonEquipment = JsonConvert.DeserializeObject<BlizzardEquipment>(response.Content);
                return jsonEquipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Roster GetRoster(string href)
        {
            var client = new RestClient(href);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            IRestResponse response = client.Execute(request);
            try
            {
                return JsonConvert.DeserializeObject<Roster>(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static BlizzardGuild GetGuild(string server, string guildName)
        {
            server = server.Replace(' ', '-');
            guildName = guildName.Replace(' ', '-');
            var client = new RestClient($"https://eu.api.blizzard.com/data/wow/guild/{server.ToLower()}/{guildName.ToLower()}?namespace=profile-eu");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            IRestResponse response = client.Execute(request);
            try
            {
                return JsonConvert.DeserializeObject<BlizzardGuild>(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    class Authorizer
    {
        [JsonProperty("access_token")]
        public string AccesToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
