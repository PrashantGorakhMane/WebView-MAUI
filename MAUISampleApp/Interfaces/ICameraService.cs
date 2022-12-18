using System;
namespace MAUISampleApp.Interfaces
{
	public interface ICameraService
	{
        Task<string> TakePhoto();
    }
}

