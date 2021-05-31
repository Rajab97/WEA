using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Interfaces.Services;
using WEA.Core.Models;
using WEA.Presentation.Models;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Services
{
    public class FileServiceFacade : BaseServiceFacade
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public FileServiceFacade(IAttachmentService attachmentService, IWebHostEnvironment webHostEnvironment,IConfiguration configuration)
        {
            _attachmentService = attachmentService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public async Task<Result> UploadAsync(AttachmentViewModel model)
        {
            try
            {
                var files = new List<AttachmentModel.File>();
                var attachmentModel = new AttachmentModel()
                {
                    FolderPath = Path.Combine(_webHostEnvironment.WebRootPath, _configuration.GetValue<string>("FileSettings:FolderName")),
                    Files = files
                };
                if (model.Base64String != null && model.Base64String.Any())
                {
                    int i = 0;
                    foreach (var item in model.Base64String)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var name = model.FileName[i];
                            var conType = model.ContentType[i];
                            var base64String = item.Substring(item.IndexOf(',') + 1);
                            byte[] bytes = Convert.FromBase64String(base64String);
                            AttachmentModel.File file = new AttachmentModel.File()
                            {
                                Stream = bytes,
                                ContentType = conType,
                                FileExtension = Path.GetExtension(name),
                                FileName = name,
                                ContentLength = bytes.Length
                            };
                            files.Add(file);
                        }
                        i++;
                    }
                }
                attachmentModel.ProductId = model.ProductId;
                var result = await _attachmentService.UploadAsync(attachmentModel);
                return result;
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result.Failure(ExceptionMessages.FatalError);
            }
        } 
    }
}
