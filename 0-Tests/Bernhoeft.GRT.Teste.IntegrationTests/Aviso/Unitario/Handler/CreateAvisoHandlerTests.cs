using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Aviso.Unitario.Handler
{
    public class CreateAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repositoryMock;
        private readonly CreateAvisoHandler _handler;

        public CreateAvisoHandlerTests()
        {
            _repositoryMock = new Mock<IAvisoRepository>();
            _handler = new CreateAvisoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarCreated_QuandoDadosValidos()
        {
            // Arrange
            var request = new CreateAvisoRequest
            {
                Titulo = "Teste",
                Mensagem = "Mensagem de teste"
            };

            // Setup correto: AddAsync retorna a entidade
            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity entity, CancellationToken ct) => entity);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(CustomHttpStatusCode.Created);

            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveTrimmarEspacos_QuandoTituloEMensagemTemEspacos()
        {
            // Arrange
            var request = new CreateAvisoRequest
            {
                Titulo = "  Teste  ",
                Mensagem = "  Mensagem de teste  "
            };

            AvisoEntity capturedEntity = null;

            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .Callback<AvisoEntity, CancellationToken>((entity, ct) => capturedEntity = entity)
                .ReturnsAsync((AvisoEntity entity, CancellationToken ct) => entity);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            capturedEntity.Should().NotBeNull();
            capturedEntity!.Titulo.Should().Be("Teste");
            capturedEntity.Mensagem.Should().Be("Mensagem de teste");
        }

        [Fact]
        public async Task Handle_DeveDefinirDataCriacao_QuandoCriarAviso()
        {
            // Arrange
            var request = new CreateAvisoRequest
            {
                Titulo = "Teste",
                Mensagem = "Mensagem"
            };

            AvisoEntity capturedEntity = null;

            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .Callback<AvisoEntity, CancellationToken>((entity, ct) => capturedEntity = entity)
                .ReturnsAsync((AvisoEntity entity, CancellationToken ct) => entity);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            capturedEntity.Should().NotBeNull();
            capturedEntity!.DataCriacao.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            capturedEntity.Ativo.Should().BeTrue();
        }
    }
}