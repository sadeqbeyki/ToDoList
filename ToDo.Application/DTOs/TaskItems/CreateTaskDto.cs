using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs.TaskItems;

public class CreateTaskDto
{
    [Required(ErrorMessage = "عنوان را وارد کنید")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "بین ۳ تا ۱۰۰ حرف")]
    public string Title { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public long TaskListId { get; set; }
}

