namespace Ittech24.Framework.Identity.JsonWebToken
{
    interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
