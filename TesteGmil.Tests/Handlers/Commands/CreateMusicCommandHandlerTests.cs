using AutoMapper;
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
    public class CreateMusicCommandHandlerTests
    {
        [Fact]
        public async Task Given_ValidMusicCommand_ReturnsCorrectResult()
        {            
            var command = new CreateMusicCommand
            {
                Title = "Test Music",
                GenderId = Guid.NewGuid(), 
                ArtistId = Guid.NewGuid()
            };

            var dbContextMock = new Mock<TestGmilContext>();
            var mapperMock = new Mock<IMapper>();

            dbContextMock.Setup(context => context.Add(It.IsAny<Music>()));
            dbContextMock.Setup(context => context.Add(It.IsAny<ArtistMusic>()));
            dbContextMock.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1); 

            var handler = new CreateMusicCommandHandler(dbContextMock.Object, mapperMock.Object);

            var expectedMusicDto = new MusicDto { Title = "Test Music" };
            mapperMock.Setup(mapper => mapper.Map<MusicDto>(It.IsAny<Music>()))
                .Returns(expectedMusicDto);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(201, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedMusicDto, result.Data);
            Assert.Null(result.Error);
        }
    }
}

