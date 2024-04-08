﻿using System.Net.Http.Headers;

namespace MoonHotels.Hub.Application.Serializers;

public interface ISerializer<in TRequest, TResponse>
{
    public Task<TResponse> DeserializeAsync(Stream stream);

    Task<MemoryStream> SerializeAsync(TRequest data);

    MediaTypeHeaderValue GetContentType();
}