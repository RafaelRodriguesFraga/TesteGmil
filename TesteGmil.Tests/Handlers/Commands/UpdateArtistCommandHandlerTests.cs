using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Handlers.Commands;
using TesteGmil.Model.Models;
using TesteGmil.View;
using Xunit;

namespace TesteGmil.Tests.Handlers.Commands
{
    public class UpdateArtistCommandHandlerTests
    {
        [Fact]
        public async Task Given_ExistingArtist_ReturnsCorrectResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestGmilContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new TestGmilContext(options))
            {
                var existingArtist = new Artist("Existing Artist");
                context.Artists.Add(existingArtist);
                context.SaveChanges();

                var command = new UpdateArtistCommand { Id = existingArtist.Id, Name = "Updated Artist" };

                var mapperMock = new Mock<IMapper>();
                var handler = new UpdateArtistCommandHandler(context, mapperMock.Object);

                var expectedArtistDto = new ArtistDto { Name = "Updated Artist" };
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
                var existingArtist = new Artist("Existing Artist");
                context.Artists.Add(existingArtist);
                context.SaveChanges();

                var command = new UpdateArtistCommand { Id = Guid.NewGuid(), Name = "Updated Artist" };

                var mapperMock = new Mock<IMapper>();
                var handler = new UpdateArtistCommandHandler(context, mapperMock.Object);

                var expectedArtistDto = new ArtistDto { Name = "Updated Artist" };
                mapperMock.Setup(mapper => mapper.Map<ArtistDto>(It.IsAny<Artist>()))
                    .Returns(expectedArtistDto);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(400, result.Code);
                Assert.Null(result.Data);
                Assert.Equal("Artista não encontrado",result.Error);

                // You can add more specific assertions if needed
            }
        }
    }
}



