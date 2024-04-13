using BAL.Models;

namespace BAL.Services.Interface
{
    public interface IBookService
    {
        Task<JsonResponse> GetBooksSortedByPublisherAuthorTitle();

        Task<JsonResponse> GetBooksSortedByAuthorTitle();

        Task<JsonResponse> GetTotalPriceOfAllBooks();

        Task SaveBooks(List<Book> books);
    }
}
