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
    public class FindMusicsQueryHandlerTests
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
                var existingMusic = new Music("Existing Music", Guid.NewGuid());
                context.Musics.Add(existingMusic);
                context.SaveChanges();

                var query = new FindMusicsQuery();

                var mapperMock = new Mock<IMapper>();
                var handler = new FindMusicsQueryHandler(context, mapperMock.Object);

                var expectedMusicDto = new MusicDto();
                mapperMock.Setup(mapper => mapper.Map<MusicDto>(It.IsAny<Music>()))
                    .Returns(expectedMusicDto);

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
