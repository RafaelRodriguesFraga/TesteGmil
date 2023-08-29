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
    public class DeleteArtistCommandHandlerTests
    {
        [Fact]
        public async Task Given_ValidArtist_ReturnsCorrectResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingArtist = new Artist("Title");
                var existingArtistMusic = new ArtistMusic(existingArtist.Id, Guid.NewGuid());
                context.Artists.Add(existingArtist);
                context.ArtistMusic.Add(existingArtistMusic);
                context.SaveChanges();

                var command = new DeleteArtistCommand(existingArtist.Id);

                var mapperMock = new Mock<IMapper>();
                var handler = new DeleteArtistCommandHandler(context, mapperMock.Object);

                var expectedArtistDto = new ArtistDto { Name = "Deleted Artist" };
                mapperMock.Setup(mapper => mapper.Map<ArtistDto>(It.IsAny<Artist>()))
                    .Returns(expectedArtistDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(200, result.Code);
                Assert.NotNull(result.Data);
                Assert.Null(result.Error);
            }
        }

        [Fact]
        public async Task Given_ArtistNotFound_ReturnsErrorResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingArtist = new Artist("Title");
                context.Artists.Add(existingArtist);
                context.SaveChanges();

                var command = new DeleteArtistCommand(Guid.NewGuid());

                var mapperMock = new Mock<IMapper>();
                var handler = new DeleteArtistCommandHandler(context, mapperMock.Object);

                var expectedArtistDto = new ArtistDto { Name = "Deleted Artist" };
                mapperMock.Setup(mapper => mapper.Map<ArtistDto>(It.IsAny<Artist>()))
                    .Returns(expectedArtistDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(400, result.Code);
                Assert.Null(result.Data);
                Assert.Equal("Artista não encontrado", result.Error);
            }
        }
    }
}

