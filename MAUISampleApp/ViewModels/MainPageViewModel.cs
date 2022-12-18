using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MAUISampleApp.Controls;
using MAUISampleApp.Models;
using MAUISampleApp.Interfaces;
using MAUISampleApp.Constants;
using System.Text.Json;
using System.Runtime.Serialization;

namespace MAUISampleApp.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
    {
        readonly ICameraService _cameraService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region properties
        private WebViewSource source;
        public WebViewSource Source
        {
            get => source;
            set
            {
                if (!object.Equals(source, value))
                {
                    source = value;
                    OnPropertyChanged();
                }
            }
        }
        private string capturedImage { get; set; }
        public string CapturedImage {
            get => capturedImage;
            set
            {
                if (!object.Equals(capturedImage, value))
                {
                    capturedImage = value;
                    OnPropertyChanged();
                }
            }
        }
        private Coordinates coordinates { get; set; }
        public Coordinates Coordinates
        {
            get { return coordinates; }
            set
            {
               coordinates = value;
               OnPropertyChanged();
            }
        }
        #endregion

        #region Command

        public ICommand SendCoordinatesToWebView
        {
            get { return new Command<object>(OnSendLocationToWebViewCommand); }
        }
        public ICommand WebViewCommand
        {
            get { return new Command<object[]>(OnWebViewCommand); }
        }

        private void OnSendLocationToWebViewCommand(object sender)
        {
            var webView = sender as HybridWebView;
            Task.Run(async () => await webView.EvaluateJavaScriptAsync(new EvaluateJavaScriptAsyncRequest("nativeToWebViewSendCoordinates()")));
          //Task.Run(async () => await webView.EvaluateJavaScriptAsync(new EvaluateJavaScriptAsyncRequest("nativeToWebViewSendCoordinates()")));
            // Task.Run(async () => await webView.RegisterAction(new Action<string>("nativeToWebViewSendCoordinates()")));

        }
        private void OnWebViewCommand(object[] sender)
        {
            if (sender == null)
                return;

            var webView = sender[0] as HybridWebView;
            string data = sender[1].ToString();
            if (data != "TakePhoto")
                Coordinates = JsonSerializer.Deserialize<Coordinates>(data);
            else
            {
                CapturedImage = null;
                //CapturedImage = await _cameraService.TakePhoto();
                TakePhoto(webView);
            }
        }

        #endregion

        public MainPageViewModel()//(ICameraService cameraService)
        {
            // _cameraService = cameraService;
            Coordinates = new Coordinates();
            Source = new HtmlWebViewSource() { Html = UIConstant.mainPageHtmlSource };
        }

       
        public void TakePhoto(HybridWebView webView)
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
                        CapturedImage = Convert.ToBase64String(byteArray);
                        if (CapturedImage != null)
                        {
                            Task.Run(async () =>await webView.EvaluateJavaScriptAsync(new EvaluateJavaScriptAsyncRequest("nativeToWebViewBindImage('" + CapturedImage + "')")));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
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

