using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationContext _context;

        public AttachmentService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddTicketAttachment(IFormFile file, Guid ticketId)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            var id = Guid.NewGuid();

            string fileName = id + extension;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(),
                $"Attachments\\Tickets\\{ticketId.ToString()}\\");

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), 
                $"Attachments\\Tickets\\{ticketId.ToString()}\\",
                fileName);

            var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            await _context.TicketAttachments.AddAsync(new TicketAttachment()
            {
                Id = id,
                TicketId = ticketId,
                FilePath = path,
            });

            await _context.SaveChangesAsync(); 

            return id;
        }

        public async Task<Guid> AddMessageAttachment(IFormFile file, Guid messageId)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            var id = Guid.NewGuid();

            string fileName = id + extension;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(),
                $"Attachments\\Messages\\{messageId.ToString()}\\");

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), 
                $"Attachments\\Messages\\{messageId.ToString()}\\",
                fileName);

            var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            await _context.MessageAttachments.AddAsync(new MessageAttachment()   
            {
                Id = id,
                MessageId = messageId,
                FilePath = path,
            });

            await _context.SaveChangesAsync();

            return id;
        }
        
        public async Task<AttachmentGetDto> GetTicketAttachment(Guid attachmentId)
        {
            var filePath = await _context.TicketAttachments
                .Where(t => t.Id == attachmentId)
                .Select(t=>t.FilePath)
                .FirstOrDefaultAsync();
                
                if (filePath == null)
                {
                    throw new ApiException(404, "Ticket attachment does not exist");
                }
                
                var provider = new FileExtensionContentTypeProvider();
                
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                return new AttachmentGetDto()
                {
                    FileBytes = await File.ReadAllBytesAsync(filePath),
                    ContentType = contentType,
                    FilePath = Path.Combine(filePath),
                };
        }

        public async Task<AttachmentGetDto> GetMessageAttachment(Guid attachmentId)
        {
            var filePath = await _context.MessageAttachments
                .Where(m => m.Id == attachmentId)
                .Select(m=>m.FilePath)
                .FirstOrDefaultAsync();
                
            if (filePath == null)
            {
                throw new ApiException(404, "Message attachment does not exist");
            }
            
            var provider = new FileExtensionContentTypeProvider();
                
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return new AttachmentGetDto()
            {
                FileBytes = await File.ReadAllBytesAsync(filePath),
                ContentType = contentType,
                FilePath = Path.Combine(filePath),
            };
        }
    }
}