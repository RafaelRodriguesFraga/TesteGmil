using AutoMapper;
using Moq;
using TesteGmil.Model.Commands;
using TesteGmil.Model.Context;
using TesteGmil.Model.Handlers.Commands;
using TesteGmil.Model.Models;
using TesteGmil.View;
using Xunit;

namespace TesteGmil.Tests.Handlers.Commands
{
    public class CreateArtistCommandHandlerTests
    {
        [Fact]
        public async Task Given_ValidArtist_ReturnsCorrectResult()
        {
            var command = new CreateArtistCommand { Name = "Test Artist" };
            var dbContextMock = new Mock<TestGmilContext>();
            var mapperMock = new Mock<IMapper>();

            dbContextMock.Setup(context => context.Add(It.IsAny<Artist>()));
            dbContextMock.Setup(context => context
                .SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1); 

            var handler = new CreateArtistCommandHandler(dbContextMock.Object, mapperMock.Object);

            var expectedArtistDto = new ArtistDto { Name = "Test Artist" };
            mapperMock.Setup(mapper => mapper.Map<ArtistDto>(It.IsAny<Artist>()))
                .Returns(expectedArtistDto);
       
            var result = await handler.Handle(command, CancellationToken.None);
         
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(201, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedArtistDto, result.Data);
            Assert.Null(result.Error);
        }
    }
}

