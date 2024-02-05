using System.Diagnostics.CodeAnalysis;
using BIS_project.AppData;
using BIS_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BIS_project.Helper;

public class FileSaver
{
    private IWebHostEnvironment _env;
    private readonly DataContext _dataContext;
    

    public FileSaver(IWebHostEnvironment env, DataContext data)
    {
        _env = env;
        _dataContext = data;
    }

    public async Task<Image> FileSaveAsync(IFormFile file, string filePath)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            string sanitizedFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string route = Path.Combine(_env.WebRootPath, filePath);
    
            var img = new Image()
            {
                Name = file.FileName,
                Path = $"{filePath}/{sanitizedFileName}"
            };
            await _dataContext.Images.AddAsync(img);
            await _dataContext.SaveChangesAsync();
            // image saved

            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }

            string fileRoute = Path.Combine(route, sanitizedFileName);
            using (FileStream fs = File.Create(fileRoute))
            {
                await file.OpenReadStream().CopyToAsync(fs);
            }
            return img;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}", ex);
        }
    }

    public async Task<List<Image>> SaveImageListAsync(List<IFormFile> files, string filePath)
    {
        try
        {
            List<Image> savedImages = new List<Image>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    // Handle the case where a file is null or empty (optional)
                    continue;
                }

                string sanitizedFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string route = Path.Combine(_env.WebRootPath, filePath);

                var img = new Image()
                {
                    Name = file.FileName,
                    Path = $"{filePath}/{sanitizedFileName}"
                };
            
                await _dataContext.Images.AddAsync(img);
                await _dataContext.SaveChangesAsync();

                // Ensure the directory exists
                if (!Directory.Exists(route))
                {
                    Directory.CreateDirectory(route);
                }

                string fileRoute = Path.Combine(route, sanitizedFileName);

                using (FileStream fs = File.Create(fileRoute))
                {
                    await file.OpenReadStream().CopyToAsync(fs);
                }

                savedImages.Add(img);
            }

            return savedImages;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}", ex);
        }
    }



    public async Task<Image> GetFile(string filename)
    {
        return await _dataContext.Images.FirstOrDefaultAsync(e => e.Name == filename);
    }

    public async Task<List<Image>> UploadFiles(IFormFileCollection files, string filePath)
    {
        List<Image> uploadedImages = new List<Image>();
        try
        {
            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    continue; 
                }
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string route = Path.Combine(_env.WebRootPath, filePath);
                var img = new Image()
                {
                    Name = file.FileName,
                    Path = $"{filePath}/{fileName}"
                };
                if (!Directory.Exists(route))
                {
                    Directory.CreateDirectory(route);
                }
                using (FileStream fileStream = new FileStream(Path.Combine(route, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                uploadedImages.Add(img);
            }
            if (uploadedImages.Count > 0)
            {
                await _dataContext.Images.AddRangeAsync(uploadedImages);
                await _dataContext.SaveChangesAsync();
            }
            return uploadedImages;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    
}