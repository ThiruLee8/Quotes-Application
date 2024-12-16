using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Quotes.Api.Controllers;
using Quotes.Common.AppResponse;
using Quotes.Common.Constants;
using Quotes.Common.CustomExceptions;
using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using Quotes.Data.Repositories.Interface;
using Quotes.Service.Implementations;
using Quotes.Service.Infrastructure.AutoMapper;

namespace Quotes.Test.Tests
{
    public class ApiTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IQuoteRepo> _quoteRepoMock;
        private readonly QuoteService _quoteservice;
        private readonly QuotesController _quoteControllerMock;

        public ApiTests()
        {
            _mapper = GetMapper();
            _quoteRepoMock = new Mock<IQuoteRepo>();
            _quoteservice = new QuoteService(_quoteRepoMock.Object, _mapper);
            _quoteControllerMock = new QuotesController(_quoteservice);
        }
        [Fact]
        public async void CreateQuote_Retuns200Result_WithStringResponse()
        {
            //Arrange
            var req = GetQuoteReqList(quoteReq1);
            CreateAddQuoteRepoMock();

            //Act
            var resp = await _quoteControllerMock.CreateQuotes(req);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<string>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equal(AppMessage.QuoteCreatedSuccess, returnedQuote.Response);
        }

        [Fact]
        public async void CreateQuotesList_Retuns200Result_WithStringResponse()
        {
            //Arrange
            var req = GetQuoteReqList(quoteReq1, quoteReq2);
            CreateAddQuoteRepoMock();

            //Act
            var resp = await _quoteControllerMock.CreateQuotes(req);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<string>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equal(AppMessage.QuoteCreatedSuccess, returnedQuote.Response);
        }
        [Fact]
        public async void CreateQuote_InvalidData_WithErorResponse()
        {
            //Arrange
            var data = new QuoteReqDto { Author = "some author" };
            var req = GetQuoteReqList(data);
            CreateAddQuoteRepoMock();

            //Act
            try
            {
                var resp = await _quoteControllerMock.CreateQuotes(req);
            }
            catch (Exception ex)
            {
                //Assert.
                Assert.IsType<UserFriendlyException>(ex);
                Assert.NotNull(ex.Message);
            }
        }

        [Fact]
        public async void GetAllQuotesList_Retuns200Result_WithAllQuotes()
        {
            //Arrange
            var result = GetRepoQuote(quoteReq1, quoteReq2);
            var expectedResult = GetQuoteRespList(result);
            CreateGetAllQuoteRepoMock(result);

            //Act
            var resp = await _quoteControllerMock.GetAllQuotes();

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<List<QuoteRespDto>>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equal(expectedResult.Count, returnedQuote.Response.Count);
            Assert.Equivalent(expectedResult, returnedQuote.Response);
        }

        [Fact]
        public async void GetQuoteById_Retuns200Result_GetsIdMatchingQuote()
        {
            //Arrange
            var quote = GetRepoQuote(quoteReq1).FirstOrDefault();
            CreateGetQuoteByIdRepoMock(quote);
            var expectedResult = GetQuoteResp(quote);

            //Act
            var resp = await _quoteControllerMock.GetQuoteById(quote.QuoteId);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<QuoteRespDto>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equivalent(expectedResult, returnedQuote.Response);
            Assert.Equal(quote.QuoteId, returnedQuote.Response.QuoteId);
        }

        [Fact]
        public async void GetQuoteById_InvalidId_GetsUserFriendlyException()
        {
            //Arrange
            CreateGetQuoteByIdRepoMock(null);

            //Act
            try
            {
                var resp = await _quoteControllerMock.GetQuoteById(1000);
            }
            catch (Exception ex)
            {
                //Assert.
                Assert.IsType<UserFriendlyException>(ex);
                Assert.NotNull(ex.Message);
                Assert.Equal(AppMessage.InvalidQuoteId, ex.Message);
            }
            
        }

        [Fact]
        public async void UpdateQuote_Retuns200Result_GetsUpdatedQuote()
        {
            //Arrange
            var quote = GetRepoQuote(quoteReq1).FirstOrDefault();
            CreateGetQuoteByIdRepoMock(quote);
            CreateUpdateQuoteRepoMock(quote);
            var expectedResult = GetQuoteResp(quote);

            //Act
            var resp = await _quoteControllerMock.UpdateQuote(quote.QuoteId, quoteReq1);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<QuoteRespDto>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equivalent(expectedResult, returnedQuote.Response);
            Assert.Equal(quote.QuoteId, returnedQuote.Response.QuoteId);
        }

        [Fact]
        public async void UpdateQuote_UseInvalidId_GetUserfrendlyException()
        {
            //Arrange
            CreateGetQuoteByIdRepoMock(null);
            CreateUpdateQuoteRepoMock(null);

            //Act
            try
            {
                var resp = await _quoteControllerMock.UpdateQuote(1000, quoteReq1);
            }
            catch (Exception ex)
            {
                //Assert.
                Assert.IsType<UserFriendlyException>(ex);
                Assert.NotNull(ex.Message);
                Assert.Equal(AppMessage.InvalidQuoteId, ex.Message);
            }

        }

        [Fact]
        public async void DeleteQuote_Retuns200Result_GetDeletedResponseMessage()
        {
            //Arrange
            var quote = GetRepoQuote(quoteReq1).FirstOrDefault();
            CreateGetQuoteByIdRepoMock(quote);
            CreateUpdateQuoteRepoMock(quote);
            var expectedResult = AppMessage.DeleteQuote;

            //Act
            var resp = await _quoteControllerMock.DeleteQuote(quote.QuoteId);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<string>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equal(expectedResult, returnedQuote.Response);
        }

