using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WEA.Core.Entities;
using WEA.Core.Interfaces.Services;
using WEA.Core.Models;
using WEA.Core.Repositories;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.SharedKernel.Resources;

namespace WEA.Core.Services
{
    public class AttachmentService : BaseService<Attachment>, IAttachmentService
    {
        private static Dictionary<string, string> fileExtentionsVsContentTypePairs = new Dictionary<string, string>() {
            { "image/jpeg",".jpeg" }
        };
        private readonly IUnitOfWork _unitOfWork;

        public AttachmentService(IAttachmentRepository attachmentRepository,IUnitOfWork unitOfWork):base(attachmentRepository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> RemoveByProductIdAsync(Guid Id)
        {
            try
            {
                var attachments = GetAll().Data.Where(m => m.ProductId == Id).ToArray();
                foreach (var file in attachments)
                {
                    if (File.Exists(file.FilePath))
                    {
                        File.Delete(file.FilePath);
                    }
                    var fileResult = await RemoveAsync(file.Id);
                    if (!fileResult.IsSucceed)
                    {
                        throw new ApplicationException(ExceptionMessages.FileNotRemoved);
                    }
                }
                return Result.Succeed();
            }
            catch (ApplicationException e)
            {
                return Result.Failure(e.Message);
            }
            catch (Exception e)
            {
                return Result.Failure(ExceptionMessages.FatalError);
            }
        }

        public async Task<Result> UploadAsync(AttachmentModel attachments)
        {

            string directoryPath = string.Empty;
            List<string> filePaths = new List<string>();
            if (!attachments.Files.Any())
            {
                return Result.Failure(ExceptionMessages.MinOnefile);
            }

            try
            {
                var date = DateTime.Now;
                directoryPath = attachments.FolderPath;
                directoryPath = Path.Combine(directoryPath, date.Year.ToString(), date.Month.ToString(), date.Day.ToString());
                foreach (var item in attachments.Files)
                {
                    if (String.IsNullOrEmpty(item.FileExtension) && fileExtentionsVsContentTypePairs.TryGetValue(item.ContentType, out var fileEx))
                        item.FileExtension = fileEx;

                    if (item.ContentLength > int.Parse(Formats.FileSize) * 1024 * 1024)
                        throw new ApplicationException(String.Format(ExceptionMessages.FileSizeNotAllowed, Formats.FileSize));

                    item.FileName = Guid.NewGuid().ToString().Replace('/', 'z');
                    var contentType = item.ContentType;
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    var filePath = Path.Combine(directoryPath, item.FileName + item.FileExtension);
                    await File.WriteAllBytesAsync(filePath,item.Stream);

                    filePaths.Add(filePath);
                    var dto = new Attachment
                    {
                        FileName = item.FileName,
                        FileExtension = item.FileExtension,
                        FilePath = filePath,
                        ProductId = attachments.ProductId,
                        Description = item.Description,
                        ContentType = item.ContentType
                    };
                    await CreateAsync(dto);
                }
                return Result.Succeed();
            }
            catch (ApplicationException ex)
            {
                foreach (var path in filePaths)
                {
                    if (File.Exists(path)) File.Delete(path);
                }
                return Result.Failure(ex);
            }
            catch (Exception ex)
            {
                foreach (var path in filePaths)
                { 
                    if (File.Exists(path)) File.Delete(path);
                }
                return Result.Failure(ExceptionMessages.FatalError);
            }
        }


    }
}
