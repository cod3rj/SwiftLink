using FluentValidation;

namespace SwiftLink.API.Features.Url
{
    public class UrlValidator : AbstractValidator<Url>
    {
        public UrlValidator()
        {
            RuleFor(x => x.OriginalUrl).NotEmpty().WithMessage("Url cannot be empty.");
            RuleFor(x => x.ShortenedUrl).NotEmpty().WithMessage("Shortened Url cannot be empty.");
        }
    }
}