        [Fact]
        public async void DeleteQuote_UseInvalidId_GetUserfriendlyExcepton()
        {
            //Arrange
            CreateGetQuoteByIdRepoMock(null);
            CreateUpdateQuoteRepoMock(null);

            //Act
            try
            {
                var resp = await _quoteControllerMock.DeleteQuote(1000);
            }
            catch (Exception ex)
            {
                //Assert.
                Assert.IsType<UserFriendlyException>(ex);
                Assert.NotNull(ex.Message);
                Assert.Equal(AppMessage.InvalidQuoteId, ex.Message);
            }

        }

        [Fact]
        public async void SearchQuote_Retuns200Result_GetsSearchResult()
        {
            //Arrange
            var filter = new QuoteFilter
            {
                AuthorFilter = "test"
            };
            var quotes = GetRepoQuote(quoteReq1, quoteReq2);
            var expectedResult = GetSearchResp(quotes);
            CreateFilterQuoteRepoMock(expectedResult);

            //Act
            var resp = await _quoteControllerMock.SearchQuote(filter);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<List<QuotePaginatedRespDto>>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equivalent(expectedResult, returnedQuote.Response);
            Assert.Equal(expectedResult.Count, returnedQuote.Response.Count);
        }

        [Fact]
        public async void SearchQuote_NoFilter_GetEmptySearchResult()
        {
            //Arrange
            var filter = new QuoteFilter();
            var expectedResult = new List<QuotePaginatedRespDto>();
            CreateFilterQuoteRepoMock(expectedResult);

            //Act
            var resp = await _quoteControllerMock.SearchQuote(filter);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(resp);
            Assert.Equal(200, okResult.StatusCode);
            var returnedQuote = Assert.IsType<GenericResponse<List<QuotePaginatedRespDto>>>(okResult.Value);
            Assert.NotNull(returnedQuote);
            Assert.Equivalent(expectedResult, returnedQuote.Response);
            Assert.Equal(expectedResult.Count, returnedQuote.Response.Count);
            Assert.Empty(returnedQuote.Response);
        }


        //SetUp
        public void CreateAddQuoteRepoMock()
        {
            _quoteRepoMock.Setup(x => x.CreateQuotesAsync(It.IsAny<List<Quote>>()))
               .ReturnsAsync(true);
        }

        public void CreateGetAllQuoteRepoMock(List<Quote> listReq)
        {
            _quoteRepoMock.Setup(x => x.GetAllQuotesAsync())
               .ReturnsAsync(listReq);
        }

        public void CreateGetQuoteByIdRepoMock(Quote quote)
        {
            _quoteRepoMock.Setup(x => x.GetQuoteByIdAsync(It.IsAny<int>())).ReturnsAsync(quote);
        }

        public void CreateUpdateQuoteRepoMock(Quote quote)
        {
            _quoteRepoMock.Setup(x => x.UpdateQuoteAsync(It.IsAny<Quote>())).ReturnsAsync(quote);
        }

        public void CreateFilterQuoteRepoMock(List<QuotePaginatedRespDto> quotes)
        {
            _quoteRepoMock.Setup(x => x.SearchQuoteAsync(It.IsAny<QuoteFilter>(), CancellationToken.None)).ReturnsAsync(quotes);
        }

        public void CreateDeleteQuoteRepoMock(Quote quote)
        {
            _quoteRepoMock.Setup(x => x.UpdateQuoteAsync(It.IsAny<Quote>())).ReturnsAsync(quote);
        }


        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppAutoMapperProfile());
            });

            return config.CreateMapper();
        }
        QuoteReqDto quoteReq1 = new QuoteReqDto()
        {
            Author = "Test Author",
            InspirationalQuote = "Test Quote",
            Tags = ["TestTag1", "TestTag2"]
        };
        QuoteReqDto quoteReq2 = new QuoteReqDto()
        {
            Author = "Test Author2",
            InspirationalQuote = "Test Quote2",
            Tags = ["TestTag12", "TestTag22"]
        };
        public List<QuoteReqDto> GetQuoteReqList(params QuoteReqDto[] quotes)
        {
            return new List<QuoteReqDto>(quotes);
        }
        public List<QuoteRespDto> GetQuoteRespList(List<Quote> quotes)
        {
            return _mapper.Map<List<QuoteRespDto>>(quotes);
        }
        public QuoteRespDto GetQuoteResp(Quote quote)
        {
            return _mapper.Map<QuoteRespDto>(quote);
        }
        public List<QuotePaginatedRespDto> GetSearchResp(List<Quote> data)
        {
            return data.Select((x, i) => new QuotePaginatedRespDto
            {
                Author = x.Author,
                InspirationalQuote = x.InspirationalQuote,
                QuoteId = x.QuoteId,
                RowNumber = i,
                Tags = x.Tags.Select(x => x.TagName).ToList(),
                TotalCount = data.Count,
            }).ToList();
        }
        public List<Quote> GetRepoQuote(params QuoteReqDto[] quotesParam)
        {
            List<Quote> result = quotesParam.Select((x, i) => new Quote
            {
                QuoteId = i,
                Author = x.Author,
                InspirationalQuote = x.InspirationalQuote,
                Tags = x.Tags.Select(y => new Tag { QuoteId = i, TagId = i, TagName = y }).ToList()
            }).ToList();
            return result;
        }

    }
}
