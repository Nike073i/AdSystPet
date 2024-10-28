using MediatR;

namespace AdSyst.WebFiles.Application.Images.Commands.UploadImage
{
    public record UploadImageCommand(Stream ImageStream, string FileName) : IRequest<Guid>;
}
