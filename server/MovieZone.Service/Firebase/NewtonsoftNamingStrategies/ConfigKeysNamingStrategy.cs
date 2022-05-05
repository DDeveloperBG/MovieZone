namespace MovieZone.Service.Firebase.NewtonsoftNamingStrategies
{
    using System.Linq;

    using Newtonsoft.Json.Serialization;

    public class ConfigKeysNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            if (!name.Any(char.IsUpper))
            {
                return name;
            }

            name = char.ToLower(name[0]) + name[1..];

            for (int i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    name = name[0..i] + '_' + char.ToLower(name[i]) + name[(i + 1)..];
                    i++;
                }
            }

            return name;
        }
    }
}
