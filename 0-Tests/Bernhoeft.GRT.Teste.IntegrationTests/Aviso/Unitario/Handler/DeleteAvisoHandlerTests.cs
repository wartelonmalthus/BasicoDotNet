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
    public class DeleteAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repositoryMock;
        private readonly Mock<IValidator<DeleteAvisoRequest>> _validatorMock;
        private readonly DeleteAvisoHandler _handler;

        public DeleteAvisoHandlerTests()
        {
            _repositoryMock = new Mock<IAvisoRepository>();
            _validatorMock = new Mock<IValidator<DeleteAvisoRequest>>();
            _handler = new DeleteAvisoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveDesativarAviso_QuandoAvisoExiste()
        {
            // Arrange
            var request = new DeleteAvisoRequest { Id = 1 };

            var avisoExistente = new AvisoEntity("Título", "Mensagem");
            

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
            avisoExistente.Ativo.Should().BeFalse();
            avisoExistente.DataAlteracao.Should().NotBeNull();

            _repositoryMock.Verify(x => x.Update(It.IsAny<AvisoEntity>()));
            _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}