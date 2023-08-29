using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGmil.Model.Context;
using TesteGmil.Model.Handlers;
using TesteGmil.Model.Models;
using TesteGmil.Model.Queries;
using TesteGmil.View;
using Xunit;

namespace TesteGmil.Tests.Handlers.Queries
{
    public class FindGenresQueryHandlerTests
    {
        [Fact]
        public async Task Given_ValidGenre_ReturnsCorrectResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingGenre = new Genre("Existing Genre");
                context.Genres.Add(existingGenre);
                context.SaveChanges();

                var query = new FindGenresQuery();

                var mapperMock = new Mock<IMapper>();
                var handler = new FindGenresQueryHandler(context, mapperMock.Object);

                var expectedGenreDto = new GenreDto();
                mapperMock.Setup(mapper => mapper.Map<GenreDto>(It.IsAny<Genre>()))
                    .Returns(expectedGenreDto);

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(200, result.Code);
                Assert.NotNull(result.Data);
                Assert.Null(result.Error);
            }
        }
    }
}
