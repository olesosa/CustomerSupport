using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CS.BL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationContext _context;

        public AttachmentService(ApplicationContext context)
        {
            _context = context;
        }

        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<string> AddAttachment(IFormFile file, Guid guid)
        {
            var path = GenerateFilePath(file, guid.ToString());
                
            var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);
            
            await _context.TicketAttachments.AddAsync(new TicketAttachment()
            {
                TicketId = guid,
                FilePath = path,
            });

            await _context.SaveChangesAsync();

            return path;
        }

        private string GenerateFilePath(IFormFile file, string name)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            string fileName = name + extension;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            return Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);
        }
    }
}