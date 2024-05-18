using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using MediatR;
using System.Runtime.CompilerServices;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Category).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Name is required.");
        



        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async  Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from the command object
            //save to database
            //return CreateProductResult result
            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
