using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Handlers.Commands;
using TesteGmil.Model.Models;
using TesteGmil.View;
using Xunit;

namespace TesteGmil.Tests.Handlers.Commands
{
    public class UpdateGenreCommandHandlerTests
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
                var existingGenre = new Genre("Title");
                context.Genres.Add(existingGenre);
                context.SaveChanges();

                var command = new UpdateGenreCommand(existingGenre.Id, "Updated Genre");

                var mapperMock = new Mock<IMapper>();
                var handler = new UpdateGenreCommandHandler(context, mapperMock.Object);

                var expectedGenreDto = new GenreDto { Name = "Updated Genre" };
                mapperMock.Setup(mapper => mapper.Map<GenreDto>(It.IsAny<Genre>()))
                    .Returns(expectedGenreDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(200, result.Code);
                Assert.NotNull(result.Data);
                Assert.Null(result.Error);
            }
        }

        [Fact]
        public async Task Given_GenreNotFound_ReturnsErrorResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingGenre = new Genre("Title");
                context.Genres.Add(existingGenre);
                context.SaveChanges();

                var command = new UpdateGenreCommand(Guid.NewGuid(), "Updated Genre");

                var mapperMock = new Mock<IMapper>();
                var handler = new UpdateGenreCommandHandler(context, mapperMock.Object);

                var expectedGenreDto = new GenreDto { Name = "Updated Genre" };
                mapperMock.Setup(mapper => mapper.Map<GenreDto>(It.IsAny<Genre>()))
                    .Returns(expectedGenreDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(400, result.Code);
                Assert.Null(result.Data);
                Assert.Equal("Genero não encontrado", result.Error);
            }
        }
    }
}
