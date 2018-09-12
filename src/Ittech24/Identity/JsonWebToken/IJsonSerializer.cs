namespace Ittech24.Identity.JsonWebToken
{
    interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
