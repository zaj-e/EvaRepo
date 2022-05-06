using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FirelessApi.Extensions;

public static class ModeStateExtensions
{
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
    {
        return dictionary.SelectMany(m => m.Value.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
    }
}