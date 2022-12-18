using System;
using MAUISampleApp.Interfaces;
namespace MAUISampleApp.Controls
{
    public class CameraFeatures : ICameraService
    {
        private readonly TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        public async Task<string> TakePhoto()
        {
            FileResult mediaFile = null;
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    mediaFile = Task.Run(async () => await MediaPicker.Default.CapturePhotoAsync()).Result;

                    if (mediaFile != null)
                    {
                        var byteArray = FileToByteArray(mediaFile.OpenReadAsync().Result);
                        return Convert.ToBase64String(byteArray);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

      
        public byte[] FileToByteArray(Stream ImageStream)
        {
            if (null != ImageStream)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ImageStream.CopyTo(memoryStream);
                        ImageStream.Dispose();
                        return memoryStream.ToArray();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }
    }
}
