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
    public class CreateGenreCommandHandlerTests
    {
        [Fact]
        public async Task Given_ValidGenre_ReturnsCorrectResult()
        {
            var command = new CreateGenreCommand { Name = "Test Genre" };
            var dbContextMock = new Mock<TestGmilContext>();
            var mapperMock = new Mock<IMapper>();

            dbContextMock.Setup(context => context.Add(It.IsAny<Genre>()));
            dbContextMock.Setup(context => context
                .SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1); 

            var handler = new CreateGenreCommandHandler(dbContextMock.Object, mapperMock.Object);

            var expectedGenreDto = new GenreDto { Name = "Test Genre" };
            mapperMock.Setup(mapper => mapper.Map<GenreDto>(It.IsAny<Genre>()))
                .Returns(expectedGenreDto);
       
            var result = await handler.Handle(command, CancellationToken.None);
         
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(201, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedGenreDto, result.Data);
            Assert.Null(result.Error);
        }
    }
}

