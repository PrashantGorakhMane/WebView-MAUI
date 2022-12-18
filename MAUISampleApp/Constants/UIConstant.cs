using System;
namespace MAUISampleApp.Constants
{
	public static class UIConstant
	{
        public const string mainPageHtmlSource = @"
                                                <html>
                                                <head>
                                                 <style type='text/css'>
                                                    .btnPhoto 
                                                    {  
                                                        border: solid 1px #000000; 
                                                        color: black;
                                                        height: 40px;
                                                        width: 150px;
                                                        margin-bottom:5px;
                                                        font-size:16px;
                                                    }
                                                    .btnLocation 
                                                    {  
                                                        border: solid 1px #000000; 
                                                        color: black;
                                                        height: 40px;
                                                        width: 200px;
                                                        font-size:14px;
                                                    }

                                                    #capturedImg {
                                                            margin-bottom:10px;
                                                        }

                                                </style>
                                                </head>
                                                <body>

                                                <script>
                                                   
                                                    // This function for invoke Native feature from webView
                                                    function buttonClicked(e) {		
		                                                invokeNativeAction('TakePhoto');

                                                    }
                                                    function buttonClickedSendCoordinates(e) {

                                                        var latitude = document.getElementById('latitudeID');
                                                        var longitude = document.getElementById('longitudeID');

                                                        var Coordinates = {
                                                                            Latitude: latitude.value,
                                                                            Longitude: longitude.value,
                                                                          };
  
                                                        // Converting JS object to JSON string
                                                        var data = JSON.stringify(Coordinates);

		                                                invokeNativeAction(data);
                                                    }

                                                    // This function for bind base64 image to image tag
                                                    function nativeToWebViewBindImage(base64data) {
                                                        document.getElementById('capturedImg').src = 'data:image/png;base64,' + base64data;
                                                    }

                                                    function nativeToWebViewSendCoordinates(latitude,longitude) {
                                                       document.getElementById('latitudeID').value = latitude;
                                                       document.getElementById('longitudeID').value = longitude;
                                                    }

                                                </script>

                                                <div style='display: flex; flex-direction: column; justify-content: center; align-items: center; width: 100%'>

                                                    <button class='btnPhoto' id='hereBtn' onclick='javascript:buttonClicked(event)'>Take Photo</button>
                                                    <img id='capturedImg' alt='Captured Image' width='200' height='200'>

                                                    
                                                    <input type='text' id='latitudeID' name='latitudeID' value=''><br>
                                                    <input type='text' id='longitudeID' name='longitudeID' value=''><br>
                                                    <button class='btnLocation' id='hereBtn' onclick='javascript:buttonClickedSendCoordinates(event)'>Send Location Co-ordinates to MAUI</button>

                            
                                                </div> 
                                                </html>
                                                ";
    }
}

