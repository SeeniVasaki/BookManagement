using Azure;
using BAL.Models;
using BAL.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static BAL.Utilities.Constants;
using static System.Reflection.Metadata.BlobBuilder;

namespace BAL.Services
{
    public class BookService : IBookService
    {
        private readonly string _connectionString;

        public BookService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    

        public async Task<JsonResponse> GetBooksSortedByPublisherAuthorTitle()
        {
            var books = new List<Book>();
            JsonResponse response = new();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SP_GetBooksSortedByPublisherAuthorTitle", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                Publisher = reader["Publisher"].ToString(),
                                Title = reader["Title"].ToString(),
                                AuthorLastName = reader["AuthorLastName"].ToString(),
                                AuthorFirstName = reader["AuthorFirstName"].ToString(),
                                YearPublished = Convert.ToInt32(reader["PublishedYear"]),
                                CityPublished = reader["PublishedCity"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                            books.Add(book);
                        }
                    }
                }
                response.Status = ResponseStatus.Success;
                response.Message = ResponseStatus.Success;
                response.Data = books;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Failure;
                response.Message = ResponseMessages.Failure;
            }

            return response;
        }

        public async Task<JsonResponse> GetBooksSortedByAuthorTitle()
        {
            var books = new List<Book>();
            JsonResponse response = new();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SP_GetBooksSortedByAuthorTitle", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                Publisher = reader["Publisher"].ToString(),
                                Title = reader["Title"].ToString(),
                                AuthorLastName = reader["AuthorLastName"].ToString(),
                                AuthorFirstName = reader["AuthorFirstName"].ToString(),
                                YearPublished = Convert.ToInt32(reader["PublishedYear"]),
                                CityPublished = reader["PublishedCity"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                            books.Add(book);
                        }
                    }
                }

                response.Status = ResponseStatus.Success;
                response.Message = ResponseMessages.Success;
                response.Data = books;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Failure;
                response.Message = ResponseMessages.Failure;
            }
            return response;
        }

        public async Task<JsonResponse> GetTotalPriceOfAllBooks()
        {
            JsonResponse response = new();
            decimal total = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SP_GetTotalPriceOfBooks", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = Convert.ToDecimal(reader["TotalPrice"]);
                        }
                    }
                }

                response.Status = ResponseStatus.Success;
                response.Message = ResponseMessages.Success;
                response.Data = total;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Failure;
                response.Message = ResponseMessages.Failure;
            }
            return response;
        }

        public async Task SaveBooks(List<Book> books)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var dataTable = new DataTable();
                dataTable.Columns.Add("Publisher", typeof(string));
                dataTable.Columns.Add("Title", typeof(string));
                dataTable.Columns.Add("AuthorLastName", typeof(string));
                dataTable.Columns.Add("AuthorFirstName", typeof(string));
                dataTable.Columns.Add("Price", typeof(decimal));
                dataTable.Columns.Add("PublishedYear", typeof(int));
                dataTable.Columns.Add("PublishedCity", typeof(string));

                foreach (var book in books)
                {
                    dataTable.Rows.Add
                    (
                        book.Publisher
                        , book.Title
                        , book.AuthorLastName
                        , book.AuthorFirstName
                        , book.Price
                        , book.YearPublished
                        , book.CityPublished
                    );
                }

                var command = new SqlCommand("SP_InsertBooks", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Books", dataTable);
                command.ExecuteNonQuery();
            }
        }
    }
}
