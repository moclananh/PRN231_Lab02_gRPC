using Grpc.Core;
using GrpcService.Models;
using MyProto;
using System.Linq;
using System.Threading.Tasks;
using static MyProto.GrpcBook;

namespace GrpcService.Services
{
    public class BookService: GrpcBookBase
    {
        public readonly PRN231_Lab02_gRPCContext _db;

        public BookService(PRN231_Lab02_gRPCContext db)
        {
            _db = db;
        }

        public override Task<BookList> GetAll(MyProto.Empty requestData, ServerCallContext context)
        {
            BookList response = new BookList();
            var cusList = from obj in _db.Books
                          select new MyProto.Book()
                          {
                              Id = obj.BId,
                              Name = obj.BName,
                              Version= obj.BVersion,
                              Page = obj.BPages,
                              Price = obj.BPrice,
                              Year= obj.BYear,
                              AuthorId = (int)obj.AId 
                          };
            response.Book.AddRange(cusList);

            return Task.FromResult(response);
        }

        public override Task<AuthorList> GetAllAuthor(MyProto.Empty requestData, ServerCallContext context)
        {
            AuthorList response = new AuthorList();
            var cusList = from obj in _db.Authors
                          select new MyProto.Author()
                          {
                              AuthorId = obj.AId,
                              AuthorName = obj.AName,
                              
                          };
            response.Author.AddRange(cusList);

            return Task.FromResult(response);
        }

        public override Task<MyProto.Book> GetBookById(IDRequest request, ServerCallContext context)
        {

            var obj = this._db.Books.Find(request.Id);

            if (obj == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }


            MyProto.Book book = new MyProto.Book()
            {
                Id = obj.BId,
                Name = obj.BName,
                Version = obj.BVersion,
                Page = obj.BPages,
                Price = obj.BPrice,
                Year = obj.BYear,
                AuthorId = (int)obj.AId
            };

            return Task.FromResult(book);
        }

        public override async Task<MyProto.Book> CreateBook(CreateBookRequest request, ServerCallContext context)
        {
            var bookEntity = new Models.Book()
            {
                BName = request.Name,
                BVersion = request.Version,
                BPages = request.Page,
                BPrice = request.Price,
                BYear = request.Year,
                AId = request.AuthorId
            };

            _db.Books.Add(bookEntity);
            await _db.SaveChangesAsync();

            var book = new MyProto.Book()
            {
                Id = bookEntity.BId,
                Name = bookEntity.BName,
                Version = bookEntity.BVersion,
                Page = bookEntity.BPages,
                Price = bookEntity.BPrice,
                Year = bookEntity.BYear,
                AuthorId = (int)bookEntity.AId
            };

            return book;
        }

        public override async Task<MyProto.Book> UpdateBook(UpdateBookRequest request, ServerCallContext context)
        {
            var bookEntity = _db.Books.Find(request.Id);

            if (bookEntity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }

            bookEntity.BName = request.Name;
            bookEntity.BVersion = request.Version;
            bookEntity.BPages = request.Page;
            bookEntity.BPrice = request.Price;
            bookEntity.BYear = request.Year;
            bookEntity.AId = request.AuthorId;

            await _db.SaveChangesAsync();

            var updatedBook = new MyProto.Book()
            {
                Id = bookEntity.BId,
                Name = bookEntity.BName,
                Version = bookEntity.BVersion,
                Page = bookEntity.BPages,
                Price = bookEntity.BPrice,
                Year = bookEntity.BYear,
                AuthorId = (int)bookEntity.AId
            };

            return updatedBook;
        }

        public override async Task<Empty> DeleteBook(IDRequest request, ServerCallContext context)
        {
            var bookEntity = _db.Books.Find(request.Id);

            if (bookEntity == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }

            _db.Books.Remove(bookEntity);
            await _db.SaveChangesAsync();

            return new Empty();
        }
    }
}

