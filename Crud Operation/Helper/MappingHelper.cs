using Microsoft.AspNetCore.Hosting;

namespace Crud_Operation.Helper
{
    public  class MappingHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MappingHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(IFormFile model)
        {
            string uploadFileName = string.Empty;
            try
            {
                if (model != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
                    uploadFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                    string fullPath = Path.Combine(uploadFolder, uploadFileName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        model.CopyTo(fileStream);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while uploading the image.");
            }

            return uploadFileName;
        }
    }
}
