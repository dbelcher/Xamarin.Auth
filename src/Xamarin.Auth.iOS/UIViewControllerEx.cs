//
//  Copyright 2012, Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace Xamarin.Utilities.iOS
{
	internal static class UIViewControllerEx
	{
		public static void ShowError (this UIViewController controller, string title, Exception error, Action continuation = null)
		{
			ShowError (controller, title, error.GetUserMessage (), continuation);
		}

		public static void ShowError (this UIViewController controller, string title, string message, Action continuation = null)
		{
			var mainBundle = NSBundle.MainBundle;
			
			var alert = new UIAlertView (
				mainBundle.LocalizedString (title, "Error message title"),
				mainBundle.LocalizedString (message, "Error"),
				null,
				mainBundle.LocalizedString ("OK", "Dismiss button title for error message"));

			if (continuation != null) {
				alert.Dismissed += delegate {
					continuation ();
				};
			}

      // This is a work-around to account for localhost attempts on iOS...
      if (!String.Equals (message, "Could not connect to the server.") &&
        !String.Equals (message, "Connexion au serveur impossible.") && 
        !String.Equals (message, "Verbindung zum Server konnte nicht hergestellt werden.")  &&
        !String.Equals (message, "サーバに接続できませんでした。") &&
        !String.Equals (message, "Geen verbinding met de server mogelijk.") &&
        !String.Equals (message, "Impossibile connettersi al server.") &&
        !String.Equals (message, "No se ha podido establecer conexión con el servidor.") &&
        !String.Equals (message, "Ei yhteyttä palvelimeen.") &&
        !String.Equals (message, "Kunde inte ansluta till servern.") &&
        !String.Equals (message, "서버에 연결할 수 없습니다.") &&
        !String.Equals (message, "No s’ha pogut connectar al servidor.") &&
        !String.Equals (message, "K serveru se nelze připojit.") &&
        !String.Equals (message, "Nie można było połączyć się z serwerem.") &&
        !String.Equals (message, "未能连接到服务器。") &&
        !String.Equals (message, "無法連接伺服器。")) {
        alert.Show ();
      }
  
		}
	}
}

