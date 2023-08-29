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
    public class DeleteMusicCommandHandlerTests
    {
        [Fact]
        public async Task Given_ValidMusic_ReturnsCorrectResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingMusic = new Music("Title", Guid.NewGuid());
                var existingArtistMusic = new ArtistMusic(Guid.NewGuid(), existingMusic.Id);
                context.Musics.Add(existingMusic);
                context.ArtistMusic.Add(existingArtistMusic);
                context.SaveChanges();

                var command = new DeleteMusicCommand(existingMusic.Id);

                var mapperMock = new Mock<IMapper>();
                var handler = new DeleteMusicCommandHandler(context, mapperMock.Object);

                var expectedMusicDto = new MusicDto { Title = "Deleted Music" };
                mapperMock.Setup(mapper => mapper.Map<MusicDto>(It.IsAny<Music>()))
                    .Returns(expectedMusicDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(200, result.Code);
                Assert.NotNull(result.Data);
                Assert.Null(result.Error);
            }
        }

        [Fact]
        public async Task Given_MusicNotFound_ReturnsErrorResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingMusic = new Music("Title", Guid.NewGuid());
                context.Musics.Add(existingMusic);
                context.SaveChanges();

                var command = new DeleteMusicCommand(Guid.NewGuid());

                var mapperMock = new Mock<IMapper>();
                var handler = new DeleteMusicCommandHandler(context, mapperMock.Object);

                var expectedMusicDto = new MusicDto { Title = "Deleted Music" };
                mapperMock.Setup(mapper => mapper.Map<MusicDto>(It.IsAny<Music>()))
                    .Returns(expectedMusicDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(400, result.Code);
                Assert.Null(result.Data);
                Assert.Equal("Musica não encontrada", result.Error);
            }
        }
    }
}

