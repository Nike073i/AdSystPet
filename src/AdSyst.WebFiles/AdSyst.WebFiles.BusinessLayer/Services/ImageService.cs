using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.DAL.MongoDb.Enums;
using AdSyst.WebFiles.DAL.MongoDb.Interfaces;

namespace AdSyst.WebFiles.BusinessLayer.Services
{
    public class ImageService : IImageService
    {
        private static readonly ImageSize[] _imageSizes = new[]
        {
            ImageSize.Source,
            ImageSize.Small
        };

        private readonly IImageRepository _repository;
        private readonly IFileService _fileService;
        private readonly IImageCompressor _imageCompressor;

        public ImageService(
            IImageRepository repository,
            IFileService fileService,
            IImageCompressor imageCompressor
        )
        {
            _repository = repository;
            _fileService = fileService;
            _imageCompressor = imageCompressor;
        }

        public async Task<Guid> SaveAsync(
            Stream sourceStream,
            string imageExtension,
            CancellationToken cancellationToken
        )
        {
            var resourceId = Guid.NewGuid();

            var imageBuilder = new ImageBuilder(
                _imageCompressor,
                _fileService,
                sourceStream,
                resourceId,
                imageExtension
            );

            foreach (var size in _imageSizes)
            {
                await imageBuilder.AddImageSizeAsync(size, cancellationToken);
            }

            var image = imageBuilder.Build();

            await _repository.AddAsync(image, cancellationToken);

            return resourceId;
        }
    }
}
