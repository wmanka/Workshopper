namespace Workshopper.Application.Common.Abstractions;

public record FileReponse(Stream FileStream, string ContentType, string FileName, string Extension);