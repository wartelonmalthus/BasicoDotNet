using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Aviso.Unitario.Handler
{
    public class UpdateAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repositoryMock;
        private readonly Mock<IValidator<UpdateAvisoRequest>> _validatorMock;
        private readonly UpdateAvisoHandler _handler;

        public UpdateAvisoHandlerTests()
        {
            _repositoryMock = new Mock<IAvisoRepository>();
            _validatorMock = new Mock<IValidator<UpdateAvisoRequest>>();
            _handler = new UpdateAvisoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarOk_QuandoAvisoExisteEDadosValidos()
        {
            // Arrange
            var request = new UpdateAvisoRequest
            {
                Id = 1,
                Titulo = "Título Atualizado",
                Mensagem = "Mensagem Atualizada"
            };

            var avisoExistente = new AvisoEntity("Título Antigo", "Mensagem Antiga");
                
            

            _validatorMock
                .Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(avisoExistente);

            _repositoryMock
                .Setup(x => x.Update(It.IsAny<AvisoEntity>()))
                .Returns((AvisoEntity entity) => entity); 

            _repositoryMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            result.Data.Should().NotBeNull();
            result.Data.Titulo.Should().Be("Título Atualizado");
            result.Data.Mensagem.Should().Be("Mensagem Atualizada");

            _repositoryMock.Verify(x => x.Update(It.IsAny<AvisoEntity>()));
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveRetornarBadRequest_QuandoValidacaoFalha()
        {
            // Arrange
            var request = new UpdateAvisoRequest
            {
                Id = 1,
                Titulo = "",
                Mensagem = ""
            };

            _repositoryMock
                   .Setup(x => x.Update(It.IsAny<AvisoEntity>()))
                   .Returns((AvisoEntity entity) => entity);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
           
        }
    }
}