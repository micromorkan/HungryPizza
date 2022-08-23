namespace HungryPizza.Api.Util
{
    public class Utils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetEnumString(Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }
    }
}
