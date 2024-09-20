using AppFramework.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs.TaskItems;

public class CreateTaskDto : IValidatableObject
{
    [Required(ErrorMessage = "Enter the title.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Between 3 and 100 characters")]
    [NoForbiddenWords("fuck", "bitch", ErrorMessage = "The title contains illegal words.")]
    public string Title { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public long TaskListId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Title != null && Title.Contains("test"))
        {
            yield return new ValidationResult("The word 'test' is not allowed.", new[] { nameof(Title) });
        }
    }
}

