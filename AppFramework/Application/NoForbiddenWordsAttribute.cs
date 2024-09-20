using System.ComponentModel.DataAnnotations;
using System;

namespace AppFramework.Application;

public class NoForbiddenWordsAttribute : ValidationAttribute
{
    public string[] ForbiddenWords { get; set; }

    public NoForbiddenWordsAttribute(params string[] forbiddenWords)
    {
        ForbiddenWords = forbiddenWords;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var stringValue = value as string;

        if (!string.IsNullOrEmpty(stringValue))
        {
            foreach (var word in ForbiddenWords)
            {
                if (stringValue.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"عبارت '{word}' مجاز نیست.");
                }
            }
        }

        return ValidationResult.Success;
    }
}
