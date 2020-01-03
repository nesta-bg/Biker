﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Biker.Core
{
    public interface IPhotoStorage
    {
        Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}
