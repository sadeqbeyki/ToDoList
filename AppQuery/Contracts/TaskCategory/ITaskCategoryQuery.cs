using System.Collections.Generic;

namespace AppQuery.Contracts.BookCategory
{
    public interface ITaskCategoryQuery
    {
        List<TaskCategoryQueryModel> GetBookCategories();
        List<TaskCategoryQueryModel> GetBookCategoriesWithBooks();
    }
}
